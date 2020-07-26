using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountAdLibrary.Account
{
    /// <summary>
    /// История аккаунта клиента
    /// </summary>
    public class Log
    {
        /// <summary>
        /// История
        /// </summary>
        public ObservableCollection<Case> Cases { get; set; }

        /// <summary>
        /// Добавить в историю событие
        /// </summary>
        /// <param name="case">Новое событие</param>
        public void AddToHistory (Case @case)
        {
            Cases.Add(@case);
        }

        /// <summary>
        /// История аккаунта клиента
        /// </summary>
        public Log()
        {
            Cases = new ObservableCollection<Case>();
        }

    }
}
