using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz13.Clients
{
    /// <summary>
    /// Юр. лицо
    /// </summary>
    public class Entity : Client
    {
        public string isPerson { get => "Юр. лицо"; }

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
        public Entity(string envName, string dirName, string dirSurname, decimal account, decimal invoice, string address = "n/a") : base(account, invoice, address, envName)
        {
            this.DirName = dirName;
            this.DirSurname = dirSurname;
        }

        /// <summary>
        /// Юр. лицо
        /// </summary>
        public Entity() : base(0, 0, "n/a", "entityName")
        {
            this.DirName = "dirName";
            this.DirSurname = "dirSurname";
        }

    }
}
