using MainLibrary.Clients;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static dz13.Functions.FunctionsMain;

namespace dz13.Functions
{
    public static class Extensions
    {

        /// <summary>
        /// Добавить клиента в базу
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="clients">База данных</param>
        static public void AddToDB(this Client client, ObservableCollection<Client> clients)
        {
            clients.Add(client);
        }

        /// <summary>
        /// Заполнения данных в форму
        /// </summary>
        /// <param name="client">Выделенный клиент</param>
        /// <param name="window">Главное окно приложения</param>
        static public void FillData(this Client client, MainWindow window)
        {
            if (client is Person)
                FillPersonData(client as Person, window);
            else
                FillEntityData(client as Entity, window);
        }
    }
}
