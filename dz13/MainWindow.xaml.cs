using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
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

        public MainWindow()
        {
            InitializeComponent();
            DBClients = new ObservableCollection<Client>();
            dbList.ItemsSource = DBClients;
            //Task.Delay(5000).ContinueWith(_ => sometest(this.Dispatcher));
        }

        void CreateSQLManager()
        {
            var sqlCon = new SqlConnectionStringBuilder()
            {

            }
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

        /// <summary>
        /// Инморт данных
        /// </summary>
        private void bImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFile(ref DBClients, this);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(dbList.ItemsSource); //применение сортировки
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("isPerson");
            view.GroupDescriptions.Add(groupDescription);
        }

        /// <summary>
        /// Экспорт данных
        /// </summary>
        private void bExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFile(DBClients);
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
            Transaction transaction = new Transaction(dbList.SelectedItem as Client, DBClients);
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
                MessageBox.Show("Новый клиент добавлен!");
            }
        }

        /// <summary>
        /// Кнопка управления вкладом
        /// </summary>
        private void bDepositTransaction_Click(object sender, RoutedEventArgs e)
        {
            Calculation calculation = new Calculation(dbList.SelectedItem as Client);
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


        //public static void sometest(Dispatcher window) 
        //{
        //    window.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { DBClients.Add(new Person()); }));
        //}
    }
}
