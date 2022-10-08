using Buying_House_Management_StoreApp_Project.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Viewdata.xaml
    /// </summary>
    public partial class Viewdata : Window
    {
        Show S = new Show();
        public Viewdata()
        {
            InitializeComponent();



            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            S.Show();
            S.Showdata();
            this.Close();
        }
    }
}
