using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Project_DB_Tire_Service_Admin_Part
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsOpen = false;

        /// <summary>
        /// Главное окно
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void splitMenu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = ((ListView) sender).SelectedIndex;

            switch (item)
            {
                case 0:
                    {
                        myFrame.Navigate(new Template.CustomersPage());
                        break;
                    }
                case 1:
                    {
                        myFrame.Navigate(new Template.MastersPage());
                        break;
                    }
                case 2:
                    {
                        myFrame.Navigate(new Template.ServicesPage());
                        break;
                    }
                case 3:
                    {
                        myFrame.Navigate(new Template.OrdersPage());
                        break;
                    }
                case 4:
                    {
                        myFrame.Navigate(new Template.ArmorPage());
                        break;
                    }
                case 5:
                    {
                        //myFrame.Navigate(new Template.CustomersPage());
                        break;
                    }
                default:
                    break;
            }

            CloseMenu();
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenMenu();
        }

        private void OpenMenu()
        {
            Storyboard sb = this.FindResource("OpenMenu") as Storyboard;

            if (!IsOpen)
            {
                sb.Begin();

                IsOpen = true;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            CloseMenu();
        }

        private void CloseMenu()
        {
            Storyboard sb = this.FindResource("CloseMenu") as Storyboard;

            if (IsOpen)
            {
                sb.Begin();

                IsOpen = false;
            }
        }
    }
}
