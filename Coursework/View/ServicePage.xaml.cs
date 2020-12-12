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
using System.Data.Entity;
using System.Collections.ObjectModel;
using Coursework.Entities;
using System.Drawing;
using System.Globalization;
using Coursework.View.AddAndEditWindows;
using Coursework.Methods;

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        private ObservableCollection<Service> services;
        DatabaseContext _context = new DatabaseContext();
        public ServicePage()
        {
            InitializeComponent();
            Background = BackgroundColor.Colors();
            _context.Services.Load();
            //var servicesBindingList = _context.Services.Local.ToBindingList();
            var servicesList = _context.Services.Local.Where(c => c.Delete == false).ToList();

            services = new ObservableCollection<Service>(servicesList);

            PriceDataGrid.ItemsSource = services;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddServiceWindow addServiceWindow = new AddServiceWindow(_context);
            var reuslt = addServiceWindow.ShowDialog();
            if(reuslt == true)
            {
                services.Add(_context.Services.OrderByDescending(c => c.ID).FirstOrDefault());
            }
        }

        private void ServiceName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextInfo cultureInfo = new CultureInfo("ru-RU").TextInfo;
            string searchText = cultureInfo.ToTitleCase(ServiceName.Text.ToLower());
            var serchList = from service in services
                            where service.Name.Contains(searchText)
                            select service;
            PriceDataGrid.ItemsSource = serchList;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if(PriceDataGrid.SelectedItem != null)
            {
                Service service = (Service)PriceDataGrid.SelectedItem;
                EditServiceWindow editServiceWindow = new EditServiceWindow(service, _context);
                editServiceWindow.ShowDialog();
                services.Remove(service);
                services.Add(_context.Services.Where(c => c.ID == service.ID).FirstOrDefault());
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(PriceDataGrid.SelectedItem != null)
            {
                Service service = (Service)PriceDataGrid.SelectedItem;
                string message = "Данная услуга '" + service.Name + "' будет удалена, продолжить?";
                string caption = "Удаление услуги";
                MessageBoxButton messageBoxButton = MessageBoxButton.YesNo;
                var result = MessageBox.Show(message,caption,messageBoxButton, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    service.Delete = true;
                    _context.SaveChanges();
                    services.Remove(service);
                }
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(PriceDataGrid, "Печать таблицы");
            }
        }
    }
}
