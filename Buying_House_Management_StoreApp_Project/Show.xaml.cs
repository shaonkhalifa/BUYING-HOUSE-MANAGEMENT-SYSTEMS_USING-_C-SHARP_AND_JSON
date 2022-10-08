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
    /// Interaction logic for Show.xaml
    /// </summary>
    public partial class Show : Window
    {
        public string fileName = @"BuyerInformation.json";
        public FileInfo TempImageFile { get; set; }
        public BitmapImage DefaultImage => new BitmapImage(new Uri(GetImagePath() + "default.jpg"));
        public string DefaultImagePath => GetImagePath() + "default.jpg";

       

        public Show()
        {
            InitializeComponent();
        }
        private bool IsValidJson(string data)
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
        public ImageSource ImageInstance(Uri path)  //Create image instance rather than referencing image file
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = path;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.DecodePixelWidth = 300;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }

        public void Showdata()
        {
            var Json = File.ReadAllText(fileName);
            if (!IsValidJson(Json))
            {
                return;
            }
            var JsonObj = JObject.Parse(Json);
            var BuyerJason = JsonObj.GetValue("BuyerInformation").ToString();
            var Buyerlist = JsonConvert.DeserializeObject<List<BuyerInformation>>(BuyerJason);
            Buyerlist = Buyerlist.OrderBy(x => x.ID).ToList();
            foreach (var item in Buyerlist)
            {
                item.ImageShow= ImageInstance(new Uri(GetImagePath() + item.ImageInformation));
            }
            BuyerDetails.ItemsSource = Buyerlist;
            BuyerDetails.Items.Refresh();
           
        }
        
        private string GetImagePath()
        {
            var AddinAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var AddinFolder = System.IO.Path.GetDirectoryName(AddinAssembly.Location);
            string ImagePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AddinFolder, @"..\..\Images\"));

            return ImagePath;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Edit_Information edit = new Edit_Information();
            edit.Show();
            this.Hide();
            edit.Owner = this;
            Button b = sender as Button;
            BuyerInformation information = b.CommandParameter as BuyerInformation;
            edit.txtID.IsEnabled = false;
            edit.txtID.Text = information.ID.ToString();
            edit.Cmb_Title.Text = information.Title;
            edit.txtFirstName.Text = information.FirstName;
            edit.txtLastName.Text = information.LastName;
            edit.Cmb_Gender.Text = information.Gender;
            edit.txtPhone.Text = information.Phone;
            edit.txtDesignation.Text = information.Designation;
            edit.txtcompany.Text = information.Company;
            edit.txt_Country.Text = information.Country;
            edit.txtEmail.Text = information.Email;
            edit.ImageShow.Source = information.ImageShow;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var JasonD = File.ReadAllText(fileName);
            var JasonObj = JObject.Parse(JasonD);
            JArray BuyerDeleteArry = (JArray)JasonObj["BuyerInformation"];
            Button b = sender as Button;
            BuyerInformation buyer = b.CommandParameter as BuyerInformation;
            int buyerId = buyer.ID;
            if (buyerId > 0)
            {
                var informationToDeleted = BuyerDeleteArry.FirstOrDefault(obj => obj["ID"].Value<int>() == buyerId);
                BuyerDeleteArry.Remove(informationToDeleted);
                string output = JsonConvert.SerializeObject(JasonObj, Formatting.Indented);

                MessageBoxResult result = MessageBox.Show($"Are you want to delete {informationToDeleted["ID"].Value<string>()}", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    File.WriteAllText(fileName, output);
                    MessageBox.Show("Data Deleted Successfully !!", "Delete", MessageBoxButton.OK, MessageBoxImage.Question);
                    Showdata();
                    Buyer bu = new Buyer();
                    bu.AllClear();
                }
                else
                {
                    return;
                }
            }  
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Buyer buyer = new Buyer();
            buyer.Show();
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Edit_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            Viewdata viewdata = new Viewdata();
            viewdata.Show();
            Button b = sender as Button;
            BuyerInformation i = b.CommandParameter as BuyerInformation;

            viewdata.txttest.Text = $" Id Number\t:  {i.ID}\n Name\t\t:  {i.Title } {  i.FirstName } {i.LastName} \n Gender\t\t:  {i.Gender}  \n Designation\t:  {i.Designation} \n Company\t:  {i.Company} \n Country\t\t:  {i.Country} \n Phone\t\t:  {i.Phone} \n Email\t\t:  {i.Email}";
            viewdata.imagedata.Source = i.ImageShow;
            this.Close();
        }
    }
}
