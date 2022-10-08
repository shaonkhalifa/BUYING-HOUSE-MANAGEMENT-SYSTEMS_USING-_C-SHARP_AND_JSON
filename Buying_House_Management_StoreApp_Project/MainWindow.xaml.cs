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

namespace Buying_House_Management_StoreApp_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBuyerList_Click(object sender, RoutedEventArgs e)
        {
            Buyer buyer = new Buyer();//to show new window i create an instance
            buyer.Show();
            this.Close();
        }

        private void BtnSupplier_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This Option is not Ready !!!","", MessageBoxButton.OK, MessageBoxImage.Information);

        }

      

        private void Btnorder_Click(object sender, RoutedEventArgs e)
        {
            OrderDetails order = new OrderDetails();//to show new window i create an instance
            order.Show();
            this.Close();
        }


        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
