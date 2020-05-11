using dz13.Clients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            //DataBase.DBClients = DBClients;
            if (dbList.SelectedItem is Client)
            {
                if(dbList.SelectedItem is Person)
                {
                    gridClient.Visibility = Visibility.Visible;
                    gridEntity.Visibility = Visibility.Hidden;
                    FillPersonData(dbList.SelectedItem as Person, this);
                }
                else
                {
                    gridClient.Visibility = Visibility.Hidden;
                    gridEntity.Visibility = Visibility.Visible;
                    FillEntityData(dbList.SelectedItem as Entity, this);
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
            if (dbList.SelectedItem is Person)
                FillPersonData(dbList.SelectedItem as Person, this);
            else
                FillEntityData(dbList.SelectedItem as Entity, this);
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
                DBClients.Add(addNewClient.Client);
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
            if (dbList.SelectedItem is Person)
                FillPersonData(dbList.SelectedItem as Person, this);
            else
                FillEntityData(dbList.SelectedItem as Entity, this);
        }

        /// <summary>
        /// Кнопка истории аккаунта клиента
        /// </summary>
        private void bHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History(dbList.SelectedItem as Client);
            history.Show();
        }
    }
}
