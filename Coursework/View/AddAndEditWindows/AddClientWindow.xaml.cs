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
using Coursework.Entities;
using System.Text.RegularExpressions;

namespace Coursework.View.AddAndEditWindows
{
    /// <summary>
    /// Interaction logic for AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        private DatabaseContext _context;
        public AddClientWindow(DatabaseContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(ClientLastNameValidationStatus.Text == "" && ClientLastNameValidationStatus.Text == "" && ClientLastNameValidationStatus.Text == "")
            {
                Client client = new Client { FirstName = ClientFirstName.Text, LastName = ClientLastName.Text, PhoneNumber = ClientPhoneNumber.Text };
                _context.Clients.Add(client);
                _context.SaveChanges();
                this.Close();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
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
                    ClientFirstNameValidationStatus.Text = "Введите имя без пробелов в формате: 'Евгений'";
                }
            }
        }

        private void ClientLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ClientLastName.Text != "")
            {
                string pattern = @"[А-Я]{1}[а-я]+$";
                if(Regex.IsMatch(ClientLastName.Text, pattern))
                {
                    ClientLastNameRectangle.Stroke = Brushes.MediumTurquoise;
                    ClientLastNameValidationStatus.Text = "";
                }
                else
                {
                    ClientLastNameRectangle.Stroke = Brushes.PaleVioletRed;
                    ClientLastNameValidationStatus.Text = "Введите фамилию без пробелов в формате: 'Иванов'";
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
