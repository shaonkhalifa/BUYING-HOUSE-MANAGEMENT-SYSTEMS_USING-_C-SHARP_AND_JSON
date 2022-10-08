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
using Buying_House_Management_StoreApp_Project.Model;

namespace Buying_House_Management_StoreApp_Project
{
    /// <summary>
    /// Interaction logic for ShowOrderInformation.xaml
    /// </summary>
    public partial class ShowOrderInformation : Window
    {
        public string fileName = @"orderinformation.json";
        OrderDetails r = new OrderDetails();
        public ShowOrderInformation()
        {
            InitializeComponent();
          

        }
        public void Showdata()
        {

            var Json = File.ReadAllText(fileName);//put data from global field to Json field
            if (!IsValidJson(Json))//to check Json file is valid or not.....if not valid then return
            {
                return;
            }
            var JsonObj = JObject.Parse(Json);   //Convert data as Json Object
            var OrderJason = JsonObj.GetValue("orderInformation").ToString();   //Put the value from the Json Field to Orderjson as string data type
            var Orderlist = JsonConvert.DeserializeObject<List<orderInformation>>(OrderJason);  // DeserializObject from OrderJason
            Orderlist = Orderlist.OrderBy(x => x.orderId).ToList(); //To serialz data as acending Order
            OrderDetails.ItemsSource = Orderlist;
            OrderDetails.Items.Refresh();

        }
        private bool IsValidJson(string data)//To verify data is valid or not valid
        {
            try
            {
                var temdata = (JContainer)JToken.Parse(data);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        private void View_Click(object sender, RoutedEventArgs e)//To show the single Order information.
        {
            viewOrderData viewdata = new viewOrderData();//Instant create to access another window.
            viewdata.Show();
            Button b = sender as Button;
            orderInformation i = b.CommandParameter as orderInformation;

            viewdata.txtorderdetails.Text = $" Id Number\t\t:  {i.orderId}\n Product Name\t\t:  {i.productName } \n Product Available\t\t:  {i.productAvailable} \n Order Date\t\t:  {i.orderDate} \n Quantity\t\t:  {i.quantity} \n Unit Price\t\t:  {i.Unitprice} \n Total Amount\t\t:  {i.TotalPrice}";

            this.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {


            EditOrderData edit = new EditOrderData();//Instant create to access another window.
            edit.Show();
            this.Hide();
            edit.Owner = this;
            Button b = sender as Button;//to send data from orderInformation 
            orderInformation information = b.CommandParameter as orderInformation;//to send data from orderInformation 
            edit.txtorderId.IsEnabled = false;//I disable the order Id because ...People can not change the order id.
            edit.txtorderId.Text = information.orderId.ToString();
            edit.cmbproductname.SelectedItem = information.productName;
            edit.txtdatepicker.Text = information.orderDate.ToString();
            edit.txtquantity.Text = information.quantity.ToString();
            edit.txtUnitPrice.Text = information.Unitprice.ToString();
            edit.rbtnyes.IsChecked = information.productAvailable;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var JasonD = File.ReadAllText(fileName);//put data from global field to Json field
            var JasonObj = JObject.Parse(JasonD);//Convert data as Json Object and put it in JasonObj
            JArray OrderDeleteArry = (JArray)JasonObj["orderInformation"];//Put data into Json Array
            Button b = sender as Button;
            orderInformation Order = b.CommandParameter as orderInformation;
            int OrderId = Order.orderId;//put Order id Value order information in new field
            if (OrderId > 0)//check the order id is greater than 0 or not....If this is greater than 0 then excute internal option
            {
                var informationToDeleted = OrderDeleteArry.FirstOrDefault(obj => obj["orderId"].Value<int>() == OrderId);//check  object order id and order id are equal or not
                OrderDeleteArry.Remove(informationToDeleted);//if equal then delete
                string output = JsonConvert.SerializeObject(JasonObj, Formatting.Indented);

                MessageBoxResult result = MessageBox.Show($"Are you want to delete {informationToDeleted["orderId"].Value<string>()}", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);//This message box will show a warning do you want to delete or not
                if (result == MessageBoxResult.Yes)
                {
                    File.WriteAllText(fileName, output);
                    MessageBox.Show("Data Deleted Successfully !!", "Delete", MessageBoxButton.OK, MessageBoxImage.Question);//if yes then this message box will show the confirmation
                    Showdata();
                    r.AllClear();
                }
                else//If this is not greater than 0 then return
                {
                    return;
                }
            }

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            r.Show();
            this.Close();
        }
    }
}
