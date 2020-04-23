using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Main_Bar
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MainWindow main;
        public LoginWindow(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void Btn_done_Loaded(object sender, RoutedEventArgs e)
        {

            //we set profile to basic standard image
            //we have exception for when the file is not the or is used by another process
            try
            {
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("baseimage.jpg", UriKind.Relative));
                picture.Fill = myBrush;
            }
            catch (AccessViolationException)
            {
                MessageBox.Show("It looks like we cannot upload the standard picture because it used by another process");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Please put the baseimage.jpg picture in the same folder as the App");
            }
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_done_Click(object sender, RoutedEventArgs e)
        {
           
            if (!txtbox_Password.Password.Equals(string.Empty) && !txtbox_Username.Text.Equals(string.Empty))
            {
                //login API call
                try
                {
                    string Uri = "https://horex.beststudios.ro/v1/index.php?key=4ccd407cdae29b2b2f56de1be88cae08&action=modelCNP&login=" + txtbox_Username.Text + "&password=" + txtbox_Password.Password;
                    HttpWebRequest httprequest = (HttpWebRequest)WebRequest.Create(Uri);
                    X509Store store2 = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    store2.Open(OpenFlags.ReadOnly);
                    X509Certificate2Collection collection2 = store2.Certificates.Find(X509FindType.FindBySubjectName, "Horex Client Best Studios", true);
                    httprequest.ClientCertificates = collection2;
                    httprequest.Method = "POST";
                    string CNP;
                    // returned values are returned as a stream, then read into a string
                    HttpWebResponse Response = (HttpWebResponse)httprequest.GetResponse();
                    using (StreamReader ResponseStream = new StreamReader(Response.GetResponseStream()))
                    {

                        CNP = ResponseStream.ReadToEnd();

                        ResponseStream.Close();

                        //MessageBox.Show(CNP);
                        //GaliceanuJ5
                    }

                    //  

                    if (CNP.Contains("Login"))
                    {
                        CNP = CNP.Split('"')[5];
                        main.setCNP(CNP);
                        this.Hide();
                    }
                    else
                        MessageBox.Show("Incorrect Username or password " + CNP);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("error when connecting to server: " + ex.Message);
                }
            }
            else
                MessageBox.Show("Please complete the inputs");
        }
    }
}
