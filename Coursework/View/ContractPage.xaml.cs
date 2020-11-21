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

namespace Coursework.View
{
    /// <summary>
    /// Interaction logic for ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {
        DatabaseContext _context = new DatabaseContext();
        public ContractPage()
        {
            InitializeComponent();

            var EstimateAndService = _context.Estimates
                .Include(e => e.Services)
                .SelectMany(e => new {EstimateId = e.Id, e.Quantity, e.FullPrice, });


            var bindinglist = _context.Estimates.Local.ToBindingList();
            test.ItemsSource = bindinglist;
            ContractList.ItemsSource = _context.Contracts.Include(c => c.Client).ToList();

        }
    }
}
