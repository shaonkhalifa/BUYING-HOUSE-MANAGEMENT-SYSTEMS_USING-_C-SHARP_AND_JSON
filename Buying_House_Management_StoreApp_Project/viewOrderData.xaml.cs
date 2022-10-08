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

namespace Buying_House_Management_StoreApp_Project
{
    /// <summary>
    /// Interaction logic for viewOrderData.xaml
    /// </summary>
    public partial class viewOrderData : Window
    {
        public viewOrderData()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ShowOrderInformation showOrder = new ShowOrderInformation();
            showOrder.Show();
            showOrder.Showdata();
            this.Close();

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
