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
using Project_DB_Tire_Service_Admin_Part.Tables;

namespace Project_DB_Tire_Service_Admin_Part.Template
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        /// <summary>
        /// Заказы
        /// </summary>
        public OrdersPage()
        {
            InitializeComponent();

            ordersTable.ItemsSource = new Orders().Load<Orders>();
            ordersTable.Items.Refresh();
        }
    }
}
