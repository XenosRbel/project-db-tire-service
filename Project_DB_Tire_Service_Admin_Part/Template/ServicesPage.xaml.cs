using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        private string imgURL;

        /// <summary>
        /// Услуги
        /// </summary>
        public ServicesPage()
        {
            InitializeComponent();

            GridRefresh();
        }

        private void GridRefresh()
        {
            servicesTable.ItemsSource = new Services().Load<Services>();
            servicesTable.Items.Refresh();
        }

        private void btnAddServicesRec_Click(object sender, RoutedEventArgs e)
        {
            var decoderPath = ((BitmapFrame)imagePhoto.Source).Decoder.ToString();

            Regex regex = new Regex("(component.*|.png|.jpg)");

            var imgPath = regex.Match(decoderPath).Value.Replace("component", "");
            var img = new BitmapImage(new Uri(decoderPath));
            new Services()
            {
                NameService = this.textService.Text,
                PhotoDetails = img,
                Price = Convert.ToInt32(this.textPrice.Text),
                Radius = Convert.ToByte(this.cmbRadius.SelectedItem)
            }.Insert();

            GridRefresh();
        }

        private void btnDelServicesRec_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop, true);

                imagePhoto.Source = new BitmapImage(new Uri(files[0]));
            }
        }
    }
}
