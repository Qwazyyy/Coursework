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
using System.Windows.Shapes;
using System.Data.Entity;
using System.Collections.ObjectModel;
using Coursework.Entities;


namespace Coursework.View.AddAndEditWindows
{
    /// <summary>
    /// Interaction logic for AddContractWindow.xaml
    /// </summary>
    public partial class AddContractWindow : Window
    {
        public class ClientForComboBox
        {
            public int ID { get; set; }
            public string FIO { get;set; }
        }
        private ObservableCollection<Client> clients;
        private ObservableCollection<ClientForComboBox> clientForComboBox;
        DatabaseContext _context;
        public AddContractWindow(DatabaseContext context)
        {
            InitializeComponent();
            _context = context;
            _context.Clients.Load();
            clients = new ObservableCollection<Client>(_context.Clients.Local.ToList());
            var clientFor = _context.Clients.AsEnumerable().Select(p => new ClientForComboBox
            {
                ID = p.ID,
                FIO = $"{p.LastName} {p.FirstName} {p.PhoneNumber}"
            }).ToList();

            clientForComboBox = new ObservableCollection<ClientForComboBox>(clientFor);

            ClientsComboBox.ItemsSource = clientForComboBox;
            ClientsComboBox.DisplayMemberPath = "FIO";
            ClientsComboBox.SelectedValuePath = "ID";

            DateFinal.SelectedDate = DateTime.Now;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClientsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if( ClientsComboBox.SelectedValue != null)
            {
                ClientRectangle.Stroke = Brushes.MediumTurquoise;
                ClientValidationStatus.Text = "";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(ClientsComboBox.SelectedValue != null)
            {
                Contract contract = new Contract
                {
                    DateConclusionContract = DateTime.Now,
                    DateOfCompletion = (DateTime)DateFinal.SelectedDate,
                    ClientID = (int)ClientsComboBox.SelectedValue,
                    TotalAmount = 0
                };
                _context.Contracts.Add(contract);
                _context.SaveChanges();
                this.DialogResult = true;
            }
        }

        private void DateFinal_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateFinal.SelectedDate != null)
            {
                DateFinalRectangle.Stroke = Brushes.MediumTurquoise;
                DateFinalValidationStatus.Text = "";
            }
        }
    }
}
