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
using Coursework.Entities;
using System.Data.Entity;

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for AddAndEditServicePage.xaml
    /// </summary>
    public partial class AddAndEditServicePage : Page
    {
        DatabaseContext _context = new DatabaseContext();
        public AddAndEditServicePage()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Service service = new Service { Name = NameService.Text, UnitOfMeasurement = UnitOfMeasurement.Text, Price = Int32.Parse(Price.Text) };

            _context.Services.Add(service);
            _context.SaveChanges();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/ServicePage.xaml", UriKind.Relative));
        }
    }
}
