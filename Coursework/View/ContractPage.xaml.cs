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
using Coursework.View.AddAndEditWindows;

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {
        private ObservableCollection<ContractAndEstimate> ContractAndServices;
        private ObservableCollection<Contract> contracts;
        DatabaseContext _context = new DatabaseContext();
        
        public ContractPage()
        {
            InitializeComponent();
            Background = BackgroundColor.Colors();

            var contractsAndServices = _context.Contracts.Select(p => new ContractAndEstimate
            {
                ContractID = p.ID,
                ClientID = (int)p.ClientID,
                FirstName = p.Client.FirstName,
                LastName = p.Client.LastName,
                DateConclusionContract = p.DateConclusionContract,
                DateOfCompletion = p.DateOfCompletion,
                TotalAmount = p.TotalAmount
            }).ToList();

            ContractAndServices = new ObservableCollection<ContractAndEstimate>(contractsAndServices);

            _context.Contracts.Include(c => c.Client).Load();

            //var test = _context.Contracts.Include(c => c.Client).FirstOrDefault();
           // MessageBox.Show(test.Client.LastName);

            var contract = _context.Contracts.Include(c => c.Client).ToList();
            contracts = new ObservableCollection<Contract>(contract);
            ContractList.ItemsSource = ContractAndServices;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddContractWindow addContractWindow = new AddContractWindow(_context);

            var result = addContractWindow.ShowDialog();
            if(result ==true)
            {
                AddEstimateWindow addEstimateWindow = new AddEstimateWindow(_context);
                result = addEstimateWindow.ShowDialog();
                if(result == true && _context.Contracts.OrderByDescending(c => c.ID).FirstOrDefault().Estimates.Count() != 0)
                {
                    ContractAndServices.Add(_context.Contracts.OrderByDescending(c => c.ID).Select(p => new ContractAndEstimate
                    {
                        ContractID = p.ID,
                        ClientID = (int)p.ClientID,
                        FirstName = p.Client.FirstName,
                        LastName = p.Client.LastName,
                        DateConclusionContract = p.DateConclusionContract,
                        DateOfCompletion = p.DateOfCompletion,
                        TotalAmount = p.TotalAmount
                    }).FirstOrDefault());
                    ContractList.Items.Refresh();
                }
                else
                {
                    _context.Contracts.Remove(_context.Contracts.OrderByDescending(c => c.ID).FirstOrDefault());
                    _context.SaveChanges();
                    //ContractAndServices.Add(_context.Contracts.OrderByDescending(c => c.ID).Select(p => new ContractAndEstimate
                    //{
                    //    ContractID = p.ID,
                    //    ClientID = (int)p.ClientID,
                    //    FirstName = p.Client.FirstName,
                    //    LastName = p.Client.LastName,
                    //    DateConclusionContract = p.DateConclusionContract,
                    //    DateOfCompletion = p.DateOfCompletion,
                    //    TotalAmount = p.TotalAmount
                    //}).FirstOrDefault());
                    //ContractList.Items.Refresh();
                }
            }
        }

        //private void EditButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if(ContractList.SelectedValue != null)
        //    {
        //        ContractAndEstimate contractAndEstimate = (ContractAndEstimate)ContractList.SelectedValue;
        //        EditContractWindow editContractWindow = new EditContractWindow(_context, contractAndEstimate.ContractID);
        //        var result = editContractWindow.ShowDialog();
        //        if(result == true)
        //        {
        //            //EditEstimateWindow editEstimateWindow = new EditEstimateWindow(_context, contractAndEstimate.ContractID);
        //            //result = editEstimateWindow.ShowDialog();
        //            if(result == true)
        //            {
        //                //доделать редактирование
        //                ContractAndServices.Remove(ContractAndServices.Where(c => c.ClientID == contractAndEstimate.ClientID).FirstOrDefault());
        //                ContractAndServices.Add(_context.Contracts.Where(c => c.ClientID == contractAndEstimate.ClientID).Select(p => new ContractAndEstimate
        //                {
        //                    ContractID = p.ID,
        //                    ClientID = (int)p.ClientID,
        //                    FirstName = p.Client.FirstName,
        //                    LastName = p.Client.LastName,
        //                    DateConclusionContract = p.DateConclusionContract,
        //                    DateOfCompletion = p.DateOfCompletion,
        //                    TotalAmount = p.TotalAmount
        //                }).FirstOrDefault());
        //            }
        //        }
        //        else
        //        {
        //            ContractAndServices.Remove(ContractAndServices.Where(c => c.ClientID == contractAndEstimate.ClientID).FirstOrDefault());
        //            ContractAndServices.Add(_context.Contracts.Where(c => c.ClientID == contractAndEstimate.ClientID).Select(p => new ContractAndEstimate
        //            {
        //                ContractID = p.ID,
        //                ClientID = (int)p.ClientID,
        //                FirstName = p.Client.FirstName,
        //                LastName = p.Client.LastName,
        //                DateConclusionContract = p.DateConclusionContract,
        //                DateOfCompletion = p.DateOfCompletion,
        //                TotalAmount = p.TotalAmount
        //            }).FirstOrDefault());
        //        }

        //    }
        //}

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(ContractList.SelectedValue != null)
            {
                ContractAndEstimate contract = (ContractAndEstimate)ContractList.SelectedValue;
                var result = MessageBox.Show($"Данный договор {contract.ContractID} будет удален, продолжить?","Удаление договора",MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    ContractAndEstimate contractAndEstimate = (ContractAndEstimate)ContractList.SelectedValue;
                    _context.Contracts.Remove(_context.Contracts.Where(c => c.ID == contractAndEstimate.ContractID).FirstOrDefault());
                    _context.SaveChanges();
                    ContractAndServices.Remove(ContractAndServices.Where(c => c.ContractID == contractAndEstimate.ContractID).FirstOrDefault());
                }
            }
        }

        private void ContractList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ContractList.SelectedValue != null)
            {
                ContractAndEstimate contract = (ContractAndEstimate)ContractList.SelectedValue;
                EstimateForContractWindow estimateForContractWindow = new EstimateForContractWindow(_context, contract.ContractID);
                estimateForContractWindow.ShowDialog();
            }
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
