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
using Coursework.View;
using System.Data.Entity;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Coursework.View.AddAndEditWindows
{
    /// <summary>
    /// Interaction logic for AddEstimateWindow.xaml
    /// </summary>
    public partial class AddEstimateWindow : Window
    {
        DatabaseContext _context;
        private List<EstimateAndService> estimatesAndServices = new List<EstimateAndService>();
        private Contract CurrentContract;
        private int TotalAmount = 0;

        public AddEstimateWindow(DatabaseContext context)
        {
            InitializeComponent();
            _context = context;

            _context.Services.Load();
            _context.Contracts.Load();
            _context.Estimates.Include(c => c.Service).Load();

            ServiceComboBox.ItemsSource = _context.Services.ToList();
            ServiceComboBox.DisplayMemberPath = "Name";
            ServiceComboBox.SelectedValuePath = "ID";
            CurrentContract = _context.Contracts.OrderByDescending(c => c.ID).FirstOrDefault();
        }

        private void ServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceComboBox.SelectedItem != null)
            {
                Service service = (Service)ServiceComboBox.SelectedItem;
                Unit.Text = service.UnitOfMeasurement.ToString();
            }
        }

        private void Count_KeyUp(object sender, KeyEventArgs e)
        {
            string pattern = @"^\d+$";
            if (Regex.IsMatch(Count.Text, pattern) && ServiceComboBox.SelectedIndex > -1)
            {
                Service service = (Service)ServiceComboBox.SelectedItem;
                TotalSum.Text = (service.Price * Int32.Parse(Count.Text)).ToString();
                ValidationColor.Stroke = Brushes.MediumTurquoise;
                ValidationStatus.Text = "";
            }
            else
            {
                ValidationColor.Stroke = Brushes.Red;
                ValidationStatus.Text = "Поле принимает только числа не больше 7 знаков";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Service service = (Service)ServiceComboBox.SelectedItem;
            int count = 0;

            foreach (var estim in estimatesAndServices)
            {
                if (service.Name == estim.ServiceName)
                {
                    count = 1;
                }
            }
            //Contract contract = new Contract { DateConclusionContract = DateTime.Now, DateOfCompletion = DateTime.Now, TotalAmount = 0, ClientID = client.ID };
            //_context.Contracts.Add(contract);
            //_context.SaveChanges();
            if (count == 0)
            {
                if (Count.Text != "")
                {
                    Estimate estimate = new Estimate { ContractID = CurrentContract.ID, Quantity = Int32.Parse(Count.Text), ServiceID = service.ID };
                    //estimate.Services.Add(service);
                    _context.Estimates.Add(estimate);
                    EstimateAndService estimateAndService = new EstimateAndService
                    {
                        ContractID = estimate.ContractID,
                        EstimateID = estimate.ID,
                        ServiceName = service.Name,
                        ServiceUnit = service.UnitOfMeasurement,
                        ServicePrice = service.Price,
                        EstimateCount = estimate.Quantity,
                        EstimateFullPrice = (service.Price * (double)estimate.Quantity)
                    };
                    TotalAmount += Int32.Parse(TotalSum.Text);
                    TotalAmountEstimate.Text = "Итоговая сумма: " + TotalAmount + "руб.";
                    estimatesAndServices.Add(estimateAndService);
                    EstimateDataGrid.ItemsSource = estimatesAndServices;
                    _context.SaveChanges();

                    EstimateDataGrid.Items.Refresh();

                    TotalSum.Text = "";
                    Unit.Text = "";
                    Count.Text = "";
                    ServiceComboBox.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Введите размер работ!");
                }
            }
            else
            {
                MessageBox.Show("Выбраная работа уже добавлена в смету!");
            }
        }

        private void ServiceComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ServiceComboBox.IsDropDownOpen = true;
            //var tb = (TextBox)e.OriginalSource;
            //tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            //CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(ServiceComboBox.ItemsSource);
            //cv.Filter = s =>
            //    ((Estimate)s).IndexOf(ServiceComboBox.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private void SaveContract_Click(object sender, RoutedEventArgs e)
        {
            CurrentContract.TotalAmount = TotalAmount;
            _context.SaveChanges();
            this.DialogResult = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EstimateDataGrid.SelectedValue != null)
            {
                EstimateAndService estimateAndService = (EstimateAndService)EstimateDataGrid.SelectedValue;
                string content = $"Данная услуга '{estimateAndService.ServiceName}' будет удалена из сметы, продолжить?";
                string caption = "Удаление услуги";
                var result = MessageBox.Show(content, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    TotalAmount -= (int)estimateAndService.EstimateFullPrice;
                    TotalAmountEstimate.Text = $"Итоговая сумма: {TotalAmount} руб.";
                    Contract contract = _context.Contracts.Where(c => c.ID == CurrentContract.ID).FirstOrDefault();
                    contract.TotalAmount = TotalAmount;
                    _context.Estimates.Remove(_context.Estimates.OrderByDescending(c => c.ID).FirstOrDefault());
                    _context.SaveChanges();
                    estimatesAndServices.Remove(estimatesAndServices.Where(c => c.EstimateID == estimateAndService.EstimateID).FirstOrDefault());
                    EstimateDataGrid.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу в таблице сверху");
            }
        }
    }
}
