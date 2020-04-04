using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;

namespace Jasmin_Monitor
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// This window will be the login window Inputs,user and password and w eget CNp and key 
    /// </summary>
    
    public partial class Login : Window
    {
        //constructor gets main to pass down CNP and it creates Eventhadnles
        public Login(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            txtbox_password.MouseDown += Txtbox_password_MouseDoubleClick;
            txtbox_Username.MouseDown += Txtbox_Username_MouseDoubleClick;
            Done.Content = "Done";
            Done.Click += Done_Click;
        }
        //we need main to pass down the CNP and password
        private MainWindow main;
        //all the events ar ehere some dont work
        #region events
        //calls api and sets CNP in main 
        //main.setCNP also start slave and makes it update the interface
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox_password.Text != "" && txtbox_Username.Text != "" && txtbox_Username.Text != "Username..." && txtbox_password.Text != "Password...")
            {
                //login API call
                try
                {
                    string Uri = "https://horex.beststudios.ro/v1/index.php?key=4ccd407cdae29b2b2f56de1be88cae08&action=modelCNP&login="+txtbox_Username.Text+"&password="+txtbox_password.Text;
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
                    }

                    //
                    main.setCNP(CNP);
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wrong password or username prease try again");
                }
            }
            else
                MessageBox.Show("Please complete the inputs");
        }
        //doesnt work should empty the bos at doubleclick
        private void Txtbox_Username_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (txtbox_password.Text == "Username...")
                txtbox_password.Clear();
        }
        //doesnt work should empty the bos at doubleclick
        private void Txtbox_password_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (txtbox_password.Text == "Password...")
                txtbox_password.Clear();

        }
        #endregion
    }
}
