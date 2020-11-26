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
using Coursework.Methods;

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {
        private ObservableCollection<EstimateAndService> EstimateAndServices;
        private ObservableCollection<Contract> contracts;
        DatabaseContext _context = new DatabaseContext();
        public ContractPage()
        {
            InitializeComponent();
            Background = BackgroundColor.Colors();

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
                        ServiceName = service.Name,
                        ServiceUnit = service.UnitOfMeasurement,
                        ServicePrice = service.Price,
                        EstimateCount = estimate.Quantity,
                        EstimateFullPrice = estimate.FullPrice
                    };
                    estimateAndServices.Add(estimateAndService);
                }
            }

            EstimateAndServices = new ObservableCollection<EstimateAndService>(estimateAndServices);
            //var bindinglist = estimateAndServices;
            var contract = _context.Contracts.Include(c => c.Client).ToList();
            contracts = new ObservableCollection<Contract>(contract);
            ContractList.ItemsSource = contracts;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/AddContractPage.xaml", UriKind.Relative));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/AddEstimatePage.xaml", UriKind.Relative));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContractList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Contract contract = (Contract)ContractList.SelectedItem;
            List<EstimateAndService> estimateAndServices = new List<EstimateAndService>();
            foreach (var estimate in _context.Estimates.Include(t => t.Services).Where(c => c.ContractID == contract.ID))
            {
                foreach (var service in estimate.Services)
                {
                    EstimateAndService estimateAndService = new EstimateAndService
                    {
                        ContractID = estimate.ContractID,
                        ServiceName = service.Name,
                        ServiceUnit = service.UnitOfMeasurement,
                        ServicePrice = service.Price,
                        EstimateCount = estimate.Quantity,
                        EstimateFullPrice = estimate.FullPrice
                    };
                    estimateAndServices.Add(estimateAndService);
                }
            }
            EstimateAndServices = new ObservableCollection<EstimateAndService>(estimateAndServices);
            EstimateSelectedContract.ItemsSource = EstimateAndServices;
        }

        private void ClientLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = ClientLastName.Text;
            string lowerSearchText = ClientLastName.Text.ToLower();
            string upperSearchText = ClientLastName.Text.ToUpper();
            var serchList = from contract in contracts
                            where
                                contract.ID.ToString().StartsWith(upperSearchText)
                                || contract.ID.ToString().StartsWith(lowerSearchText)
                                || contract.ID.ToString().Contains(searchText)
                            select contract;
            ContractList.ItemsSource = serchList;
        }

        private void ContractNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = ContractNumber.Text;
            string lowerSearchText = ContractNumber.Text.ToLower();
            string upperSearchText = ContractNumber.Text.ToUpper();
            var serchList = from contract in contracts
                            where
                                contract.ID.ToString().StartsWith(upperSearchText)
                                || contract.ID.ToString().StartsWith(lowerSearchText)
                                || contract.ID.ToString().Contains(searchText)
                            select contract;
            ContractList.ItemsSource = serchList;
        }
    }
}
