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
using Coursework.Entities;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Coursework.View.AddAndEditWindows
{
    /// <summary>
    /// Interaction logic for EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        DatabaseContext _context;
        private Client currentClient;
        public EditClientWindow(Client client, ObservableCollection<Client> clients, DatabaseContext context)
        {
            InitializeComponent();
            currentClient = client;

            ClientFirstName.Text = currentClient.FirstName;
            ClientLastName.Text = currentClient.LastName;
            ClientPhoneNumber.Text = currentClient.PhoneNumber;

            _context = context;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            currentClient.FirstName = ClientFirstName.Text;
            currentClient.LastName = ClientLastName.Text;
            currentClient.PhoneNumber = ClientPhoneNumber.Text;
            _context.SaveChanges();
            this.Close();
        }

        private void CancelChangesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClientFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ClientFirstName.Text != "")
            {
                string pattern = @"[А-Я]{1}[а-я]+$";
                if (Regex.IsMatch(ClientFirstName.Text, pattern))
                {
                    ClientFirstNameRectangle.Stroke = Brushes.MediumTurquoise;
                    ClientFirstNameValidationStatus.Text = "";
                }
                else
                {
                    ClientFirstNameRectangle.Stroke = Brushes.PaleVioletRed;
                    ClientFirstNameValidationStatus.Text = "Формат ввода: 'Евгений'";
                }
            }
        }

        private void ClientLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ClientLastName.Text != "")
            {
                string pattern = @"[А-Я]{1}[а-я]+$";
                if (Regex.IsMatch(ClientLastName.Text, pattern))
                {
                    ClientLastNameRectangle.Stroke = Brushes.MediumTurquoise;
                    ClientLastNameValidationStatus.Text = "";
                }
                else
                {
                    ClientLastNameRectangle.Stroke = Brushes.PaleVioletRed;
                    ClientLastNameValidationStatus.Text = "Формат ввода: 'Иванов'";
                }
            }
        }

        private void ClientPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ClientPhoneNumber.Text != "")
            {
                string pattern = @"\+7\d{10}";
                if (Regex.IsMatch(ClientPhoneNumber.Text, pattern))
                {
                    ClientPhoneNumberRectangle.Stroke = Brushes.MediumTurquoise;
                    ClientPhoneNumberValidationStatus.Text = "";
                }
                else
                {
                    ClientPhoneNumberRectangle.Stroke = Brushes.PaleVioletRed;
                    ClientPhoneNumberValidationStatus.Text = "Введите номер телефона без пробелов в формате: '+79221113322'";
                }
            }
        }

    }
}
