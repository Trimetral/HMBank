using AccountAdLibrary.Account;
using ExceptionsLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace MainLibrary.Clients
{
    /// <summary>
    /// Клиент банка
    /// </summary>
    [XmlInclude(typeof(Person))]
    [XmlInclude(typeof(Entity))]
    public abstract class Client
    {
        string id;

        /// <summary>
        /// Индивидуальный ID для всех клиентов
        /// </summary>
        public string ID
        {
            get => id;
            set
            {
                if (ClientsIDs.Contains(id))
                    ClientsIDs.Remove(id);  //если это перезанзначение, то удалить прошлое значение
                ClientsIDs.Add(value);
                id = value;
            }
        }

        /// <summary>
        /// Адрес клиента
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Имя клиента (название компании)
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Вклад
        /// </summary>
        public Deposit Deposit { get; set; }

        /// <summary>
        /// История аккаунта клиента
        /// </summary>
        public Log Logs { get; set; }


        /// <summary>
        /// Счёт клиента
        /// </summary>
        public Invoice Invoice 
        { 
            get => invoice;
            set 
            {
                invoice = value;
                invoice.AccountChanged += Logs.AddToHistory;
                TransactionError += Logs.AddToHistory;
            } 
        }
        
        Invoice invoice;
                     
        /// <summary>
        /// Коллекция ID клиентов
        /// </summary>
        public static List<string> ClientsIDs { get; set; }

        static Client()
        {
            ClientsIDs = new List<string>();
        }


        /// <summary>
        /// Событие ошибки транзакции
        /// </summary>
        event Action<Case> TransactionError;

        /// <summary>
        /// Клиента банка
        /// </summary>
        /// <param name="account">Состояние счёта клиента</param>
        /// <param name="invoice">Прибыль клиента</param>
        /// <param name="address">Адрес клиента</param>
        /// <param name="name">Фамилия клиента (название компании)</param>
        public Client(decimal account, decimal invoice, string address, string name)
        {
            Address = address;
            ClientName = name;
            Logs = new Log();
            Invoice = new Invoice(account, invoice);

            while (true)
            {
                string newID = Guid.NewGuid().ToString().Substring(0, 5);
                if (ClientsIDs.Contains(newID)) continue;
                ID = newID;
                break;
            }
        }

        /// <summary>
        /// Перевести на данный счёт средства с другого счёта
        /// </summary>
        /// <typeparam name="T">Отправитель</typeparam>
        /// <param name="sum">Сумма</param>
        /// <param name="sender">Отправитель</param>
        /// <returns>Успешно ли прошла операция</returns>
        public bool TransToBalance<T>(decimal sum, T sender)
            where T : Client
        {
            if ((this is Person && sender is Entity) || (this is Entity && sender is Person))
            {
                sender.TransactionError?.Invoke(new Case(new BankTransactionException("Трансакция прервана: блокировака транзакции", 1)));
                return false;
            }
            if (sender.Invoice.Account < sum)
            {
                sender.TransactionError?.Invoke(new Case(new BankTransactionException("Трансакция прервана: недостаточно средств", 2)));
                return false;
            }
            if (sender == this)
            {
                sender.TransactionError?.Invoke(new Case(new BankTransactionException("Трансакция прервана: ошибка транзакции", 3)));
                return false;
            }

            this.Invoice.AddToBalance(sum * 0.99m, sender.ClientName); //комиссия 1%
            sender.Invoice.RemoveFromBalance(sum, this.ClientName);
            return true;
        }
        
        /// <summary>
        /// Транзакции с вкладом
        /// </summary>
        /// <param name="amount">Сумма транзакции</param>
        /// <param name="toDeposit">true - со счёта на вклад, fales - обратно</param>
        /// <returns>Успешно ли прошла операция</returns>
        public bool ManageDeposit(decimal amount, bool toDeposit)
        {
            if (toDeposit)
            {
                if (amount > this.Invoice.Account) throw new BankTransactionException("Ошибка перевода, сумма транзакции больше, чем средст на счету", 41);
                Deposit.Amount += amount;
                this.Invoice.RemoveFromBalance(amount, Deposit);
            }
            else
            {
                if (Deposit.Amount < amount) throw new BankTransactionException("Ошибка перевода, сумма транзакции больше, чем средст на счету", 42);
                Deposit.Amount -= amount;
                this.Invoice.AddToBalance(amount, Deposit);
            }
            return true;
        }

        public override string ToString() => ClientName;
    }
}