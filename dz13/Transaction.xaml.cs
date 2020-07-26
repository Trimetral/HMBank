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
using System.Windows.Shapes;
using MainLibrary.Clients;

namespace dz13
{
    /// <summary>
    /// Логика взаимодействия для Transaction.xaml
    /// </summary>
    public partial class Transaction : Window
    {
        Client Sender { get; set; }
        Client Reciever { get; set; }
        ObservableCollection<Client> DB { get; set; }

        bool isLocal, toDeposit;


        /// <summary>
        /// Откно перевода средств
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="clients">Возможные получатели</param>
        /// <param name="local">Если перевод связан со вкладом</param>
        /// <param name="toDeposit">true - перевод со счёта на вклад, false - обратно</param>
        public Transaction(Client sender, ObservableCollection<Client> clients, bool local = false, bool toDeposit = true)
        {
            InitializeComponent();
            isLocal = local;
            this.toDeposit = toDeposit;
            Sender = sender;

            if (isLocal)
            {
                cbReciever.Visibility = Visibility.Hidden;
                if (toDeposit)
                {
                    lSender.Content = "Со счёта";
                    lReciever.Content = "На вклад";
                    slAmount.Maximum = (double)Sender.Invoice.Account;
                }
                else
                {
                    lSender.Content = "С вклада";
                    lReciever.Content = "На счёт";
                    slAmount.Maximum = (double)Sender.Deposit.Amount;
                }
            }
            else
            {
                DB = clients;
                cbReciever.ItemsSource = DB.Where(n => n != sender);
                tbSender.Text = sender.ToString();
                slAmount.Maximum = (double)Sender.Invoice.Account;
            }
        }

        private void tbAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (slAmount == null) return;

            if(decimal.TryParse(tbAmount.Text, out decimal amount))
            {
                if (amount > (decimal)slAmount.Maximum) amount = (decimal)slAmount.Maximum;
                slAmount.Value = (double)amount;
            }
        }

        private void slAmount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbAmount.Text = slAmount.Value.ToString("#.##");
        }

        private void bAccept_Click(object sender, RoutedEventArgs e)
        {
            if (slAmount.Value == 0) return;

            if (isLocal)    //перевод между вкладом и счётом
                if (MessageBox.Show($"Вы уверены, что хотите совершить данную операцию?", "Подтверждение перевода", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Sender.ManageDeposit((decimal)slAmount.Value, toDeposit);
                        MessageBox.Show("Перевод выполнен!", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    Close();
                }
                else
                {
                    return;
                }

            if (cbReciever.SelectedIndex == -1) return;

            //перевод между клиентами
            if (MessageBox.Show($"Вы уверены, что хотите перевести {slAmount.Value.ToString("#.##")} на счёт {Reciever.ClientName}?\r\nКомиссия 1%", "Подтверждение перевода", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(Reciever.TransToBalance((decimal)slAmount.Value, Sender))
                {
                    MessageBox.Show("Перевод выполнен!", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка перевода!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Close();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cbReciever_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reciever = cbReciever.SelectedItem as Client;
        }
    }
}
