using Microsoft.Win32;
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
    /// Interaction logic for Edit_Information.xaml
    /// </summary>
    public partial class Edit_Information : Window
    {
        public string fileName = @"BuyerInformation.json";
        public FileInfo TempImageFile { get; set; }
        public BitmapImage DefaultImage => new BitmapImage(new Uri(GetImagePath() + "default.jpg"));
        public string DefaultImagePath => GetImagePath() + "default.jpg";

        public FileInfo OldImageFile { get;  set; }

        public Edit_Information()
        {
            InitializeComponent();
            string[] titles = new string[] { "Mr.", "Mrs.", "Miss." };
            this.Cmb_Title.ItemsSource = titles;
            Cmb_Title.Text = titles[0];
            string[] gender = new string[] { "Male", "Female" };
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
        private string GetImagePath()//creat the path to save 
        {
            var AddinAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var AddinFolder = System.IO.Path.GetDirectoryName(AddinAssembly.Location);
            string ImagePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AddinFolder, @"..\..\Images\"));

            return ImagePath;
        }

      

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            var ID = int.Parse(txtID.Text);
            var Title = Cmb_Title.SelectedItem.ToString();
            var FirstName = txtFirstName.Text;
            var LastName = txtLastName.Text;
            var Gender = Cmb_Gender.SelectedItem.ToString();
            var Phone = txtPhone.Text;
            var Designation = txtDesignation.Text;
            var Company = txtcompany.Text;
            var Country = txt_Country.Text;
            var Email = txtEmail.Text;
            var JasonD = File.ReadAllText(fileName);
            var JasonObj = JObject.Parse(JasonD);
            var BuyerJason = JasonObj.GetValue("BuyerInformation").ToString();
            var Buyerlist = JsonConvert.DeserializeObject<List<BuyerInformation>>(BuyerJason);
            foreach (var item in Buyerlist.Where(x=>x.ID==ID))
            {
                item.Title = Title;
                item.FirstName = FirstName;
                item.LastName = LastName;
                item.Gender = Gender;
                item.Phone = Phone;
                item.Designation = Designation;
                item.Company = Company;
                item.Country = Country;
                item.Email = Email;
                OldImageFile = (item.ImageInformation != "default.jpg") ? new FileInfo(GetImagePath() + item.ImageInformation) : null;//check image is not default image then.....
                if (TempImageFile != null && OldImageFile == null)  
                {
                    TempImageFile.CopyTo(GetImagePath() + item.ID + TempImageFile.Extension); //Copy upload image to target directory
                    item.ImageInformation = item.ID + TempImageFile.Extension;
                    TempImageFile = null;
                }
                if (OldImageFile != null && TempImageFile != null && File.Exists(OldImageFile.FullName)) //Check if upload image not null && old image not null. Extra => check if old file exists in directory
                {
                    item.ImageInformation = item.ID + TempImageFile.Extension;
                    OldImageFile.Delete();      //Delete exists image
                    TempImageFile.CopyTo(GetImagePath() + ID + TempImageFile.Extension); //Copy upload image to target directory
                    TempImageFile = null;
                }

            }
            JArray buyerArray = JArray.FromObject(Buyerlist);
            JasonObj["BuyerInformation"] = buyerArray;
            string output = JsonConvert.SerializeObject(JasonObj, Formatting.Indented);
            File.WriteAllText(@"BuyerInformation.json", output);
            this.Close();
            Show ShowS = new Show();
            ShowS.Show();
            ShowS.Showdata();
            MessageBox.Show("Data Updated Successfully !!");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png;";
            fd.Title = "Select an Image";
            if (fd.ShowDialog().Value == true)
            {
                ImageShow.Source = new BitmapImage(new Uri(fd.FileName));
                TempImageFile = new FileInfo(fd.FileName);

                MessageBox.Show("Image"+TempImageFile.Extension);
            }
            else
            {
                MessageBox.Show("Uploading Cancel!!!!!");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Show show = new Show();
            show.Show();
            show.Showdata();
            this.Close();
        }
    }
}
