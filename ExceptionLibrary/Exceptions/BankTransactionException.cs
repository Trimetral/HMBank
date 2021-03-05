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
        /// Ошибка транзакции
        /// </summary>
        /// <param name="message">Сообщение ошибки</param>
        public BankTransactionException(string message) : base(message)
        {
            
        }
    }
}
