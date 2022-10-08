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
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
        public string fileName = @"orderinformation.json";//put the json file location into a global field.
        public OrderDetails()
        {

            InitializeComponent();
            


            string[] titles = new string[] { "T-Shirt", "Full Shirt", "Denim Pant","Half-Shirt","Baby Product" };//Put value in combobox
            this.cmbproductname.ItemsSource = titles;
            cmbproductname.Text = titles[0];
        }

        private void cmbproductname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

  

        private void btnorderdetails_Click(object sender, RoutedEventArgs e)
        {
            ShowOrderInformation showOrder = new ShowOrderInformation();
            showOrder.Show();
            this.Close();
            showOrder.Showdata();
        }

    
        private bool IsValidJson(string data)//this method is creating For checking the data is valid or not
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
        private bool IsExists(string data)//this method is creating for checking data is existing or not
        {
            string filedata = File.ReadAllText(fileName);

            var root = (JContainer)JToken.Parse(filedata);//copy the data from filedata and generated it in jcontainer. 

            var name = root.DescendantsAndSelf()//elements contain root and all descendant element of this element in document order
                .OfType<JProperty>()
                .Select(p => p.Name)
                .FirstOrDefault();
            if (name == data)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsIdExists(int inputId)//This Method is create to avoid duplicate orderId
        {
            string filedata = File.ReadAllText(fileName);
            var fileObj = JObject.Parse((string)filedata);
            var orderJson = fileObj.GetValue("orderInformation").ToString();
            var ds = JsonConvert.DeserializeObject<List<orderInformation>>(orderJson);
            var exists = ds.Find(x => x.orderId == inputId);//check is available or not..
            if (exists != null)//if is not null...then show the message below...
            {
                MessageBox.Show($"ID - {exists.orderId} exists\nTry with different Id", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void rbtnyes_Click(object sender, RoutedEventArgs e)
        {
            
        }

       

        private void btnSaveorder_Click(object sender, RoutedEventArgs e)
        {
            
            orderInformation r = new orderInformation();
            

            r.orderId = Convert.ToInt32(txtorderId.Text);                   //convert it into integer data type format
            r.productName = cmbproductname.SelectedItem.ToString();         //convert the seleted item into string data type format
            r.productAvailable = bool.Parse(rbtnyes.IsChecked.ToString());  //Convert it boolen data type
            r.orderDate = txtdatepicker.SelectedDate.Value.Date;            //convert it into date time data type format
            r.quantity = int.Parse(txtquantity.Text);                       //convert it into integer data type format
            r.Unitprice = int.Parse(txtUnitPrice.Text);                     //convert it into integer data type format
            r.TotalPrice = Convert.ToInt32(r.Unitprice * r.quantity);       //This is Calculative option ..I claculate Total Price mulitiple by unit price and quantity
            string filedata = File.ReadAllText(fileName);

            if (IsValidJson(filedata) && IsExists("orderInformation") && !IsIdExists(r.orderId))        //check file contains valid json format and exists
            {
                var inputString = r.Serialize();                        //serialize the data
                var inputObject = JObject.Parse(inputString);//convert inputString data as json oject
                var fileObj = JObject.Parse(filedata);//convert filedata data as json oject
                var OrderArray = fileObj.GetValue("orderInformation") as JArray;//put data into JArry
                OrderArray.Add(inputObject);//add order arry data into inputObject
                fileObj["orderInformation"] = OrderArray;
                var newJasonResult = fileObj.Serialize();//serialize the file object data
                MessageBox.Show("Data Saved Successfully!!!", "Save", MessageBoxButton.OK, MessageBoxImage.Information);

                File.WriteAllText(fileName, newJasonResult);



            }
            if (!IsValidJson(filedata))
            {
                var x = new { orderInformation = new orderInformation[] { r } };
                string NewJsonResult = JsonConvert.SerializeObject(x, Formatting.Indented);

                File.WriteAllText(fileName, NewJsonResult);
            }
            AllClear();
        }
        public void AllClear()//To clear text box and button
        {
            txtorderId.Clear();
            cmbproductname.SelectedIndex = -1;
            txtquantity.Clear();
            txtUnitPrice.Clear();
            rbtnyes.IsChecked = false;
            txtdatepicker.SelectedDate = null;
            txtorderId.IsEnabled = true;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
