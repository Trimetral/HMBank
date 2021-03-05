using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountAdLibrary.Account
{
    /// <summary>
    /// Событие для истории аккаунта
    /// </summary>
    public class Case
    {
        /// <summary>
        /// Время события
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Направлениее пополнения счёта
        /// </summary>
        public bool Direction { get; set; }

        /// <summary>
        /// Другой клиент
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Сумма перевода
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Ошибка
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Событие для истории аккаунта
        /// </summary>
        public Case() { }

        /// <summary>
        /// Перевод между клиентами
        /// </summary>
        /// <param name="clientName">Второй клиент</param>
        /// <param name="direction">true - перевод на этот счёт, false - перевод на счёт другого клиента</param>
        /// <param name="amount">Сумма перевода</param>
        public Case(string clientName, bool direction, decimal amount, DateTime time)
        {
            Time = time;
            ClientName = clientName;
            Direction = direction;
            Amount = amount;
        }

        /// <summary>
        /// Перевод между счетом и вкладом киента
        /// </summary>
        /// <param name="direction">true - на счёт, false - на вклад</param>
        /// <param name="amount">Сумма перевода</param>
        public Case(bool direction, decimal amount, DateTime time)
        {
            Time = time;
            Direction = direction;
            Amount = amount;
        }

        /// <summary>
        /// Ошибка перевода
        /// </summary>
        /// <param name="errorMsg">Текст ошибки</param>
        public Case(Exception exception, DateTime time)
        {
            Time = time;
            Message = exception.Message;
        }


        public override string ToString()
        {
            if (Message != null) return $"{Time:dd.MM.yyyy [HH:mm]} {Message}";
            if (ClientName != null)
            {
                if (Direction)
                    return $"{Time:dd.MM.yyyy [HH:mm]} пополнение счёта на {Amount:0.00} от {ClientName}";
                else
                    return $"{Time:dd.MM.yyyy [HH:mm]} списание со счёта на {Amount:0.00} на счёт {ClientName}";
            }
            else
            {
                if (Direction)
                    return $"{Time:dd.MM.yyyy [HH:mm]} пополнение счёта на {Amount:0.00} со счёта вклада";
                else
                    return $"{Time:dd.MM.yyyy [HH:mm]} списание со счёта на {Amount:0.00} на счёт вклада";
            }
        }
    }
}
