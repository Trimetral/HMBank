using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System;
using AccountAdLibrary.Account;
using MainLibrary.Clients;
using dz13.Functions;

using static dz13.Functions.FunctionsMain;

namespace dz13
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Client> DBClients;
        EFManager EFManager;

        public MainWindow()
        {
            InitializeComponent();
            DBClients = new ObservableCollection<Client>();
            dbList.ItemsSource = DBClients;

            StartProgramm();
        }

        /// <summary>
        /// Начало программы с возможность выбора источника данных
        /// </summary>
        void StartProgramm()
        {
            if (MessageBox.Show("Использовать базу данных для программы?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    EFManager = new EFManager();
                    EFManager.FillData(DBClients);
                    MessageBox.Show("Подключение к базе данных успешно.", "Выполнено подключение", MessageBoxButton.OK, MessageBoxImage.Information);
                    bClear.IsEnabled = false;
                    bImport.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Подключиться к базе данных не удалось, программа будет работать без базы данных.\r\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"Программа будет работать без базы данных.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            SortList();
        }


        /// <summary>
        /// Очистить БД
        /// </summary>
        private void bClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите очистить всю базу данных клиентов?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                gridClient.Visibility = Visibility.Hidden;
                gridEntity.Visibility = Visibility.Hidden;
                DBClients.Clear();
            }
        }

        private void dbList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dbList.SelectedItem is Client)
            {
                (dbList.SelectedItem as Client).FillData(this);
                if (dbList.SelectedItem is Person)
                {
                    gridClient.Visibility = Visibility.Visible;
                    gridEntity.Visibility = Visibility.Hidden;
                }
                else
                {
                    gridClient.Visibility = Visibility.Hidden;
                    gridEntity.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Кнопка транзакций
        /// </summary>
        private void bTransaction_Click(object sender, RoutedEventArgs e)
        {
            Transaction transaction = new Transaction(dbList.SelectedItem as Client, DBClients, EFManager);
            transaction.ShowDialog();
            (dbList.SelectedItem as Client).FillData(this);
            
        }

        /// <summary>
        /// Кнопка добавления нового клиента
        /// </summary>
        private void bAddNewC_Click(object sender, RoutedEventArgs e)
        {
            AddNewClient addNewClient = new AddNewClient();
            addNewClient.ShowDialog();
            if (addNewClient.Ready)
            {
                addNewClient.Client.AddToDB(DBClients);
                if (EFManager != null) EFManager.AddNewClient(addNewClient.Client);
                MessageBox.Show("Новый клиент добавлен!");
            }
        }

        /// <summary>
        /// Кнопка управления вкладом
        /// </summary>
        private void bDepositTransaction_Click(object sender, RoutedEventArgs e)
        {
            Calculation calculation = new Calculation(dbList.SelectedItem as Client, EFManager);
            calculation.ShowDialog();
            (dbList.SelectedItem as Client).FillData(this);
        }

        /// <summary>
        /// Кнопка истории аккаунта клиента
        /// </summary>
        private void bHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History(dbList.SelectedItem as Client);
            history.Show();
        }

        /// <summary>
        /// Кнопка импорта данных в программу
        /// </summary>
        private void bImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFile(ref DBClients, this);
            SortList();
        }

        /// <summary>
        /// Кнопка экспорта данных в программу
        /// </summary>
        private void bExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFile(DBClients);
        }

        /// <summary>
        /// Отсортировать список клиентов по тому, являются ли они физ/юр лицами
        /// </summary>
        void SortList()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(dbList.ItemsSource); //применение сортировки
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("IsPerson");
            view.GroupDescriptions.Add(groupDescription);
        }

    }
}
