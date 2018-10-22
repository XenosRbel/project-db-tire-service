using Project_DB_Tire_Service_Admin_Part.Tables;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void GridRefresh()
        {
            customersTable.ItemsSource = new Customers().Load();
            customersTable.Items.Refresh();
        }

        private void CustomersTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                (customersTable.SelectedItem as Customers).DeleteCustomer();
            }
            if (e.Key == Key.F5)
            {
                GridRefresh();
            }
        }

        private void btnAddCustomersRec_Click(object sender, RoutedEventArgs e)
        {
            new Customers() {
                FioC = this.textCustomer.Text,
                Email = this.textEmail.Text,
                Phone = this.textPhone.Text
            }.Insert();

            GridRefresh();
        }

        private void btnDelCustomersRec_Click(object sender, RoutedEventArgs e)
        {
            (customersTable.SelectedItem as Customers).DeleteCustomer();
            GridRefresh();
        }
    }
}
