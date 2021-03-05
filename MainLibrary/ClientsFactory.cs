using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MainLibrary.Clients;

namespace MainLibrary
{
    public static class ClientsFactory
    {
        public enum ClientType { Person, Entity }

        /// <summary>
        /// Создание нового клиента, тип может быть Person или Entity.
        /// Для Person необходимо заполнить personName и personVip.
        /// Для Entity необходимо заполнить directorName и directorSurname.
        /// ID заполняется только в том случае, если оно заранее известо и требуется его сохранить.
        /// </summary>
        /// <param name="type">Person (физ.лицо) или Entity (юр.лицо)</param>
        /// <param name="account">Состояние счёта клиента</param>
        /// <param name="invoice">Доход клиента</param>
        /// <param name="adress">Адрес клиента</param>
        /// <param name="clientName">Для физ.лица - фамилия, для юр.лица - название компании</param>
        /// <param name="id">ID клиента</param>
        /// <param name="personName">Имя физ.лица</param>
        /// <param name="personVip">Вип-статус физ.лица</param>
        /// <param name="directorName">Имя директора юр.лица</param>
        /// <param name="directorSurname">Фамилия юр.лица</param>
        /// <returns></returns>
        public static Client CreateNewClient(ClientType type, decimal account, decimal invoice, string adress, string clientName, string id = "",
            string personName = "n/a", bool personVip = false, string directorName = "n/a", string directorSurname = "n/a")
        {
            switch (type)
            {
                case ClientType.Person: return new Person(personName, account, invoice, id, clientName, adress, personVip);
                case ClientType.Entity: return new Entity(clientName, directorName, directorSurname, account, invoice, id, adress);
                default: return new NullClient();
            }
        }
    }
}
