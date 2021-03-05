using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountAdLibrary.Account;
using MainLibrary.Clients;
using MainLibrary;

namespace dz13.Functions
{
    public class EFManager
    {
        BankDBEntities BankDB { get; set; }
        public EFManager()
        {
            BankDB = new BankDBEntities();
        }

        /// <summary>
        /// Получить данные из базы данных и заполнить внутренню базу
        /// </summary>
        /// <param name="DBClients">Внутренняя бд</param>
        public void FillData(ObservableCollection<Client> DBClients)
        {
            #region Получение физ. лиц

            var personsRequest = BankDB.Clients.Join(
                BankDB.Persons,
                client => client.idClient,
                person => person.idClient,
                (client, person) => new
                {
                    client.idClient,
                    client.isPerson,
                    client.clientName,
                    client.adress,
                    client.account,
                    client.invoice,
                    person.cName,
                    person.isVip
                });

            foreach (var person in personsRequest)
            {
                DBClients.Add(ClientsFactory.CreateNewClient(
                    ClientsFactory.ClientType.Person,
                    (decimal)person.account,
                    (decimal)person.invoice,
                    person.adress,
                    person.clientName,
                    id: person.idClient,
                    personName: person.cName,
                    personVip: person.isVip
                    ));
            }

            #endregion

            #region Получение юр. лиц

            var entityRequest = BankDB.Clients.Join(
                BankDB.Entity,
                client => client.idClient,
                entity => entity.idClient,
                (client, entity) => new
                {
                    client.idClient,
                    client.isPerson,
                    client.clientName,
                    client.adress,
                    client.account,
                    client.invoice,
                    entity.dirName,
                    entity.dirSurname
                });

            foreach (var entity in entityRequest)
            {
                DBClients.Add(ClientsFactory.CreateNewClient(
                    ClientsFactory.ClientType.Entity,
                    (decimal)entity.account,
                    (decimal)entity.invoice,
                    entity.adress,
                    entity.clientName,
                    id: entity.idClient,
                    directorName: entity.dirName,
                    directorSurname: entity.dirSurname));
            }

            #endregion

            #region Вклады и логи

            var depositsRequest = BankDB.Deposits;

            foreach (var i in depositsRequest)
            {
                ManageClientsDeposit(DBClients, i.idClient,
                    new Deposit()
                    {
                        Amount = (decimal)i.amount,
                        IsCapitilised = i.isCapitalised,
                        Percent = i.percent,
                        IsOpend = i.isOpened
                    });
            }

            var logsRequest = BankDB.Logs.SelectMany(
                log => BankDB.Clients.Where(client => client.idClient == log.idClient).DefaultIfEmpty(),
                (log, client) => new
                {
                    log.idLog,
                    idSender = log.idClient,
                    log.idReciever,
                    log.message,
                    log.time,
                    log.amount,
                    log.direction,
                    senderName = client.clientName
                })
                .SelectMany(
                log => BankDB.Clients.Where(client => client.idClient == log.idReciever).DefaultIfEmpty(),
                (log, client) => new
                {
                    log.idLog,
                    log.idSender,
                    log.idReciever,
                    log.message,
                    log.time,
                    log.amount,
                    log.direction,
                    log.senderName,
                    recieverName = client.clientName
                });


            foreach (var i in logsRequest)
            {
                if (i.message != null)
                {
                    ManageClientsLogs(DBClients, i.idSender, new Case(new Exception(i.message), i.time));
                }
                else if (i.idReciever == null)
                {
                    ManageClientsLogs(DBClients, i.idSender, new Case(i.direction == false, (decimal)i.amount, i.time));
                }
                else
                {
                    ManageClientsLogs(DBClients, i.idSender, new Case(i.recieverName, false, (decimal)i.amount, i.time));
                    ManageClientsLogs(DBClients, i.idReciever, new Case(i.senderName, true, (decimal)i.amount, i.time));
                }
            }

            #endregion
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
        private static void ManageClientsLogs(ObservableCollection<Client> DBClients, string id, Case clientcase)
        {
            for (int i = 0; i < DBClients.Count; i++)
            {
                if (DBClients[i].ID == id)
                {
                    DBClients[i].Logs.Cases.Add(clientcase);
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
            BankDB.Clients.Add(new Clients()
            {
                adress = client.Address,
                clientName = client.ClientName,
                invoice = (double)client.Invoice.Earning,
                account = (double)client.Invoice.Account,
                idClient = client.ID,
                isPerson = client is Person
            });

            BankDB.Deposits.Add(new Deposits()
            {
                idClient = client.ID,
                amount = 0,
                percent = 0,
                isCapitalised = false,
                isOpened = false
            });

            if(client is Person)
            {
                Person p = client as Person;
                BankDB.Persons.Add(new Persons()
                {
                    cName = p.Name,
                    idClient = p.ID,
                    isVip = p.Vip
                });
            }
            else
            {
                MainLibrary.Clients.Entity e = client as MainLibrary.Clients.Entity;
                BankDB.Entity.Add(new Entity()
                {
                    dirName = e.DirName,
                    dirSurname = e.DirSurname,
                    idClient = e.ID
                });
            }

            BankDB.SaveChanges();
        }


        /// <summary>
        /// Обновление информации о вкладе и счёте клиента
        /// </summary>
        public void UpdateDeposit(Client client)
        {
            var depositRequest = BankDB.Deposits.Where(n => n.idClient == client.ID).Take(1);

            foreach (var dep in depositRequest)
            {
                dep.amount = (double)client.Deposit.Amount;
                dep.isCapitalised = client.Deposit.IsCapitilised;
                dep.percent = client.Deposit.Percent;
                dep.isOpened = client.Deposit.IsOpend;
            }

            var clientsRequest = BankDB.Clients.Where(n => n.idClient == client.ID);

            foreach (var c in clientsRequest)
                c.account = (double)client.Invoice.Account;

            BankDB.SaveChanges();
        }


        /// <summary>
        /// Добавить в лог сведения о том, что был перевод между счетами одного клиента
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="amount">Сумма перевода</param>
        /// <param name="toDeposit">true - на основной счёт, false - на счёт вклада</param>
        public void UpdateAccountDepositLog(Client client, decimal amount, bool toDeposit)
        {
            BankDB.Logs.Add(new Logs()
            {
                idClient = client.ID,
                time = DateTime.Now,
                amount = (double)amount,
                direction = toDeposit
            });

            BankDB.SaveChanges();
        }


        /// <summary>
        /// Добавить в лог информацию об успешном переводе между разными клиентами
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="reciever">Получатель</param>
        /// <param name="amount">Сумма</param>
        public void TransactionSucessful(Client sender, Client reciever, decimal amount)
        {
            var send = BankDB.Clients.Where(n => n.idClient == sender.ID);
            foreach (var i in send) i.account = (double)sender.Invoice.Account;

            var rec = BankDB.Clients.Where(n => n.idClient == reciever.ID);
            foreach (var i in rec) i.account = (double)reciever.Invoice.Account;

            BankDB.Logs.Add(new Logs()
            {
                amount = (double)amount,
                idClient = sender.ID,
                idReciever = reciever.ID,
                time = DateTime.Now
            });

            BankDB.SaveChanges();
        }


        /// <summary>
        /// Добавить в лог информацию о прерванном переводе между клиентами
        /// </summary>
        /// <param name="client">Отправитель</param>
        /// <param name="message">Сообщение ошибки</param>
        public void TransactionError(Client client, string message)
        {
            BankDB.Logs.Add(new Logs()
            {
                idClient = client.ID,
                time = DateTime.Now,
                message = message
            });

            BankDB.SaveChanges();
        }

    }
}
