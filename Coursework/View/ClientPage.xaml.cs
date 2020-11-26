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
using System.Globalization;
using Coursework.View.AddAndEditWindows;

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
            Background = BackgroundColor.Colors();
            _context.Clients.Load();

            var clientBindingList = _context.Clients.Local.ToBindingList();
            var clientList = _context.Clients.Local.ToList();

            clients = new ObservableCollection<Client> (clientList);

            ClientList.ItemsSource = clients;
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow(_context);
            addClientWindow.ShowDialog();
            clients.Add(_context.Clients.OrderByDescending(c => c.ID).LastOrDefault());
            //this.NavigationService.Navigate(new Uri("View/AddAndEditClientPage.xaml", UriKind.Relative));
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            if (ClientList.SelectedItem != null)
            {
                Client client = (Client)ClientList.SelectedItem;
                EditClientWindow editClientWindow = new EditClientWindow(client, clients, _context);
                editClientWindow.ShowDialog();
                clients.Remove(client);
                clients.Add(_context.Clients.Where(c => c.ID == client.ID).FirstOrDefault());
            }
        }

        private void ClientList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void ClientLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextInfo textInfo = new CultureInfo("ru-RU").TextInfo;
            string searchText = textInfo.ToTitleCase(ClientLastName.Text.ToLower());
            var serchList = from client in clients
                            where client.LastName.Contains(searchText)
                            select client;
            ClientList.ItemsSource = serchList;
        }

        private void ClientPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = ClientPhoneNumber.Text;
            var serchList = from client in clients
                            where client.PhoneNumber.Contains(searchText)
                            select client;
            ClientList.ItemsSource = serchList;
        }

        private void DeletClient_Click(object sender, RoutedEventArgs e)
        {
            if(ClientList.SelectedIndex > -1)
            {
                Client client = (Client)ClientList.SelectedItem;
                string message = "Данный клиент:" + client.FirstName + client.LastName + "будет удален из базы, продолжить?";
                MessageBoxButton messageBoxButton = MessageBoxButton.YesNo;
                string caption = "Удаление клиента";

                var dialogResult = MessageBox.Show(message, caption, messageBoxButton, MessageBoxImage.Question);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    foreach (var contract in _context.Contracts.Where(c => c.ClientID == client.ID))
                    {
                        _context.Contracts.Remove(contract);
                    }
                    _context.Clients.Remove(client);
                    _context.SaveChanges();
                    clients.Remove(clients.Where(c => c.ID == client.ID).FirstOrDefault());
                }
            }
        }
    }
}
