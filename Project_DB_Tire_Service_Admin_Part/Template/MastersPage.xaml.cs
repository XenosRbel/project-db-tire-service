using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Autoservice_Core.Entity;

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

            mastersTable.PreviewKeyDown += MastersTable_PreviewKeyDown;
        }

        private void MastersTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                (mastersTable.SelectedItem as Masters)?.Delete();
            }
            if (e.Key == Key.F5)
            {
                GridRefresh();
            }
        }

        private async void GridRefresh()
        {
            List<Masters> data = new List<Masters>();
            await Task.Run(() =>
            {
                data = (List<Masters>)new Masters().Select();
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
