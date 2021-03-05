using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountAdLibrary.Account;
using ExceptionsLibrary.Exceptions;
using MainLibrary.Clients;

namespace SQLLibrary
{
    public class SQLManager
    {
        SqlConnection Connection { get; set; }
        public SQLManager(SqlConnection con)
        {
            Connection = con;
        }

        /// <summary>
        /// Получить данные из базы данных и заполнить внутренню базу
        /// </summary>
        /// <param name="con">Подключение к SQL бд</param>
        /// <param name="DBClients">Внутренняя бд</param>
        public void FillData(ObservableCollection<Client> DBClients)
        {
            Connection.Open();

            var sql = @"select *
                        from Clients
                        full outer join Persons
                        on Clients.idClient = Persons.idClient
                        full outer join Entity
                        on Clients.idClient = Entity.idClient";        //получить всех клиентов
            SqlCommand command = new SqlCommand(sql, Connection);
            SqlDataReader r = command.ExecuteReader();

            while (r.Read())
            {
                if (r.GetBoolean(2))    //если физ лицо
                {
                    DBClients.Add(new Person(r.GetString(7), Convert.ToDecimal(r["invoice"]), Convert.ToDecimal(r["earning"]), r.GetString(0), r.GetString(1), r.GetString(3), r.GetBoolean(8)));
                }
                else                    //если юр лицо
                {
                    DBClients.Add(new MainLibrary.Clients.Entity(r.GetString(1), r.GetString(10), r.GetString(11), Convert.ToDecimal(r["invoice"]), Convert.ToDecimal(r["earning"]), r.GetString(0), r.GetString(3)));
                }
                //Debug.WriteLine($"{r.GetString(1)}, {r.GetDecimal(4)}, {r.GetDecimal(5)}, {r.GetString(7)}, {r.GetString(3)}, {r.GetBoolean(8)}");
            }
            r.Close();

            sql = @"select *
                from Deposits";                             //заполнить всю инфу по вкладам
            command = new SqlCommand(sql, Connection);
            r = command.ExecuteReader();

            while (r.Read())
            {
                ManageClientsDeposit(DBClients, r.GetString(0),
                    new Deposit()
                    {
                        Amount = Convert.ToDecimal(r["amount"]),
                        IsCapitilised = r.GetBoolean(3),
                        Percent = Convert.ToDouble(r["percent"])
                    });
            }
            r.Close();

            sql = @"select *
                    from Logs
                    full outer join Clients c1
                    on Logs.idReciever = c1.idClient
                    full outer join Clients c2
                    on Logs.idClient = c2.idClient
                    where Logs.idLog is not null";                          //заполнить всю инфу о логах
            command = new SqlCommand(sql, Connection);
            r = command.ExecuteReader();

            Case @case;
            while (r.Read())
            {
                if (!string.IsNullOrEmpty(r["message"].ToString()))          //ошибка транзакции
                {
                    @case = new Case(new BankTransactionException(r.GetString(6)), r.GetDateTime(2));
                    //Debug.WriteLine($"--------------------> Ошибка {r.GetString(6)}");
                }
                else if (string.IsNullOrEmpty(r["idReciever"].ToString()))   //перевод между счетами
                {
                    @case = new Case(r.GetBoolean(5), Convert.ToDecimal(r["amount"]), r.GetDateTime(2));
                    //string s = r.GetBoolean(5) ? "на счёт" : "на вклад";
                    //Debug.WriteLine($"--------------------> Между своими счетами {r.GetInt32(1)} {s} {Convert.ToDecimal(r["amount"])}");
                }
                else                                                        //перевод между клиентами
                {
                    @case = new Case(r.GetString(8), false, Convert.ToDecimal(r["amount"]), r.GetDateTime(2));
                    ManageClientsLogs(DBClients, r.GetString(3), new Case(r.GetString(14), true, Convert.ToDecimal(r["amount"]), r.GetDateTime(2)));
                    //Debug.WriteLine($"--------------------> Между клиентами {r.GetInt32(1)} -> {r.GetInt32(3)} {Convert.ToDecimal(r["amount"])}");
                }
                ManageClientsLogs(DBClients, r.GetString(1), @case);

            }

            Connection.Close();
        }


        /// <summary>
        /// По индексу клиента заполнить информацию о вкладе
        /// </summary>
        private static void ManageClientsDeposit(ObservableCollection<Client> DBClients, string id, Deposit deposit)
        {
            for (int i = 0; i < DBClients.Count; i++)
            {
                if (DBClients[i].ID == id)
                {
                    DBClients[i].Deposit = deposit;
                    break;
                }
            }
        }


        /// <summary>
        /// По индексу клиента добавить лог
        /// </summary>
        private static void ManageClientsLogs(ObservableCollection<Client> DBClients, string id, Case @case)
        {
            for (int i = 0; i < DBClients.Count; i++)
            {
                if (DBClients[i].ID == id)
                {
                    DBClients[i].Logs.Cases.Add(@case);
                    break;
                }
            }
        }


