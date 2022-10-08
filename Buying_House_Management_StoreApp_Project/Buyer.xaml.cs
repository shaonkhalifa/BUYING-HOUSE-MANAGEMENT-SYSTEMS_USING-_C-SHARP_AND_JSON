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
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Path = System.IO.Path;

namespace Buying_House_Management_StoreApp_Project
{
    /// <summary>
    /// Interaction logic for Buyer.xaml
    /// </summary>
    
    public static class JsonExtension   // Extension method to serialize data
    {
        
        public static string Serialize(this object obj)
        {
            var data = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return data;
        }
    }
    public partial class Buyer : Window
    {
        Show Shows = new Show();
        public string fileName = @"BuyerInformation.json";
        public FileInfo TempImageFile { get; set; }
        public BitmapImage DefaultImage => new BitmapImage(new Uri(GetImagePath() + "default.jpg"));
        public string DefaultImagePath => GetImagePath() + "default.jpg";

       

        public Buyer()
        {
            InitializeComponent();
            string[] titles = new string[] { "Mr.", "Mrs.", "Miss." };//add data in combobox
            this.Cmb_Title.ItemsSource = titles;
            Cmb_Title.Text = titles[0];
            string[] gender = new string[] { "Male", "Female" };//add data in combobox
            this.Cmb_Gender.ItemsSource = gender;
            Cmb_Gender.Text = gender[0];


            var path = System.IO.Path.GetDirectoryName(GetImagePath());
            if (!File.Exists(@"BuyerInformation.json"))
            {
                File.CreateText(fileName);
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            ImageShow.Source = DefaultImage;
        }


        private string GetImagePath()//set the path to  save location
        {
            var AddinAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var AddinFolder= Path.GetDirectoryName(AddinAssembly.Location);
            string ImagePath = Path.GetFullPath(Path.Combine(AddinFolder, @"..\..\Images\"));

            return ImagePath;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            BuyerInformation b = new BuyerInformation();
            b.ID = int.Parse(txtID.Text);
            b.Title = Cmb_Title.SelectedItem.ToString();//convert selected item of combobox as string
            b.FirstName = txtFirstName.Text;
            b.LastName = txtLastName.Text;
            b.Gender = Cmb_Gender.SelectedItem.ToString();//convert selected item of combobox as string
            b.Phone = txtPhone.Text;
            b.Designation = txtDesignation.Text;
            b.Company = txtcompany.Text;
            b.Country = txt_Country.Text;
            b.Email = txtEmail.Text;
            b.ImageInformation = (TempImageFile != null) ? $"{int.Parse(txtID.Text) + TempImageFile.Extension}" : "default.jpg";//if user put image then save it as buyer id + image extension or user don't put image then use default image.



            string filedata = File.ReadAllText(fileName);

            if  (IsValidJson(filedata) && IsExists("BuyerInformation") && !IsIdExists(b.ID))    //check file contains valid json format and exists
            {
                var inputString = b.Serialize();
                var inputObject = JObject.Parse(inputString);
                var fileObj = JObject.Parse(filedata);
                var BuyerArray = fileObj.GetValue("BuyerInformation") as JArray;
                BuyerArray.Add(inputObject);
                fileObj["BuyerInformation"] = BuyerArray;
                var newJasonResult = fileObj.Serialize();
                MessageBox.Show("Data Saved Successfully!!!", "Save", MessageBoxButton.OK, MessageBoxImage.Information);


                File.WriteAllText(fileName, newJasonResult);        //write all employees to json file
                if (TempImageFile!=null)
                {
                    TempImageFile.CopyTo(GetImagePath() + b.ImageInformation);
                    TempImageFile = null;
                    ImageShow.Source = DefaultImage;

                }
            
                
            }
            if (!IsValidJson(filedata))
            {
                var x = new { BuyerinFormation = new BuyerInformation[] { b } };//create json format with parent[BuyerinFormation]
                string NewJsonResult = JsonConvert.SerializeObject(x, Formatting.Indented);   //serialize json format

                File.WriteAllText(fileName, NewJsonResult);                                  //write json format to BuyerInformation.json
                if (TempImageFile != null)
                {
                    TempImageFile.CopyTo(GetImagePath() + b.ImageInformation);
                    TempImageFile = null;
                    ImageShow.Source = DefaultImage;

                }
            }
            AllClear();
           
        }

        

        private bool IsValidJson(string data)      //check whether file contains json format or not
        {
            try
            {
                var temdata = (JContainer)JToken.Parse(data);//Try to parse json data if can't will throw exception
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private bool IsExists(string data)
        {
            string filedata = File.ReadAllText(fileName);

            var root = (JContainer)JToken.Parse(filedata);

            var name=root.DescendantsAndSelf()
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
        private bool IsIdExists(int inputId)             //this method create for avoiding duplicate buyer id
        {
            string filedata = File.ReadAllText(fileName);
            var fileObj = JObject.Parse((string)filedata);           //convert as json object
            var BuyerJson = fileObj.GetValue("BuyerInformation").ToString();      //convert data as string
            var ds = JsonConvert.DeserializeObject<List<BuyerInformation>>(BuyerJson);//deserialize BuyerJason data and put it in list
            var exists = ds.Find(x => x.ID == inputId);//check id is exists or not
            if (exists!=null)
            {
                MessageBox.Show($"ID - {exists.ID} exists\nTry with different Id", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            Shows.Show();
            this.Close();

            Shows.Showdata();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

      

        public void AllClear()//build this method to clear all box ...call it when it will be needed
        {
            txtID.Clear();
            Cmb_Title.SelectedIndex = -1;
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtcompany.Clear();
            txtDesignation.Clear();
            txtPhone.Clear();
            txt_Country.Clear();
            Cmb_Gender.SelectedIndex = -1;
            ImageShow.Source = DefaultImage;
            txtID.IsEnabled = true;
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();                                    //creat instance to open file dialog box to find and select image for uploging
            fd.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png;";       //filter which format will support
            fd.Title = "Select an Image";
            if (fd.ShowDialog().Value == true)
            {
                ImageShow.Source = new BitmapImage(new Uri(fd.FileName));
                TempImageFile = new FileInfo(fd.FileName);

                MessageBox.Show("Image"+TempImageFile.Extension);
            }
            else
            {
                MessageBox.Show("Upload Cancelled");
            }
        }
    }
}
