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

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for AddContractPage.xaml
    /// </summary>
    public partial class AddContractPage : Page
    {
        private ObservableCollection<Client> clients;
        DatabaseContext _context = new DatabaseContext();

        public AddContractPage()
        {
            InitializeComponent();
            _context.Clients.Load();

            clients = new ObservableCollection<Client>(_context.Clients.Local.ToList());

            ClientsComboBox.ItemsSource = clients;
            ClientsComboBox.SelectedValuePath = "ID";

            DateFinal.SelectedDate = DateTime.Now;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/ContractPage.xaml", UriKind.Relative));
        }

        private void ClientsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Contract contract = new Contract
            {
                DateConclusionContract = DateTime.Now,
                DateOfCompletion = (DateTime)DateFinal.SelectedDate,
                ClientID = (int)ClientsComboBox.SelectedValue,
                TotalAmount = 0
            };
            _context.Contracts.Add(contract);
            Status.Text = "Данные сохранены";
            _context.SaveChanges();
        }
    }
}
