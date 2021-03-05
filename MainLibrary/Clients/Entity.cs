using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Clients
{
    /// <summary>
    /// Юр. лицо
    /// </summary>
    public class Entity : Client
    {
        /// <summary>
        /// Флаг для отображения в таблице
        /// </summary>
        public string IsPerson => "Юр. лицо";

        /// <summary>
        /// Имя директора
        /// </summary>
        public string DirName { get; set; }

        /// <summary>
        /// Фамилия директора
        /// </summary>
        public string DirSurname { get; set; }

        /// <summary>
        /// Юр. лицо
        /// </summary>
        /// <param name="envName">Название компании</param>
        /// <param name="dirName">Имя директора</param>
        /// <param name="dirSurname">Фамилия директора</param>
        /// <param name="account">Состояние счёта компании</param>
        /// <param name="invoice">Прибыль комании</param>
        /// <param name="address">Адрес компании</param>
        public Entity(string envName, string dirName, string dirSurname, decimal account, decimal invoice, string id, string address = "n/a") 
            : base(account, invoice, address, envName, id) => (DirName, DirSurname) = (dirName, dirSurname);


        /// <summary>
        /// Юр. лицо
        /// </summary>
        public Entity() : base(0, 0, "n/a", "entityName", "") => (DirName, DirSurname) = ("dirName", "dirSurname");

    }
}