        /// <summary>
        /// Добавление нового клиента в БД
        /// </summary>
        /// <param name="client">Новый клиент</param>
        public void AddNewClient(Client client)
        {
            Connection.Open();
            string sql = string.Format("INSERT INTO Clients ([idClient], [clientName], [isPerson], [adress], [invoice], [earning]) " +
                "VALUES (N'{0}', N'{1}', {2}, N'{3}', {4}, {5})",
                client.ID, client.ClientName, client is Person ? 1 : 0, client.Address, client.Invoice.Account, client.Invoice.Earning);
            new SqlCommand(sql, Connection).ExecuteNonQuery();
            if (client is Person)
            {
                Person person = client as Person;
                sql = string.Format("INSERT INTO Persons ([idClient], [cName], [isVip]) VALUES (N'{0}', N'{1}', {2})", client.ID, person.Name, person.Vip ? 1 : 0);
            }
            else
            {
                MainLibrary.Clients.Entity entity = client as MainLibrary.Clients.Entity;
                sql = $"INSERT INTO Entity ([idClient], [dirName], [dirSurname]) VALUES (N'{client.ID}', N'{entity.DirName}', N'{entity.DirSurname}')";
            }
            new SqlCommand(sql, Connection).ExecuteNonQuery();

            Connection.Close();
        }


        /// <summary>
        /// Создание вклада у клиента
        /// </summary>
        public void CreateNewDeposit(Client client)
        {
            Connection.Open();
            string sql = $"INSERT INTO [dbo].[Deposits] ([idClient], [amount], [percent], [isCapitalised]) VALUES (N'{client.ID}', 0, 0, 0)";
            new SqlCommand(sql, Connection).ExecuteNonQuery();
            Connection.Close();
        }


        /// <summary>
        /// Обновление информации о вкладе и счёте клиента
        /// </summary>
        public void UpdateDeposit(Client client)
        {
            Connection.Open();
            string sql = string.Format("UPDATE [dbo].[Deposits] SET amount = {0}, isCapitalised = {1}, [percent] = {2} where idClient = '{3}'",
                client.Deposit.Amount.ToString().Replace(',', '.'), client.Deposit.IsCapitilised ? 1 : 0, client.Deposit.Percent.ToString().Replace(',', '.'), client.ID);
            new SqlCommand(sql, Connection).ExecuteNonQuery();
            sql = $"UPDATE Clients SET invoice = {client.Invoice.Account.ToString().Replace(',', '.')} where idClient = '{client.ID}'";
            new SqlCommand(sql, Connection).ExecuteNonQuery();
            Connection.Close();
        }


        /// <summary>
        /// Добавить в лог сведения о том, что был перевод между счетами одного клиента
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="amount">Сумма перевода</param>
        /// <param name="toDeposit">true - на основной счёт, false - на счёт вклада</param>
        public void UpdateAccountDepositLog(Client client, decimal amount, bool toDeposit)
        {
            Connection.Open();
            string sql = string.Format("INSERT INTO Logs ([idClient], [time], [idReciever], [amount], [direction], [message]) " +
                "VALUES (N'{0}', N'{1}', NULL, {2}, {3}, NULL)",
                client.ID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), amount.ToString().Replace(',', '.'), toDeposit ? 1 : 0);
            new SqlCommand(sql, Connection).ExecuteNonQuery();
            Connection.Close();
        }


        /// <summary>
        /// Добавить в лог информацию об успешном переводе между разными клиентами
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="reciever">Получатель</param>
        /// <param name="amount">Сумма</param>
        public void TransactionSucessful(Client sender, Client reciever, decimal amount)
        {
            Connection.Open();
            string sql = string.Format("UPDATE Clients SET invoice = {0} where idClient = '{1}'",
                sender.Invoice.Account.ToString().Replace(',', '.'), sender.ID);
            new SqlCommand(sql, Connection).ExecuteNonQuery();
            sql = string.Format("UPDATE Clients SET invoice = {0} where idClient = '{1}'",
                reciever.Invoice.Account.ToString().Replace(',', '.'), reciever.ID);
            new SqlCommand(sql, Connection).ExecuteNonQuery();

            sql = string.Format("INSERT INTO Logs ([idClient], [time], [idReciever], [amount], [direction], [message]) " +
                "VALUES (N'{0}', N'{1}', N'{2}', {3}, NULL, NULL)",
                sender.ID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), reciever.ID, amount.ToString().Replace(',', '.'));
            new SqlCommand(sql, Connection).ExecuteNonQuery();
            Connection.Close();
        }


        /// <summary>
        /// Добавить в лог информацию о прерванном переводе между клиентами
        /// </summary>
        /// <param name="client">Отправитель</param>
        /// <param name="message">Сообщение ошибки</param>
        public void TransactionError(Client client, string message)
        {
            Connection.Open();
            string sql = string.Format("INSERT INTO Logs ([idClient], [time], [idReciever], [amount], [direction], [message]) " +
                "VALUES (N'{0}', N'{1}', NULL, NULL, NULL, N'{2}')",
                client.ID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
            new SqlCommand(sql, Connection).ExecuteNonQuery();
            Connection.Close();
        }
    }
}
