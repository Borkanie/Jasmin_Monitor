using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppBar;
namespace Jasmin_Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// here we create and change the graphical elements like the circles and the values
    /// </summary>
    public partial class MainWindow : AppBarWindow
    {
        //contructor
        public MainWindow()
        {
            InitializeComponent();
            loginForm = new Login(this);
            this.ResizeMode = ResizeMode.NoResize;
            //loginForm.Show();
            loginForm.Hide();
        }
        //here we initialize all fields
        #region fields
        private Login loginForm;//form that gets the email and password
        private List<Label> labellist = new List<Label>();//here we save all labels so that we can change values later
        private List<Ellipse> ellipselist = new List<Ellipse>();//save elipses to schange values
        private ItemsControl listBox;//canvas to change values
        private Thread Slave;//the thread that constantly changes them,5 min freeze time
        private string CNP = "2850203171690";//"2850203171690" it will be empty
        private string key = "4ccd407cdae29b2b2f56de1be88cae08";//"4ccd407cdae29b2b2f56de1be88cae08" ??
        private Ellipse baseimage;//remember the image currently displayed
        private bool checking_for_values = false;//this bool tells us if the program is running
        private System.Drawing.Color current_hours;//color to diplay traffic bun
        private System.Drawing.Color old_hours;//old color to display traffic bun we need them both
        private Button traffic_bun; // we need the button to change its color
        #endregion
        //here we load all interface objects(canvas,ellipse,label etc)
        #region loadin interface
        //adding the canvas with the values and the circles
        private void addbasicanvas(string text)
        {

            Canvas canvas = uicreatorclass.createCanvas(text, this.Width, this.Height / 20, this);
            listBox.Items.Add(canvas);
        }
        // adding the Hello Name text
        private void addtitle(string text)
        {
            Label label = uicreatorclass.createlabel(text, this.Height / 45);
            listBox.Items.Add(label);

        }
        //adding the image of the model
        private void addimage(Image image)
        {
            Ellipse ellipse = uicreatorclass.createProfilePicture(52, image);
            listBox.Items.Add(ellipse);
            baseimage = ellipse;
            ellipse.VerticalAlignment = VerticalAlignment.Center;
            ellipse.HorizontalAlignment = HorizontalAlignment.Center;

        }
        //adds label to list we need methob because threads
        public void addtolabellist(Label label)
        {
            labellist.Add(label);
        }
        //adds elipse to lsit we need method because of thread
        public void addtoelipselist(Ellipse ellipse)
        {
            ellipselist.Add(ellipse);
        }
        //creates buttons all of them
        private void addbuttons()
        {
            Button button = uicreatorclass.createbutton(this.Width / 2, this.Height / 20, 0);
            button.Click += Traffic_Bun_Click;
            traffic_bun = button;
            listBox.Items.Add(button);
            addrectangle(this.Width, 10, false);
            button = uicreatorclass.createbutton(this.Width / 2.5, this.Height / 20, 1);
            button.Click += Jasmin_Button_Click;
            listBox.Items.Add(button);
            addrectangle(this.Width, 10, false);
            button = uicreatorclass.createbutton(this.Width / 3, this.Height / 20, 3);
            button.Click += Home_Button_Click;
            listBox.Items.Add(button);
            addrectangle(this.Width, 10, false);
            button = uicreatorclass.createbutton(this.Width / 5, this.Height / 40, 2);
            button.Click += Settings_Button_Click;
            listBox.Items.Add(button);
            addrectangle(this.Width, 10, false);
        }
        //creates a rectangle where we put stuff in or fill the gaps
        private void addrectangle(double width, double height, bool black)
        {
            Rectangle rectangle = uicreatorclass.createRectangle(width, height, black);
            listBox.Items.Add(rectangle);
        }

        //initializing everything
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //setting the windows hight and width
            this.Height = SystemParameters.WorkArea.Height;
            this.Width = 120;
            //this.Top = SystemParameters.WorkArea.Top;
            //this.Left = SystemParameters.WorkArea.Width - 120;
            this.DockedWidthOrHeight += (int)(-100 / VisualTreeHelper.GetDpi(this).PixelsPerDip);
            //making the lisbtox and the gird maximum value
            grd_main.Width = this.Width;
            grd_main.Height = this.Height;
            //making a listbox to keep them nice and tight
            listBox = new ItemsControl();
            grd_main.Children.Add(listBox);
            listBox.Width = grd_main.Width;
            listBox.Height = grd_main.Height;
            //hiiding it
            this.ShowInTaskbar = false;
            addrectangle(this.Width, 5, false);
            //creating the componing elements

            Api_calstring.GetProfilePIcture(key, CNP);
            Image image = new Image();
            //Uri imageUri = new Uri("C:\\Users\\USER\\Desktop\\captcha2.jpg", UriKind.Absolute);
            Uri imageUri = new Uri("baseimage.jpg", UriKind.Relative);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            image.Source = imageBitmap;
            addimage(image);//adds the iamge

            addtitle("Hello" + '\n' + "Alisa");//adds the hello+ na e text

            addrectangle(this.Width, 5, false);
            addrectangle(this.Width, 1, true);
            addtitle("ore");
            addbasicanvas("0");
            addbasicanvas("0");

            addrectangle(this.Width, 5, false);
            addrectangle(this.Width, 1, true);
            addtitle("incasari");
            addbasicanvas("0$");
            addbasicanvas("0$");

            addrectangle(this.Width, 5, false);
            addrectangle(this.Width, 1, true);
            addtitle("$/H");
            addbasicanvas("0$");
            addbasicanvas("0$");

            addrectangle(this.Width, 5, false);
            addrectangle(this.Width, 1, true);
            addtitle("Bounce" + '\n' + "Rate");
            addbasicanvas("0%");
            addbasicanvas("0%");

            addrectangle(this.Width, 5, false);
            addrectangle(this.Width, 1, true);
            addtitle("AZI");
            addbasicanvas("0$");
            addbasicanvas("00:00");
            addtitle("pauza");
            addbasicanvas("00:00");

            addrectangle(this.Width, 5, false);
            addrectangle(this.Width, 1, true);
            addtitle("cartonase");
            addbasicanvas("0");
            addrectangle(this.Width, 5, false);
            addrectangle(this.Width, 1, true);
            addrectangle(this.Width, 15, false);
            addbuttons();
            labellist[0].Content = labellist.Count;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt"))
            {
                string newcnp = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt").Split('\r')[0];
                setCNP(newcnp);
              
            }
            else
                loginForm.Show();
           
        }
        #endregion
        //here we get the values trough requests and set them on the UI
        #region Get_data_adn_upload_GUI
        //calculates income in dolars per Hour
        private void CalcDolarPerHour(string Cartonase_now, string Cartonase_old)
        {
            double dummy = (Convert.ToDouble(Cartonase_now.Split('"')[3]) / 3600);
            double incasari_pe_ora_new;
            if (dummy > 0)
            {
                incasari_pe_ora_new = Convert.ToDouble(Cartonase_now.Split('"')[11]) / dummy;
                incasari_pe_ora_new = Math.Round(incasari_pe_ora_new, 2);
                ChangeTextInLabel(4, incasari_pe_ora_new.ToString());
                ChangeIndicatorColor(4, Api_calstring.GetColorDOlarPerHOur(incasari_pe_ora_new.ToString()));
            }
            else
            {
                ChangeTextInLabel(4, "0");
                ChangeIndicatorColor(4, Api_calstring.GetColorDOlarPerHOur(0.ToString()));
            }
            dummy = (Convert.ToDouble(Cartonase_old.Split('"')[3]) / 3600);
            if (dummy > 0)
            {
                double incasari_pe_ora_old = Convert.ToDouble(Cartonase_old.Split('"')[11]) / dummy;
                incasari_pe_ora_old = Math.Round(incasari_pe_ora_old, 2);
                ChangeTextInLabel(5, incasari_pe_ora_old.ToString());
                ChangeIndicatorColor(5, Api_calstring.GetColorDOlarPerHOur(incasari_pe_ora_old.ToString()));
            }
            else
            {
                ChangeTextInLabel(5, "0");
                ChangeIndicatorColor(5, Api_calstring.GetColorDOlarPerHOur(0.ToString()));

            }
        }
        //gets the modelinfo and adds them to the interface la income
        private void ModelInfo()
        {
            try
            {
                string ModelInfo_now = Api_calstring.GetModelInfo(key, CNP, Api_calstring.GetPerioadaActuala());
                ChangeTextInLabel(8, ModelInfo_now.Split('"')[41]);
                ChangeIndicatorColor(8, Api_calstring.GetCurrentIncomeColor(ModelInfo_now.Split('"')[41]));
                ChangeTextInLabel(9, ModelInfo_now.Split('"')[37]);

                ChangeIndicatorColor(9, Api_calstring.GetCurrentHourColor(ModelInfo_now.Split('"')[37]));
                string onlinedate = ModelInfo_now.Split('"')[25];
                double pauza = Api_calstring.GetPauza(onlinedate);//aici facem pauza
                ChangeTextInLabel(10, Convert.ToString(Math.Round(pauza / 3600)) + ":" + Convert.ToString(Math.Round((pauza - 3600 * Math.Round(pauza / 3600)) / 60)));
                ChangeIndicatorColor(10, Api_calstring.GetPauzaCOlor(pauza));
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                MessageBox.Show("Couldn't get ModelInfo" + s);
            }

        }
        //gets cartonase,online time,perioada and helps implement pauza
        private void Cartonase()
        {
            string Cartonase_now = "";
            try
            {
                Cartonase_now = Api_calstring.GetCartonase(key, CNP, Api_calstring.GetPerioadaActuala());
                ChangeTextInLabel(0, Cartonase_now.Split('"')[7]);//set ore
                ChangeIndicatorColor(0, Api_calstring.GetColorOre(Cartonase_now.Split('"')[7]));
                ChangeTextInLabel(2, Cartonase_now.Split('"')[11]);//set incasari
                ChangeIndicatorColor(2, Api_calstring.GetColorIncasari(Cartonase_now.Split('"')[11]));
                ChangeTextInLabel(11, Cartonase_now.Split('"')[14].Substring(1, 1));
                ChangeIndicatorColor(11, Api_calstring.GetCartonaseColor(Cartonase_now.Split('"')[14].Substring(1, 1)));


                current_hours = Api_calstring.GetColorOre(Cartonase_now.Split('"')[7]);//set color foir traffic bun
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                MessageBox.Show("Couldn't get Cartonase" + s);

            }
            try
            {
                string Cartonase_old = Api_calstring.GetCartonase(key, CNP, Api_calstring.GetPerioadaAnterioara());
                ChangeTextInLabel(1, Cartonase_old.Split('"')[7]);//set ore
                ChangeIndicatorColor(1, Api_calstring.GetColorOre(Cartonase_old.Split('"')[7]));
                ChangeTextInLabel(3, Cartonase_old.Split('"')[11]);//set ioncasari
                ChangeIndicatorColor(3, Api_calstring.GetColorIncasari(Cartonase_old.Split('"')[11]));
                CalcDolarPerHour(Cartonase_now, Cartonase_old);

                old_hours = Api_calstring.GetColorOre(Cartonase_old.Split('"')[7]);//set color foir traffic bun
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                MessageBox.Show("Couldn't get Cartonase_old" + s);
            }
        }
        //implements bounce rate for both periods
        private void BounceRate()
        {
            try
            {
                string Bopunce_now = Api_calstring.GetBounceRate(key, CNP, 1);
                if (Bopunce_now.Contains("BounceRate"))
                {
                    Double max = 0;
                    for (int i = 0; i < Math.Round(Convert.ToDouble((Bopunce_now.Split('"').Length - 18) / 20)); i++)
                    {
                        try
                        {
                            var temp = Convert.ToDouble(Bopunce_now.Split('"')[15 + i * 34]);
                            if (temp > max && temp < 2)
                                max = temp;
                        }
                        catch
                        {

                        }
                    }
                    ChangeTextInLabel(6, Math.Round(max, 4).ToString());
                    ChangeIndicatorColor(6, Api_calstring.GetBounceCOlor(Math.Round(max, 4)));
                }
                else
                    ChangeIndicatorColor(6, System.Drawing.Color.DarkRed);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                MessageBox.Show("Couldn't get BounceRate" + s);
            }
            //string ModelInfo_old = Api_calstring.GetModelInfo(key, CNP, Api_calstring.GetPerioadaAnterioara());

            try
            {
                string Bopunce_old = Api_calstring.GetBounceRate(key, CNP, 2);
                if (Bopunce_old.Contains("BounceRate"))
                {
                    Double max = 0;
                    for (int i = 0; i < Math.Round(Convert.ToDouble((Bopunce_old.Split('"').Length - 18) / 20)); i++)
                    {
                        try
                        {
                            var temp = Convert.ToDouble(Bopunce_old.Split('"')[15 + i * 34]);
                            if (temp > max && temp < 2)
                                max = temp;
                        }
                        catch
                        {

                        }
                    }
                    ChangeTextInLabel(7, Math.Round(max, 4).ToString());
                    ChangeIndicatorColor(7, Api_calstring.GetBounceCOlor(Math.Round(max, 4)));
                }
                else
                    ChangeIndicatorColor(7, System.Drawing.Color.DarkRed);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                MessageBox.Show("Couldn't get Bounce_old" + s);
            }

        }
        //changes a elbel we need Acces crossthread
        private void ChangeTextInLabel(int labelnumber, string value)
        {

            if (!CheckAccess())
            {
                // On a different thread
                Dispatcher.Invoke(() => labellist[labelnumber].Content = value);

            }
            else
                labellist[labelnumber].Content = value;

        }
        //chages a circle we need crossthread acces
        private void ChangeIndicatorColor(int indicatornumber, System.Drawing.Color Newcolor)
        {
            System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(Newcolor.A, Newcolor.R, Newcolor.G, Newcolor.B);
            if (!CheckAccess())
            {
                // On a different thread
                Dispatcher.Invoke(() => ellipselist[indicatornumber].Fill = new SolidColorBrush(color));

            }
            else
                ellipselist[indicatornumber].Fill = new SolidColorBrush(color);

        }
        //changes the color of the Traffic ubn indicator we need crossthread acces
        private void Traffic_Bun_Color()
        {
            //Red
            #region perioada antecedenta rosie
            if (old_hours == System.Drawing.Color.Red)
            {
                if (current_hours == System.Drawing.Color.DarkGreen)
                {
                    if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                    {
                        Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.OrangeRed);

                    }
                    else
                        traffic_bun.Background = System.Windows.Media.Brushes.OrangeRed;//orange
                }

                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {
                    Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.Red);

                }
                else
                    traffic_bun.Background = System.Windows.Media.Brushes.Red;//red
                return;
            }

            #endregion

            //Orange
            #region perioada antecedenta protocalie
            if (old_hours == System.Drawing.Color.Orange)
            {
                if (current_hours == System.Drawing.Color.Red)
                {
                    if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                    {
                        Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.Red);

                    }
                    else
                        traffic_bun.Background = System.Windows.Media.Brushes.Red;//red
                    return;
                }
                if (current_hours == System.Drawing.Color.Green)
                {

                    if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                    {
                        Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.Yellow);

                    }
                    else
                        traffic_bun.Background = System.Windows.Media.Brushes.Yellow;//yellow
                    return;
                }
                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {
                    Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.OrangeRed);

                }
                else
                    traffic_bun.Background = System.Windows.Media.Brushes.OrangeRed;//orange
            }
            #endregion

            //Yellow
            #region perioada antecedenta galebena
            if (old_hours == System.Drawing.Color.Yellow)
            {
                if (current_hours == System.Drawing.Color.Red)
                {
                    if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                    {
                        Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.Red);

                    }
                    else
                        traffic_bun.Background = System.Windows.Media.Brushes.Red; 
                    return;
                }
                if (current_hours == System.Drawing.Color.Orange)
                {
                    if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                    {
                        Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.OrangeRed);

                    }
                    else
                        traffic_bun.Background = System.Windows.Media.Brushes.OrangeRed;
                    return;
                }
                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {
                    Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.Yellow);

                }
                else
                    traffic_bun.Background = System.Windows.Media.Brushes.Yellow;
            }
            #endregion

            //LightGreen
            #region perioada antecedenta verde deschis
            if (old_hours == System.Drawing.Color.LightGreen)
            {
                if (current_hours == System.Drawing.Color.Red)
                {
                    if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                    {
                        Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.Red);

                    }
                    else
                        traffic_bun.Background = System.Windows.Media.Brushes.Red;
                    return;
                }
                if (current_hours == System.Drawing.Color.Orange)
                {
                    if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                    {
                        Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.OrangeRed);

                    }
                    else
                        traffic_bun.Background = System.Windows.Media.Brushes.OrangeRed;
                    return;
                }
                if (current_hours == System.Drawing.Color.Yellow)
                {
                    if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                    {
                        Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.Yellow);

                    }
                    else
                        traffic_bun.Background = System.Windows.Media.Brushes.Yellow;
                    return;
                }

                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {
                    Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.LightGreen);

                }
                else
                    traffic_bun.Background = System.Windows.Media.Brushes.LightGreen;

            }
            #endregion

            //Green
            #region perioada antecedenta verde
            if (current_hours == System.Drawing.Color.Red)
            {
                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {
                    Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.Red);

                }
                else
                    traffic_bun.Background = System.Windows.Media.Brushes.Red;
                return;
            }
            if (current_hours == System.Drawing.Color.Orange)
            {
                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {
                    Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.OrangeRed);

                }
                else
                    traffic_bun.Background = System.Windows.Media.Brushes.OrangeRed;
                return;
            }
            if (current_hours == System.Drawing.Color.Yellow)
            {
                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {
                    Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.Yellow);

                }
                else
                    traffic_bun.Background = System.Windows.Media.Brushes.Yellow;
                return;
            }
            if (current_hours == System.Drawing.Color.LightGreen)
            {
                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {
                    Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.LightGreen);

                }
                else
                    traffic_bun.Background = System.Windows.Media.Brushes.LightGreen;

                return;
            }
            if (current_hours == System.Drawing.Color.Green)
            {
                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {
                    Dispatcher.Invoke(() => traffic_bun.Background = System.Windows.Media.Brushes.Green);

                }
                else
                    traffic_bun.Background = System.Windows.Media.Brushes.Green;
                return;
            }
            #endregion

        }

        private void Get_Data_And_Upload_GUI()
        {
            if (CNP != "")
            {
                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {
                    Dispatcher.Invoke(() => ChangeProfilePicture());

                }
                else
                    ChangeProfilePicture();
                ModelInfo();
                Cartonase();
                BounceRate();
                Traffic_Bun_Color();
                Thread.Sleep(1000 * 300);
                this.Get_Data_And_Upload_GUI();
            }
            else
            {
                if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
                {

                    Dispatcher.Invoke(() => loginForm.Show());
                }
                else
                    loginForm.Show();
            }

        }

        public void setCNP(string CNP)
        {
            try
            {
                Slave.Abort();
            }
            catch { }
            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt"))
            {
                string[] dummy = { CNP };
                File.WriteAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt", dummy);
            }
            else
            {
                var saveFile = File.Create(System.AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt");
                saveFile.Close();
                string[] dummy = { CNP };
                File.WriteAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt", dummy);
            }
            
            this.CNP = CNP;
            Slave = new Thread(this.Get_Data_And_Upload_GUI);
            Slave.Start();
        }
        private void ChangeProfilePicture()
        {
            try
            {

                listBox.Items.Remove(baseimage);

                Api_calstring.GetProfilePIcture(key, CNP);
                Image image = new Image();
                //Uri imageUri = new Uri("C:\\Users\\USER\\Desktop\\captcha2.jpg", UriKind.Absolute);
                Uri imageUri = new Uri("profilepicture.jpg", UriKind.Relative);
                BitmapImage imageBitmap = new BitmapImage(imageUri);
                image.Source = imageBitmap;

                Ellipse ellipse = uicreatorclass.createProfilePicture(52, image);
                ellipse.BringIntoView();
                listBox.Items.Insert(1, ellipse);
                ellipse.VerticalAlignment = VerticalAlignment.Center;
                ellipse.HorizontalAlignment = HorizontalAlignment.Center;
                baseimage = ellipse;
                image = null;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                MessageBox.Show("Couldn't get Image" + s);
            };
        }

        #endregion
        //the events that are separated mainly buttons
        #region events
        //currently empty will implement a settings form
        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            loginForm.Show();
            // throw new NotImplementedException();
        }
        //majking sure it dies
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Slave.Abort();
            this.OnClosing(e);

        }
        //currently empty will navigate to the horex webpage
        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("chrome.exe", "https://horex.beststudios.ro/");
            //  throw new NotImplementedException();
        }
        //starts a jasminCam app i doesnt fill in the username and password yet
        private void Jasmin_Button_Click(object sender, RoutedEventArgs e)
        {
            //  throw new NotImplementedException();
            if (checking_for_values == false)
            {
                Process jasmin_App = new Process();
                jasmin_App.StartInfo.FileName = "C:\\Program Files\\JasminCam\\App\\JCam.exe";
                jasmin_App.StartInfo.Arguments = "email=" + Api_calstring.Email + ";password=" + Api_calstring.Password;
                //jasmin_App.StartInfo.Arguments = Api_calstring.Email + "," + Api_calstring.Password;
                jasmin_App.Start();
                checking_for_values = true;
                jasmin_App.Exited += JasminApp_Exited;
            }
        }
        //tells us that the JasminCam is clsoed
        private void JasminApp_Exited(object sender, EventArgs e)
        {
            checking_for_values = false;
        }
        //does nothing Traffic bun is an indicator maybe later it will have a function
        private void Traffic_Bun_Click(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException();
        }
        #endregion
    }
    
}
