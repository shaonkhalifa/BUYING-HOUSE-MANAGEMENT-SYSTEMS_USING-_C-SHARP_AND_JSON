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
using Buying_House_Management_StoreApp_Project.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Buying_House_Management_StoreApp_Project
{
    /// <summary>
    /// Interaction logic for EditOrderData.xaml
    /// </summary>
    public partial class EditOrderData : Window
    {
        OrderDetails details = new OrderDetails();
        public string fileName = @"orderinformation.json";
        public EditOrderData()
        {
            InitializeComponent();

            string[] titles = new string[] { "T-Shirt", "Full Shirt", "Denim Pant", "Half-Shirt", "Baby Product" };
            this.cmbproductname.ItemsSource = titles;
            cmbproductname.Text = titles[0];
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)//for updating i create different field and assaign value as property data type and convert data into json object  
        {
            var ID = int.Parse(txtorderId.Text);
            var ProductName = cmbproductname.SelectedItem.ToString();
            var OrderDate = txtdatepicker.SelectedDate.Value.Date; 
            var Quntity =int.Parse(txtquantity.Text);
            var UnitPrice = int.Parse(txtUnitPrice.Text);
            var available = bool.Parse(rbtnyes.IsChecked.ToString());
            var JasonD = File.ReadAllText(fileName);
            var JasonObj = JObject.Parse(JasonD);
            var OrderJason = JasonObj.GetValue("orderInformation").ToString();
            var Orderlist = JsonConvert.DeserializeObject<List<orderInformation>>(OrderJason);
            foreach (var item in Orderlist.Where(x => x.orderId == ID))
            {
                item.orderId = ID;
                item.productName = ProductName;
                item.productAvailable = available;
                item.orderDate = OrderDate;
                item.quantity = Quntity;
                item.Unitprice = UnitPrice;
                item.TotalPrice = Quntity * UnitPrice;
            }
            JArray orderArray = JArray.FromObject(Orderlist);
            JasonObj["orderInformation"] = orderArray;
            string output = JsonConvert.SerializeObject(JasonObj, Formatting.Indented);
            File.WriteAllText(@"orderinformation.json", output);
            this.Close();
            ShowOrderInformation show = new ShowOrderInformation();
            show.Show();
            show.Showdata();
            MessageBox.Show("Data Updated Successfully !!");
            details.AllClear();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ShowOrderInformation show = new ShowOrderInformation();
            show.Show();
            show.Showdata();//for showing data i use Showdata method 
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rbtnyes_Click(object sender, RoutedEventArgs e)
        {

        }

       

        private void cmbproductname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
