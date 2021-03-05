using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MainLibrary.Clients;
using AccountAdLibrary.Account;
using dz13.Functions;

namespace dz13
{
    /// <summary>
    /// Логика взаимодействия для Calculation.xaml
    /// </summary>
    public partial class Calculation : Window
    {
        Client Client { get; set; }
        EFManager EFManager { get; set; }

        public Calculation(Client client, EFManager efManager)
        {
            InitializeComponent();
            Client = client;
            tbDr.Text = "Год";
            EFManager = efManager;

            if (!client.Deposit.IsOpend) //если вклада нет
            {
                bWid.IsEnabled = false;
                bInc.IsEnabled = false;
                bCalculate.IsEnabled = false;
                cbCapit.IsEnabled = false;
                cbArg.IsEnabled = false;
            }
            else //есели вклад уже есть, то загрузить информацию о нём
            {
                bNew.Visibility = Visibility.Hidden;
                tbInv.Text = $"Баланс: {Client.Deposit.Amount:0.##}";
                cbCapit.IsChecked = client.Deposit.IsCapitilised;
                cbArg.ItemsSource = new List<double>() { client.Deposit.Percent };
                cbArg.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Рассчитать прибыль
        /// </summary>
        private void bCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbDuration.Text, out int duration))
            {
                if(duration < 1 || duration > 24)
                {
                    MessageBox.Show("Расчёт может быть проведён на промежуток от 1 до 24 временных отрезков!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                    listCalculation.ItemsSource = Client.Deposit.Calculate(duration);
            }
            else
                MessageBox.Show("Введите корректно промежуток времени!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void bWid_Click(object sender, RoutedEventArgs e)
        {
            Transaction transaction = new Transaction(Client, null, EFManager, true, true);
            transaction.ShowDialog();
            tbInv.Text = $"Баланс: {Client.Deposit.Amount:0.##}";
        }

        private void bInc_Click(object sender, RoutedEventArgs e)
        {
            Transaction transaction = new Transaction(Client, null, EFManager, true, false);
            transaction.ShowDialog();
            tbInv.Text = $"Баланс: {Client.Deposit.Amount:0.##}";
        }

        /// <summary>
        /// Открытие нового вклада
        /// </summary>
        private void bNew_Click(object sender, RoutedEventArgs e)
        {
            Client.Deposit.IsOpend = true;
            bNew.Visibility = Visibility.Hidden;
            bWid.IsEnabled = true;
            bInc.IsEnabled = true;
            bCalculate.IsEnabled = true;
            cbCapit.IsEnabled = true;
            cbArg.IsEnabled = true;
            tbInv.Text = "Баланс: 0";

            if (EFManager != null) EFManager.UpdateDeposit(Client);
        }

        private void cbCapit_Checked(object sender, RoutedEventArgs e)
        {
            tbDr.Text = "Месяц";
            Client.Deposit.IsCapitilised = true;
        }

        private void cbCapit_Unchecked(object sender, RoutedEventArgs e)
        {
            tbDr.Text = "Год";
            Client.Deposit.IsCapitilised = false;
        }

        /// <summary>
        /// Вычисление возможных процентных ставок
        /// </summary>
        private void cbArg_DropDownOpened(object sender, EventArgs e)
        {
            List<double> args = new List<double>();
            if (Client.Deposit.Amount >= 1000)
            {
                args.Add(3.27);
                if(Client is Person && (Client as Person).Vip) args.Add(3.47);
            }
            if (Client.Deposit.Amount >= 30000)
            {
                args.Add(3.8);
                if (Client is Person && (Client as Person).Vip) args.Add(3.95);
            }
            if (Client.Deposit.Amount >= 700000)
            {
                args.Add(4.85);
                if (Client is Person && (Client as Person).Vip) args.Add(5.1);
            }
            if (Client.Deposit.Amount >= 5_000_000)
            {
                args.Add(5.25);
                if (Client is Person && (Client as Person).Vip) args.Add(5.45);
            }
            cbArg.ItemsSource = args;
        }

        /// <summary>
        /// Изменение процента вклада при изменении
        /// </summary>
        private void cbArg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbArg.SelectedItem != null)
                Client.Deposit.Percent = (double)cbArg.SelectedItem;
        }

        private void bQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (EFManager != null)
                if (bNew.Visibility == Visibility.Hidden)
                {
                    EFManager.UpdateDeposit(Client);
                }
        }
    }
}
