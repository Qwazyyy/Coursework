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
using Coursework.View;
using System.Security.Cryptography;
using Coursework.Entities;
using System.Data.Entity;


namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseContext _context = new DatabaseContext();

        //метод хэширования sHA256
        private static SHA256 sHA256;

        public MainWindow()
        {
            InitializeComponent();
            UserName.Visibility = Visibility.Hidden;
            Menu.Width = new GridLength(0, GridUnitType.Star);
            NavMenu.Visibility = Visibility.Hidden;
            sHA256 = SHA256.Create();
        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {

            string passwordAndSalt;
            byte[] passwordBytes;
            byte[] passwordHashByte;

            byte[] bufferForSalt = { 33, 56, 86, 89, 2, 254, 1, 35, 117 };
            int salt = BitConverter.ToInt32(bufferForSalt, 0);

            foreach (var i in _context.Users)
            {
                //проверка пароля
                passwordAndSalt = salt + Password.Password;
                passwordBytes = ASCIIEncoding.ASCII.GetBytes(passwordAndSalt);
                passwordHashByte = sHA256.ComputeHash(passwordBytes);

                if (i.Login == Login.Text && i.Password == BitConverter.ToString(passwordHashByte, 0))
                {
                    //выделение место под навигационное меню
                    Menu.Width = new GridLength(0.2, GridUnitType.Star);
                    //скрытие окна авторизации
                    LoginStackPanel.Visibility = Visibility.Hidden;
                    //показ навигационного меню
                    NavMenu.Visibility = Visibility.Visible;

                    Frame.Navigate(new MainPage());
                    this.Title = "";
                    UserName.Visibility = Visibility.Visible;
                    MainPage.Background = Brushes.MediumTurquoise;
                    UserName.Text = i.Login;
                    sHA256.Dispose();
                }
                else
                {
                    Status.Visibility = Visibility.Visible;
                    Status.Text = "Неверное имя пользователя или пароль.";
                    RectangleLogin.Stroke = Brushes.PaleVioletRed;
                    RectanglePassword.Stroke = Brushes.PaleVioletRed;
                }
            }
        }

        //private void Button_Click_Reg(object sender, RoutedEventArgs e)
        //{
        //    //создание случайно сгенерированной соли
        //    byte[] bufferForSalt = { 33,56,86,89,2,254,1,35,117};
        //    int salt = BitConverter.ToInt32(bufferForSalt, 0);

        //    //объединение соли и пароля и их хэширование 
        //    string passwordAndSalt = salt + Password.Password;
        //    byte[] passwordBytes = ASCIIEncoding.ASCII.GetBytes(passwordAndSalt);
        //    byte[] passwordHashByte = sHA256.ComputeHash(passwordBytes);
        //    string passwordHash = BitConverter.ToString(passwordHashByte, 0);

        //    User user = new User { Login = Login.Text, Password = passwordHash.ToString() };


        //    _context.Users.Add(user);
        //    _context.SaveChanges();


        //}

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Uri("View/MainPage.xaml", UriKind.Relative));
            MainPage.Background = Brushes.MediumTurquoise;
            ClientPage.Background = Brushes.Teal;
            //EstimatePage.Background = Brushes.Teal;
            ServicePage.Background = Brushes.Teal;
            ContractPage.Background = Brushes.Teal;

        }

        private void ClientPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Uri("View/ClientPage.xaml", UriKind.Relative));
            MainPage.Background = Brushes.Teal;
            ClientPage.Background = Brushes.MediumTurquoise;
            //EstimatePage.Background = Brushes.Teal;
            ServicePage.Background = Brushes.Teal;
            ContractPage.Background = Brushes.Teal;
        }

        private void ContractPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Uri("View/ContractPage.xaml", UriKind.Relative));
            MainPage.Background = Brushes.Teal;
            ClientPage.Background = Brushes.Teal;
            //EstimatePage.Background = Brushes.Teal;
            ServicePage.Background = Brushes.Teal;
            ContractPage.Background = Brushes.MediumTurquoise;
        }

        private void ServicePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Uri("View/ServicePage.xaml", UriKind.Relative));
            MainPage.Background = Brushes.Teal;
            ClientPage.Background = Brushes.Teal;
            //EstimatePage.Background = Brushes.Teal;
            ServicePage.Background = Brushes.MediumTurquoise;
            ContractPage.Background = Brushes.Teal;
        }

        private void EstimatePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Uri("View/EstimatePage.xaml", UriKind.Relative));
            MainPage.Background = Brushes.Teal;
            ClientPage.Background = Brushes.Teal;
            //EstimatePage.Background = Brushes.MediumTurquoise;
            ServicePage.Background = Brushes.Teal;
            ContractPage.Background = Brushes.Teal;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButton messageBoxButton = MessageBoxButton.YesNo;
            string text = "Вы действительно хотите выйти?";
            string caption = "Выход";
            var result = MessageBox.Show(text, caption, messageBoxButton, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
