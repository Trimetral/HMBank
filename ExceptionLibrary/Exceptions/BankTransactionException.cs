using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsLibrary.Exceptions
{
    /// <summary>
    /// Ошибка транзакции
    /// </summary>
    public class BankTransactionException : Exception
    {
        /// <summary>
        /// Код ошибки
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Ошибка транзакции
        /// </summary>
        /// <param name="message">Сообщение ошибки</param>
        /// <param name="code">Код ошибки</param>
        public BankTransactionException(string message, int code) : base(message)
        {
            ErrorCode = code;
        }
    }
}
