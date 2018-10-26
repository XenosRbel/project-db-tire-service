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
    /// Логика взаимодействия для ArmorPage.xaml
    /// </summary>
    public partial class ArmorPage : Page
    {
        private readonly SolidColorBrush successColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66ff00"));
        private readonly SolidColorBrush errorColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#af2b1e"));

        /// <summary>
        /// Окно бронирования
        /// </summary>
        public ArmorPage()
        {
            InitializeComponent();
            GridRefresh();

            FillCustomerCmb();
            FillServicesCmb();
            FillStatusCmb();

            armorTable.PreviewKeyDown += ArmorTable_PreviewKeyDown;
        }

        private void ArmorTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                (armorTable.SelectedItem as Armor)?.Delete();
            }
            if (e.Key == Key.F5)
            {
                GridRefresh();
            }
        }

        private async void FillCustomerCmb()
        {
            List<Customers> customer = new List<Customers>();

            await Task.Run(() =>
            {
                 customer = new Customers().Load();                
            });

            for (int i = 0; i < customer.Count; i++)
            {
                cmbCustomers.Items.Add(customer[i].FioC);
            }
        }

        private async void FillServicesCmb()
        {
            List<Services> serv = new List<Services>();

            await Task.Run(() =>
            {
                serv = new Services().Load<Services>();
                
            });

            for (int i = 0; i < serv.Count; i++)
            {
                cmbServices.Items.Add(serv[i].NameService);
            }
        }

        private async void FillStatusCmb()
        {
            List<string> stats = new List<string>();
            await Task.Run(() =>
            {
                stats = Enum.GetNames(typeof(ArmorStatus)).ToList();
            });

            for (int i = 0; i < stats.Count; i++)
            {
                cmbStatus.Items.Add(stats[i]);
            }
        }

        private async void GridRefresh()
        {
            List<Armor> data = new List<Armor>();

            await Task.Run(() =>
            {
                data = new Armor().Load<Armor>();
            });

            armorTable.ItemsSource = data;
            armorTable.Items.Refresh();
        }

        public bool IsNulledDate(DatePicker datePicker)
        {
            if (datePicker.SelectedDate == null)
            {
                datePicker.BorderBrush = errorColor;

                return true;
            }

            return false;
        }

        public bool IsCorrectDate()
        {
            if (dateArrival.SelectedDate.Value <= dateExecute.SelectedDate.Value)
            {
                dateArrival.BorderBrush = successColor;
                dateExecute.BorderBrush = successColor;
                return true;
            }
            else
            {
                dateArrival.BorderBrush = errorColor;
                dateExecute.BorderBrush = errorColor;
                return false;
            }
        }

        private void btnAddArmorRec_Click(object sender, RoutedEventArgs e)
        {
            if (IsNulledDate(dateArrival) || IsNulledDate(dateExecute)) return;
            if (!IsCorrectDate()) return;
            if (cmbStatus.SelectedItem == null) return;
            if (cmbCustomers.SelectedItem == null) return;
            if (cmbServices.SelectedItem == null) return;

            Enum.TryParse(cmbStatus.SelectedItem.ToString(), out ArmorStatus statusValue);

            new Armor()
            {
                ArrivalDate = dateArrival.SelectedDate.Value,
                DateExecution = dateExecute.SelectedDate.Value,
                Customer = cmbCustomers.SelectedItem.ToString(),
                Service = cmbServices.SelectedItem.ToString(),
                StatusA = statusValue
            }.Insert();

            GridRefresh();
        }

        private void btnDelArmorRec_Click(object sender, RoutedEventArgs e)
        {
            (armorTable.SelectedItem as Armor)?.Delete();
            GridRefresh();
        }
    }
}
