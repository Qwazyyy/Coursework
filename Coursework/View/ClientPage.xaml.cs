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
using Coursework.Entities;
using Coursework.Methods;
using System.Collections.ObjectModel;

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        private ObservableCollection<Client> clients;
        DatabaseContext _context = new DatabaseContext();
        public ClientPage()
        {
            InitializeComponent();
            _context.Clients.Load();

            var clientBindingList = _context.Clients.Local.ToBindingList();
            var clientList = _context.Clients.Local.ToList();

            clients = new ObservableCollection<Client> (clientList);

            ClientTable.ItemsSource = clientBindingList;
            ClientList.ItemsSource = clients;

        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/AddAndEditClientPage.xaml", UriKind.Relative));
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/AddAndEditClientPage.xaml", UriKind.Relative));
        }

        private void ClientList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Client test = (Client)ClientList.SelectedItem;
            if(test != null)
            {
                MessageBox.Show(test.ID.ToString());
                DatabaseService.DeleteRowFromClients(test.ID, _context);
                clients.Remove(test);
            }
        }
    }
}
