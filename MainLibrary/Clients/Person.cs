using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Clients
{
    /// <summary>
    /// Физ. лицо
    /// </summary>
    public class Person : Client
    {
        /// <summary>
        /// Флаг для отображения в таблице
        /// </summary>
        public string IsPerson => "Физ. лицо";

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ВИП статус
        /// </summary>
        public bool Vip { get; set; }

        /// <summary>
        /// Физ. лицо
        /// </summary>
        /// <param name="pName">Имя клиента</param>
        /// <param name="account">Состояние счёта клиента</param>
        /// <param name="invoice">Прибыль клиента</param>
        /// <param name="pSurname">Фамилия клиента</param>
        /// <param name="address">Адрес клиента</param>
        /// <param name="vip">ВИП статус</param>
        public Person(string pName, decimal account, decimal invoice, string id, string pSurname = "PersonSurname", string address = "n/a", bool vip = false)
            : base(account, invoice, address, pSurname, id) => (Name, Vip) = (pName, vip);

        /// <summary>
        /// Физ. лицо
        /// </summary>
        public Person() : base (0, 0, "n/a", "PersonSurname", "") => (Name, Vip) = ("PersonName", false);

    }
}
