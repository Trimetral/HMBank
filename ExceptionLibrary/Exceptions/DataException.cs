using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsLibrary.Exceptions
{
    /// <summary>
    /// Ошибка заполнения данных
    /// </summary>
    public class DataException : Exception
    {
        /// <summary>
        /// Ошибка заполнения данных
        /// </summary>
        /// <param name="param">Параметр с ошибкой</param>
        public DataException(string param) : base($"Ошибка заполнения данных: {param}")
        {

        }
    }
}
