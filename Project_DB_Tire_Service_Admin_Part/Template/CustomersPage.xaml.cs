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

            customersTable.ItemsSource = new Customers().Load();
            customersTable.Items.Refresh();
        }

        private void customersTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                (customersTable.SelectedItem as Customers).DeleteCustomer();
            }
            if (e.Key == Key.LeftCtrl)
            {
                customersTable.CommitEdit();

                ((sender as DataGrid).SelectedItem as Customers).UpdateCustomer();
            }
            if (e.Key == Key.F5)
            {
                customersTable.ItemsSource = new Customers().Load();
                customersTable.Items.Refresh();
            }
        }
    }
}
