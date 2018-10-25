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
    /// Логика взаимодействия для MastersPage.xaml
    /// </summary>
    public partial class MastersPage : Page
    {
        /// <summary>
        /// Окно мастеов
        /// </summary>
        public MastersPage()
        {
            InitializeComponent();
            GridRefresh();
        }

        private async void GridRefresh()
        {
            List<Masters> data = new List<Masters>();
            await Task.Run(() =>
            {
                data = new Masters().Load<Masters>();
            });

            mastersTable.ItemsSource = data;
            mastersTable.Items.Refresh();
        }

        private void btnAddMasterRec_Click(object sender, RoutedEventArgs e)
        {
            new Masters()
            {
                FIO = textMaster.Text,
                Phone = textPhone.Text,
                Specialization = textSpec.Text
            }.Insert();

            GridRefresh();
        }

        private void btnDelMasterRec_Click(object sender, RoutedEventArgs e)
        {
            (mastersTable.SelectedItem as Masters)?.Delete();
            GridRefresh();
        }
    }
}
