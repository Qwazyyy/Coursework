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

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for AddEstimate.xaml
    /// </summary>
    public partial class AddEstimate : Page
    {
        DatabaseContext _context = new DatabaseContext();
        private List<EstimateAndService> estimatesAndServices = new List<EstimateAndService>();

        public AddEstimate()
        {
            InitializeComponent();
            _context.Services.Load();
            _context.Estimates.Include(c => c.Services).Load();

            ServiceComboBox.ItemsSource = _context.Services.ToList();
            ServiceComboBox.DisplayMemberPath = "Name";
            ServiceComboBox.SelectedValuePath = "ID";

            foreach (var estimate in _context.Estimates.Include(t => t.Services).Where(c => c.ContractID == 1002))
            {
                foreach (var service in estimate.Services)
                {
                    EstimateAndService estimateAndService = new EstimateAndService
                    {
                        ContractID = estimate.ContractID,
                        SeviceName = service.Name,
                        ServiceUnit = service.UnitOfMeasurement,
                        ServicePrice = service.Price,
                        EstimateCount = estimate.Quantity,
                        EstimateFullPrice = estimate.FullPrice
                    };
                    estimatesAndServices.Add(estimateAndService);
                }
            }
            EstimateDataGrid.ItemsSource = estimatesAndServices;
        }

        private void ServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Service service = (Service)ServiceComboBox.SelectedItem;
            Unit.Text = service.UnitOfMeasurement.ToString();
        }

        private void Count_KeyUp(object sender, KeyEventArgs e)
        {
            Service service = (Service)ServiceComboBox.SelectedItem;
            TotalSum.Text = (service.Price * Int32.Parse(Count.Text)).ToString();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Service service = (Service)ServiceComboBox.SelectedItem;
            Estimate estimate = new Estimate { ContractID = 1002, Quantity = Int32.Parse(Count.Text), FullPrice = Int32.Parse(TotalSum.Text)};
            estimate.Services.Add(service);
            _context.Estimates.Add(estimate);
            EstimateAndService estimateAndService = new EstimateAndService
            {
                ContractID = estimate.ContractID,
                SeviceName = service.Name,
                ServiceUnit = service.UnitOfMeasurement,
                ServicePrice = service.Price,
                EstimateCount = estimate.Quantity,
                EstimateFullPrice = estimate.FullPrice
            };
            estimatesAndServices.Add(estimateAndService);
            _context.SaveChanges();
        }
    }
}
