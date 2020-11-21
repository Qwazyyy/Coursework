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

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        DatabaseContext _context = new DatabaseContext();
        public ServicePage()
        {
            InitializeComponent();
            _context.Services.Load();
            var servicesBindingList = _context.Services.Local.ToBindingList();
            var servicesList = _context.Services.Local.ToList();

            PriceDataGrid.ItemsSource = servicesBindingList;
            ServiceList.ItemsSource = servicesList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/AddAndEditServicePage.xaml", UriKind.Relative));
        }
    }
}
