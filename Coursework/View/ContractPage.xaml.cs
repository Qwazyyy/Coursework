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
using System.Collections.ObjectModel;

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {
        private ObservableCollection<EstimateAndService> EstimateAndServices;
        DatabaseContext _context = new DatabaseContext();
        public ContractPage()
        {
            InitializeComponent();

            //var EstimateAndService = _context.Estimates
            //    .Include(e => e.Services)
            //    .SelectMany(e => new {EstimateId = e.Id, e.Quantity, e.FullPrice, });
            _context.Contracts.Include(c => c.Client).Load();
            List<EstimateAndService> estimateAndServices = new List<EstimateAndService>();

            foreach(var estimate in _context.Estimates.Include(t => t.Services).Where(c => c.ContractID == 2))
            {
                foreach(var service in estimate.Services)
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
                    estimateAndServices.Add(estimateAndService);
                }
            }

            EstimateAndServices = new ObservableCollection<EstimateAndService>(estimateAndServices);
            var bindinglist = estimateAndServices;
            test.ItemsSource = bindinglist;
            ContractList.ItemsSource = _context.Contracts.Include(c => c.Client).ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/AddContractPage.xaml", UriKind.Relative));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/AddEstimate.xaml", UriKind.Relative));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
