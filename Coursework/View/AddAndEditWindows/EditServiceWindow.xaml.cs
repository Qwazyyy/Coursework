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
    /// Interaction logic for EditServiceWindow.xaml
    /// </summary>
    public partial class EditServiceWindow : Window
    {
        private DatabaseContext _context;
        private Service currentService;
        public EditServiceWindow(Service service,DatabaseContext context)
        {
            InitializeComponent();

            _context = context;
            currentService = service;

            ServiceName.Text = currentService.Name;
            ServiceUnit.Text = currentService.UnitOfMeasurement;
            ServicePrice.Text = currentService.Price.ToString();
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            currentService.Name = ServiceName.Text;
            currentService.UnitOfMeasurement = ServiceUnit.Text;
            currentService.Price = Int32.Parse(ServicePrice.Text);
            _context.SaveChanges();
            this.Close();
        }

        private void CancelChangesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ServicePrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ServicePrice.Text != "")
            {
                string pattern = @"^\d+$";
                if (Regex.IsMatch(ServicePrice.Text, pattern))
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
            if (ServiceName.Text != "")
            {
                string pattern = @"^[А-Я]{1}[а-я\s]+$";
                if (Regex.IsMatch(ServiceName.Text, pattern))
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
