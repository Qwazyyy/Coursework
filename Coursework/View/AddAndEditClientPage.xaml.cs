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

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for AddAndEditClientPage.xaml
    /// </summary>
    public partial class AddAndEditClientPage : Page
    {
        DatabaseContext _context = new DatabaseContext();
        public AddAndEditClientPage()
        {
            InitializeComponent();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Client client = new Client { FirstName = FirstName.Text, LastName = LastName.Text, PhoneNumber = PhoneNumber.Text };

            Status.Visibility = Visibility.Visible;
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/ClientPage.xaml", UriKind.Relative));
        }
    }
}
