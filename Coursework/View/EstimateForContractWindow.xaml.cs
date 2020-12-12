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
using System.Data.Entity;


namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for EstimateForContractWindow.xaml
    /// </summary>
    public partial class EstimateForContractWindow : Window
    {
        DatabaseContext _context;
        private ObservableCollection<EstimateAndService> EstimateAndServices;
        public EstimateForContractWindow(DatabaseContext context, int id)
        {
            InitializeComponent();
            _context = context;
            this.Title = "Смета для договора №" + id;
            double fullPriceEstimate = 0;
            List<EstimateAndService> estimateAndServices = new List<EstimateAndService>();
            foreach (var estimate in _context.Estimates.Include(t => t.Service).Where(c => c.ContractID == id))
            {
                    EstimateAndService estimateAndService = new EstimateAndService
                    {
                        ContractID = estimate.ContractID,
                        EstimateID = estimate.ID,
                        ServiceName = estimate.Service.Name,
                        ServiceUnit = estimate.Service.UnitOfMeasurement,
                        ServicePrice = estimate.Service.Price,
                        EstimateCount = estimate.Quantity,
                        EstimateFullPrice = estimate.Service.Price * estimate.Quantity
                    };
                    estimateAndServices.Add(estimateAndService);
                    fullPriceEstimate += estimate.Service.Price * estimate.Quantity;

            }
            EstimateAndServices = new ObservableCollection<EstimateAndService>(estimateAndServices);
            EstimateForContract.ItemsSource = EstimateAndServices;
            FullPriceEstimate.Text = "Полная цена за все услуги: " + fullPriceEstimate + "руб.";
        }

        private void PrintEstimate_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(EstimateForContract, "Печать таблицы");
            }
        }
    }
}
