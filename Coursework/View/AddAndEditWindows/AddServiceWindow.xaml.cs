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
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Coursework.View.AddAndEditWindows
{
    /// <summary>
    /// Interaction logic for AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        DatabaseContext _context;
        public AddServiceWindow(DatabaseContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(ServicePriceValidationStatus.Text == "" && ServiceNameValidationStatus.Text == "")
            {
                Service service = new Service { Name = ServiceName.Text, UnitOfMeasurement = ServiceUnit.Text, Price = Int32.Parse(ServicePrice.Text) };
                _context.Services.Add(service);
                _context.SaveChanges();
                this.DialogResult = true;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ServicePrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ServicePrice.Text != "")
            {
                string pattern = @"^\d+$";
                if(Regex.IsMatch(ServicePrice.Text, pattern))
                {
                    ServicePriceRectangle.Stroke = Brushes.MediumTurquoise;
                    ServicePriceValidationStatus.Text = "";
                }
                else
                {
                    ServicePriceRectangle.Stroke = Brushes.PaleVioletRed;
                    ServicePriceValidationStatus.Text = "Формат ввода: '10000'";
                }
            }
            else
            {
                ServicePriceRectangle.Stroke = Brushes.PaleVioletRed;
                ServicePriceValidationStatus.Text = "Формат ввода: '10000'";
            }
        }

        private void ServiceName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ServiceName.Text != "")
            {
                string pattern = @"^[А-Я]{1}[а-я\s]+$";
                if(Regex.IsMatch(ServiceName.Text, pattern))
                {
                    ServiceNameRectangle.Stroke = Brushes.MediumTurquoise;
                    ServiceNameValidationStatus.Text = "";
                }
                else
                {
                    ServiceNameRectangle.Stroke = Brushes.PaleVioletRed;
                    ServiceNameValidationStatus.Text = "Формат ввода: 'Укладка плитки', 'Укладка плитки мозайкой'";
                }
            }
            else
            {
                ServiceNameRectangle.Stroke = Brushes.PaleVioletRed;
                ServiceNameValidationStatus.Text = "Формат ввода: 'Укладка плитки', 'Укладка плитки мозайкой'";
            }
        }

        private void ServiceUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceUnitRectangle.Stroke = Brushes.MediumTurquoise;
        }
    }
}
