using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Autoservice_Core.Entity;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Project_DB_Tire_Service_Admin_Part.Template
{
    /// <summary>
    /// Логика взаимодействия для CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        /// <summary>
        /// Окно клиентов
        /// </summary>
        public CustomersPage()
        {
            InitializeComponent();
            GridRefresh();
            textEmail.TextChanged += Text_TextChanged;
            textCustomer.TextChanged += Text_TextChanged;
            textPhone.TextChanged += Text_TextChanged;
        }

        private async void GridRefresh()
        {
            List<Customers> data = new List<Customers>();
            await Task.Run(() => { data = (List<Customers>)new Customers().Select(); });

            customersTable.ItemsSource = data;
            customersTable.Items.Refresh();
        }

        private void CustomersTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                (customersTable.SelectedItem as Customers)?.DeleteCustomer();
            }
            if (e.Key == Key.F5)
            {
                GridRefresh();
            }
        }

        private void btnAddCustomersRec_Click(object sender, RoutedEventArgs e)
        {
            var entity = new Customers()
            {
                FioC = this.textCustomer.Text,
                Email = this.textEmail.Text,
                Phone = this.textPhone.Text
            };

            if (!IsValidInput(entity))
            {
                return;
            }

            entity.Insert();
            GridRefresh();

            ClearFields();
        }

        private void ClearFields()
        {
            this.textCustomer.Text = null;
            this.textEmail.Text = null;
            this.textPhone.Text = null;
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entity = new Customers()
            {
                FioC = this.textCustomer.Text,
                Email = this.textEmail.Text,
                Phone = this.textPhone.Text
            };

            IsValidInput(entity);
        }

        private bool IsValidInput(Customers entity)
        {
            var context = new ValidationContext(entity);
            var results = new List<ValidationResult>();
            var errorMark = true;

            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                textPhone.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#af2b1e"));
                textEmail.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#af2b1e"));
                errorMark = false;
            }
            else
            {
                textPhone.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66ff00"));
                textEmail.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66ff00"));
            }

            if (string.IsNullOrWhiteSpace(textCustomer.Text))
            {
                textCustomer.BorderBrush = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#af2b1e"));
                errorMark = false;
            }
            else
            {
                textCustomer.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66ff00"));
            }

            return errorMark;
        }

        private void btnDelCustomersRec_Click(object sender, RoutedEventArgs e)
        {
            (customersTable.SelectedItem as Customers)?.DeleteCustomer();
            GridRefresh();
        }
    }
}
