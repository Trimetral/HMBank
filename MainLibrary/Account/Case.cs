using MainLibrary.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Account
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
        /// Сообщение (об ошибке)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Событие для истории аккаунта
        /// </summary>
        public Case() { }

        /// <summary>
        /// Перевод между клиентами
        /// </summary>
        /// <param name="client">Второй клиент</param>
        /// <param name="direction">true - перевод на этот счёт, false - перевод на счёт другого клиента</param>
        /// <param name="amount">Сумма перевода</param>
        public Case(Client client, bool direction, decimal amount)
        {
            Time = DateTime.Now;
            ClientName = client.ClientName;
            Direction = direction;
            Amount = amount;
        }

        /// <summary>
        /// Перевод между счетом и вкладом киента
        /// </summary>
        /// <param name="direction">true - на счёт, false - на вклад</param>
        /// <param name="amount">Сумма перевода</param>
        public Case(bool direction, decimal amount)
        {
            Time = DateTime.Now;
            Direction = direction;
            Amount = amount;
        }

        /// <summary>
        /// Ошибка перевода
        /// </summary>
        /// <param name="errorMsg">Текст ошибки</param>
        public Case(string errorMsg)
        {
            Time = DateTime.Now;
            Message = errorMsg;
        }


        public override string ToString()
        {
            if (Message != null) return $"{Time.ToString("dd.MM.yyyy [HH:mm]")} {Message}";
            if (ClientName != null)
            {
                if (Direction)
                    return $"{Time.ToString("dd.MM.yyyy [HH:mm]")} пополнение счёта на {Amount} от {ClientName}";
                else
                    return $"{Time.ToString("dd.MM.yyyy [HH:mm]")} списание со счёта на {Amount} на счёт {ClientName}";
            }
            else
            {
                if (Direction)
                    return $"{Time.ToString("dd.MM.yyyy [HH:mm]")} пополнение счёта на {Amount} со счёта вклада";
                else
                    return $"{Time.ToString("dd.MM.yyyy [HH:mm]")} списание со счёта на {Amount} на счёт вклада";
            }
        }
    }
}
