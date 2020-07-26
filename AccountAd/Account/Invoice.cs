using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountAdLibrary.Account
{
    /// <summary>
    /// Счёт клиента
    /// </summary>
    public class Invoice
    {

        decimal account;

        /// <summary>
        /// Состояние счёта клиента
        /// </summary>
        public decimal Account { get => account; set => account = value; }

        /// <summary>
        /// Прибыль клиента
        /// </summary>
        public decimal Earning { get; set; }

        /// <summary>
        /// Событие изменения счёта клиента
        /// </summary>
        public event Action<Case> AccountChanged;

        /// <summary>
        /// Счёт клиента
        /// </summary>
        public Invoice(decimal account, decimal earning)
        {
            Account = account;
            Earning = earning;
        }

        /// <summary>
        /// Счёт клиента
        /// </summary>
        public Invoice()
        {
            Account = 0;
            Earning = 0;
        }


        /// <summary>
        /// Снять с баланса аккаунта сумму
        /// </summary>
        /// <param name="sum">Сумма снятия</param>
        public void RemoveFromBalance(decimal sum, string clientName)
        {
            this.account -= sum;
            AccountChanged?.Invoke(new Case(clientName, false, sum));
        }

        /// <summary>
        /// Добавить средства на счёт
        /// </summary>
        /// <param name="sum">Сумма добавления</param>
        public void AddToBalance(decimal sum, string clientName)
        {
            this.account += sum;
            AccountChanged?.Invoke(new Case(clientName, true, sum));
        }


        /// <summary>
        /// Снять с баланса аккаунта сумму на вклад
        /// </summary>
        /// <param name="sum">Сумма снятия</param>
        public void RemoveFromBalance(decimal sum, Deposit deposit)
        {
            this.account -= sum;
            deposit.Amount += sum;
            AccountChanged?.Invoke(new Case(false, sum));
        }

        /// <summary>
        /// Добавить средства на счёт со вклада
        /// </summary>
        /// <param name="sum">Сумма добавления</param>
        public void AddToBalance(decimal sum, Deposit deposit)
        {
            this.account += sum;
            deposit.Amount -= sum;
            AccountChanged?.Invoke(new Case(true, sum));
        }

        public override string ToString() => Account.ToString("#.##");

    }
}
