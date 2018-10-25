﻿using System;
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
        private readonly SolidColorBrush successColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66ff00"));
        private readonly SolidColorBrush errorColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#af2b1e"));
        /// <summary>
        /// Заказы
        /// </summary>
        public OrdersPage()
        {
            InitializeComponent();

            GridRefresh();
            FillCustomerCmb();
            FillMasterCmb();
            FillServicesCmb();

            textCount.PreviewTextInput += TextCount_PreviewTextInput;
        }

        public bool IsNulledDate(DatePicker datePicker)
        {
            if (datePicker.SelectedDate == null)
            {
                datePicker.BorderBrush = errorColor;

                return true;
            }
            else
            {
                datePicker.BorderBrush = successColor;
            }

            return false;
        }

        private void TextCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private async void FillCustomerCmb()
        {
            List<Customers> customer = new List<Customers>();

            await Task.Run(() => {
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

        private async void FillMasterCmb()
        {
            List<Masters> master = new List<Masters>();

            await Task.Run(() =>
            {
                master = new Masters().Load<Masters>();
            });


            for (int i = 0; i < master.Count; i++)
            {
                cmbMasters.Items.Add(master[i].FIO);
            }
        }

        private async void GridRefresh()
        {
            List<Orders> data = new List<Orders>();

            await Task.Run(() =>
            {
                data = new Orders().Load<Orders>();
            });

            ordersTable.ItemsSource = data;
            ordersTable.Items.Refresh();
        }

        private void btnAddOrderRec_Click(object sender, RoutedEventArgs e)
        {
            if (IsNulledDate(dateOrder)) return;
            if (cmbServices.SelectedItem == null) return;
            if (cmbCustomers.SelectedItem == null) return;
            if (cmbMasters.SelectedItem == null) return;
            if (string.IsNullOrWhiteSpace(textCount.Text)) return;

            new Orders()
            {
                CountO = Convert.ToInt32(textCount.Text),
                IdCustomer = cmbCustomers.SelectedItem.ToString(),
                IdMaster = cmbMasters.SelectedItem.ToString(),
                IdServices = cmbServices.SelectedItem.ToString(),
                OrderDate = dateOrder.SelectedDate.Value
            }.Insert();

            GridRefresh();
        }

        private void btnDelOrderRec_Click(object sender, RoutedEventArgs e)
        {
            (ordersTable.SelectedItem as Orders)?.Delete();
            GridRefresh();
        }
    }
}
