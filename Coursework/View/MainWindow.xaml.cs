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
        private static SHA256 sHA256 = SHA256.Create();

        public MainWindow()
        {
            InitializeComponent();
            UserName.Visibility = Visibility.Hidden;
            Menu.Width = new GridLength(0, GridUnitType.Star);
            NavMenu.Visibility = Visibility.Hidden;

            //var date1 = new DateTime(2008, 5, 1, 8, 30, 52);
            _context.Services.Load();
            _context.Contracts.Load();
            //var edit = _context.Contracts.Where(c => c.ID == 1).FirstOrDefault();
            //edit.ClientID = 1;
            //_context.SaveChanges();

            //var service = _context.Services.Where(c => c.ID == 1).FirstOrDefault();
            //var service2 = _context.Services.Where(c => c.ID == 2).FirstOrDefault();

            //Contract contract = new Contract { ClientID = 1010, DateConclusionContract = date1, DateOfCompletion = date1, TotalAmount = 0 };
            //_context.Contracts.Add(contract);
            //_context.SaveChanges();

            //int price = service2.Price * 15;
            //int price2 = service2.Price * 20;
            //Estimate estimate = new Estimate { ContractID = 2, Quantity = 15, FullPrice = price};
            //estimate.Services.Add(service2);
            //Estimate estimate1 = new Estimate { ContractID = 1, Quantity = 20, FullPrice = price2 };
            //estimate1.Services.Add(service2);
            //_context.Estimates.Add(estimate);
            //_context.Estimates.Add(estimate1);
            //_context.SaveChanges();
            //_context.Estimates.Load();

            //var test1 = _context.Estimates.Include(t => t.Services).Where(c => c.ContractID == 1);
            //var test2 = _context.Estimates.Include(t => t.Services).Where(c => c.ContractID == 2);

            //foreach (var i in _context.Estimates.Include(t => t.Services).Where(c => c.ContractID == 1))
            //{
            //    foreach(var j in i.Services)
            //    {
            //        MessageBox.Show(j.Name.ToString());
            //    }
            //}
            //foreach (var i in _context.Estimates.Include(t => t.Services).Where(c => c.ContractID == 2))
            //{
            //    foreach (var j in i.Services)
            //    {
            //        MessageBox.Show(j.Name.ToString());
            //    }
            //}
            //foreach(var i in _context.Services.Include(t => t.Estimates).Where(c => c.ID == 2))
            //{
            //    foreach(var j in i.Estimates)
            //    {
            //        MessageBox.Show(j.ID.ToString());
            //    }
            //}
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
                    UserName.Visibility = Visibility.Visible;
                    MainPage.Background = Brushes.MediumTurquoise;
                    UserName.Text = i.Login;
                    sHA256.Dispose();
                }
                else
                {
                    Status.Visibility = Visibility.Visible;
                    Status.Text = "Неверное имя пользователя или пароль.";
                }
            }
        }

        private void Button_Click_Reg(object sender, RoutedEventArgs e)
        {
            //создание случайно сгенерированной соли
            byte[] bufferForSalt = { 33,56,86,89,2,254,1,35,117};
            int salt = BitConverter.ToInt32(bufferForSalt, 0);

            //объединение соли и пароля и их хэширование 
            string passwordAndSalt = salt + Password.Password;
            byte[] passwordBytes = ASCIIEncoding.ASCII.GetBytes(passwordAndSalt);
            byte[] passwordHashByte = sHA256.ComputeHash(passwordBytes);
            string passwordHash = BitConverter.ToString(passwordHashByte, 0);

            User user = new User { Login = Login.Text, Password = passwordHash.ToString() };


            _context.Users.Add(user);
            _context.SaveChanges();


        }

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new MainPage());
            MainPage.Background = Brushes.MediumTurquoise;
            ClientPage.Background = Brushes.Teal;
            EstimatePage.Background = Brushes.Teal;
            ServicePage.Background = Brushes.Teal;
            ContractPage.Background = Brushes.Teal;

        }

        private void ClientPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new ClientPage());
            MainPage.Background = Brushes.Teal;
            ClientPage.Background = Brushes.MediumTurquoise;
            EstimatePage.Background = Brushes.Teal;
            ServicePage.Background = Brushes.Teal;
            ContractPage.Background = Brushes.Teal;
        }

        private void ContractPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new ContractPage());
            MainPage.Background = Brushes.Teal;
            ClientPage.Background = Brushes.Teal;
            EstimatePage.Background = Brushes.Teal;
            ServicePage.Background = Brushes.Teal;
            ContractPage.Background = Brushes.MediumTurquoise;
        }

        private void ServicePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new ServicePage());
            MainPage.Background = Brushes.Teal;
            ClientPage.Background = Brushes.Teal;
            EstimatePage.Background = Brushes.Teal;
            ServicePage.Background = Brushes.MediumTurquoise;
            ContractPage.Background = Brushes.Teal;
        }

        private void EstimatePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new EstimatePage());
            MainPage.Background = Brushes.Teal;
            ClientPage.Background = Brushes.Teal;
            EstimatePage.Background = Brushes.MediumTurquoise;
            ServicePage.Background = Brushes.Teal;
            ContractPage.Background = Brushes.Teal;
        }
    }
}
