using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Autoservice_Core.Entity;
using Project_DB_Tire_Service_Admin_Part.Entity;

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
            textPrice.PreviewTextInput += TextPrice_PreviewTextInput;
            servicesTable.PreviewKeyDown += ServicesTable_PreviewKeyDown;
            cmbRadius.Items.Add(1);
        }

        private void ServicesTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                (servicesTable.SelectedItem as ServicesWrapper)?.Delete();
            }
            if (e.Key == Key.F5)
            {
                GridRefresh();
            }
        }

        private void TextPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private async void GridRefresh()
        {
            List<ServicesWrapper> data = new List<ServicesWrapper>();

            await Task.Run(() => { data = new ServicesWrapper().Wrapper(); });

            servicesTable.ItemsSource = data;
            servicesTable.Items.Refresh();
        }

        private void btnAddServicesRec_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textPrice.Text)) return;
            if (string.IsNullOrWhiteSpace(textService.Text)) return;

            var img = GetImage();
           
            new Services()
            {
                NameService = this.textService.Text,
                ImageBytes = ImageToByteArray(img),
                Price = Convert.ToInt32(this.textPrice.Text),
                Radius = Convert.ToByte(this.cmbRadius.SelectedItem)
            }.Insert();

            GridRefresh();
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private System.Drawing.Image GetImage()
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            System.Drawing.Image img;
            if (imgURL != null)
            {
                img = System.Drawing.Image.FromFile(imgURL);
            }
            else
            {
                img = //Bitmap.FromFile(new Uri(@"").);
                    System.Drawing.Image.FromFile(directory + @"\error_no_image.png");
            }

            return img;
        }

        private void btnDelServicesRec_Click(object sender, RoutedEventArgs e)
        {
            (servicesTable.SelectedItem as ServicesWrapper)?.Delete();
            GridRefresh();
        }

        private void Page_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            var files = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            if (files != null)
            {
                imagePhoto.Source = new BitmapImage(new Uri(files[0]));
                imgURL = files[0];
            }
        }
    }
}
