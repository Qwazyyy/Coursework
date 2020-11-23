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
    /// Interaction logic for EstimatePage.xaml
    /// </summary>
    public partial class EstimatePage : Page
    {
        DatabaseContext _context = new DatabaseContext();
        public EstimatePage()
        {
            InitializeComponent();
            _context.Contracts.Load();
            _context.Estimates.Load();

            ContractsComboBox.ItemsSource = _context.Contracts.ToList();
            test.ItemsSource = _context.Estimates.Local.ToBindingList();
        }

        private void ContractsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contract contract = (Contract)ContractsComboBox.SelectedValue;
            test.ItemsSource = _context.Estimates.Where(c => c.ContractID == contract.ID).ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
