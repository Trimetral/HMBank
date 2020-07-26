using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml.Serialization;
using MainLibrary.Clients;

namespace dz13.Functions
{
    static class FunctionsMain
    {
        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <param name="Clients">База данных всех клиентов</param>
        /// <param name="window">Основное окно</param>
        public static void OpenFile(ref ObservableCollection<Client> Clients, MainWindow window)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML файлы (*.xml)|*.xml|JSON файлы (*.json)|*.json",
                InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory
            };

            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    string format = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('.'));
                    if (format == ".xml")
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Client>));
                        using (Stream reader = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            Clients = xmlSerializer.Deserialize(reader) as ObservableCollection<Client>;
                        }

                        window.dbList.ItemsSource = Clients;
                    }
                    else if (format == ".json")
                    {
                        Clients = JsonSerializer.Deserialize<ObservableCollection<Client>>(
                            File.ReadAllText(openFileDialog.FileName),
                            new JsonSerializerOptions()
                            {
                                WriteIndented = true,
                                Converters = { new ClientsConverter() }
                            });
                        window.dbList.ItemsSource = Clients;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки данных из файла\r\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Сохранение всех данных в файл
        /// </summary>
        /// <param name="Clients">База данных всех клиентов</param>
        public static void SaveFile(ObservableCollection<Client> Clients)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "XML файлы (*.xml)|*.xml|JSON файлы (*.json)|*.json",
                InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                string format = saveFileDialog.FileName.Substring(saveFileDialog.FileName.LastIndexOf('.'));
                if (format == ".xml")
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Client>));
                    using (Stream writer = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        xmlSerializer.Serialize(writer, Clients);
                    }

                    MessageBox.Show("Сохранение успешно");
                }
                else if (format == ".json")
                {
                    File.WriteAllText(saveFileDialog.FileName, JsonSerializer.Serialize<ObservableCollection<Client>>(
                        Clients,
                        new JsonSerializerOptions()
                        {
                            WriteIndented = true,
                            Converters = { new ClientsConverter() }
                        }));
                    MessageBox.Show("Сохранение успешно");
                }
            }
        }

        /// <summary>
        /// Заполнить данные физ. лица в окно
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="window">Основное окно</param>
        public static void FillPersonData(Person client, MainWindow window)
        {
            window.tbCType.Text = client.Vip ? "Тип: Физ. лицо (VIP)" : "Тип: Физ. лицо";
            window.tbCName.Text = $"Имя: {client.Name}";
            window.tbCSurname.Text = $"Фамилия: {client.ClientName}";
            window.tbCAdress.Text = $"Адрес: {client.Address}";
            window.tbCInvoice.Text = $"Прибыль: {client.Invoice.Earning.ToString("#.##")}";
            window.tbCAccount.Text = $"Состояние счёта: {client.Invoice}";
            window.tbCDeposit.Text = client.Deposit == null ? "Нет" : $"Счёт: {client.Deposit.Amount} под {client.Deposit.Percent}%";
            if (client.Deposit != null && client.Deposit.IsCapitilised) window.tbCDeposit.Text += " с капит.";
        }

        /// <summary>
        /// Заполнить данные юр. лица в окно
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="window">основное окно</param>
        public static void FillEntityData(Entity client, MainWindow window)
        { 
            window.tbEType.Text = "Тип: Юр. лицо";
            window.tbEName1.Text = $"Компания: {client.ClientName}";
            window.tbEName.Text = $"Имя директора: {client.DirName}";
            window.tbESurname.Text = $"Фамилия директора: {client.DirSurname}";
            window.tbEAdress.Text = $"Адрес: {client.Address}";
            window.tbEInvoice.Text = $"Прибыль: {client.Invoice.Earning.ToString("#.##")}";
            window.tbEAccount.Text = $"Состояние счёта: {client.Invoice}";
            window.tbEDeposit.Text = client.Deposit == null ? "Нет" : $"Счёт: {client.Deposit.Amount} под {client.Deposit.Percent}%";
            if (client.Deposit != null && client.Deposit.IsCapitilised) window.tbEDeposit.Text += " с капит.";
        }
    }
}
