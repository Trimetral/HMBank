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

namespace dz13
{
    /// <summary>
    /// Логика взаимодействия для AddNewClient.xaml
    /// </summary>
    public partial class AddNewClient : Window
    {
        /// <summary>
        /// Сформированный клиент для добавления в базу
        /// </summary>
        public Clients.Client Client { get; set; }

        /// <summary>
        /// Флаг того, что создание нового клиента было успешно
        /// </summary>
        public bool Ready { get; set; } = false;

        public AddNewClient()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (tabClient.SelectedIndex == 0) //физ.лицо
            {
                string name = tbCName.Text.Trim(),
                    surname = tbCSurname.Text.Trim(),
                    adress = tbCAdress.Text.Trim();

                if (name.Length == 0)
                {
                    MessageBox.Show("Введите имя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (surname.Length == 0)
                {
                    MessageBox.Show("Введите фамилию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (adress.Length == 0)
                {
                    MessageBox.Show("Введите адрес!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(!decimal.TryParse(tbCInvoice.Text, out decimal invoice) || invoice <= 0)
                {
                    MessageBox.Show("Введите корректно прибыль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(tbCAccount.Text, out decimal account))
                {
                    MessageBox.Show("Введите корректно состояние счёта!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Client = new Clients.Person(name, account, invoice, surname, adress, cbIsVIP.IsChecked == true ? true : false);
                Ready = true;
                Close();
            }

            if (tabClient.SelectedIndex == 1) //юр.лицо
            {
                string name = tbEName.Text.Trim(),
                    surname = tbESurname.Text.Trim(),
                    adress = tbEAdress.Text.Trim(),
                    cname = tbEName1.Text.Trim();

                if (name.Length == 0)
                {
                    MessageBox.Show("Введите имя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (surname.Length == 0)
                {
                    MessageBox.Show("Введите фамилию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (adress.Length == 0)
                {
                    MessageBox.Show("Введите адрес!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cname.Length == 0)
                {
                    MessageBox.Show("Введите название компании!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(tbEInvoice.Text, out decimal invoice) || invoice <= 0)
                {
                    MessageBox.Show("Введите корректно прибыль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(tbEAccount.Text, out decimal account))
                {
                    MessageBox.Show("Введите корректно состояние счёта!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Client = new Clients.Entity(cname, name, surname, account, invoice, adress);
                Ready = true;
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
