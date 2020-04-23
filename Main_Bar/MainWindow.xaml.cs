using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAppBar;

namespace Main_Bar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AppBarWindow
    {
        private string email = string.Empty;
        private string password = string.Empty;
        private System.Windows.Media.Color current_hours;
        private System.Windows.Media.Color old_hours;
        private string CNP = string.Empty;
        private string timp_pauza_old="00:00";
        private int number_of_accounts;
        private string key = "4ccd407cdae29b2b2f56de1be88cae08";//"4ccd407cdae29b2b2f56de1be88cae08" ?? 
        private Thread backgorundThread;
        public MainWindow()
        {
            String thisprocessname = Process.GetCurrentProcess().ProcessName;
            this.DockMode = AppBarDockMode.Right;
            
            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
            {
                this.Close();
            }
            else
                InitializeComponent();
            
        }

       private double getSettingsRadius()
        {
            return System.Windows.SystemParameters.WorkArea.Height / 45;
        }
        
        //we change the pictures in the buttons and the profilepic with the base image here
        //we center the App in the TopRight corner
        private void AppBarWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //this.Height = desktopWorkingArea.Height;


            //we set profile to basic standard image
            //we have exception for when the file is not the or is used by another process
            try
            {
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("baseimage.jpg", UriKind.Relative));
                ProfilePicture.Fill = myBrush;
            }
            catch (AccessViolationException)
            {
                MessageBox.Show("It looks like we cannot upload the standard picture because it used by another process");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Please put the baseimage.jpg picture in the same folder as the App");
            }
           


            //set image on home button
            //we have exception for when the file is not the or is used by another process
            try
            {
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("home.png", UriKind.Relative));
                btn_Home.Background = myBrush;
            }
            catch (AccessViolationException)
            {
                MessageBox.Show("It looks like we cannot upload the Home picture because it used by another process");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Please put the home.png picture in the same folder as the App");
            }


            //set image on home button
            //we have exception for when the file is not the or is used by another process
            try
            {
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("Jcam.png", UriKind.Relative));
                btn_JasminCam.Background = myBrush;
            }
            catch (AccessViolationException)
            {
                MessageBox.Show("It looks like we cannot upload the Jasmin Cam picture because it used by another process");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Please put the Jcam.png picture in the same folder as the App");
            }



            //set image on home button
            //we have exception for when the file is not the or is used by another process
            try
            {
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri("settings.png", UriKind.Relative));
                btn_Settings.Background = myBrush;
            }
            catch (AccessViolationException)
            {
                MessageBox.Show("It looks like we cannot upload the settings picture because it used by another process");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Please put the settings.png picture in the same folder as the App");
            }
            /*
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt"))
            {
                string newcnp = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt").Split('\r')[0];
                this.setCNP(newcnp);

            }
            else
            {
            */
            if (this.Height >= System.Windows.SystemParameters.WorkArea.Height)
            {
                btn_Settings.Height = System.Windows.SystemParameters.WorkArea.Height / 25;
                btn_Settings.Width = System.Windows.SystemParameters.WorkArea.Height / 25;

                btn_Home.Height = System.Windows.SystemParameters.WorkArea.Height / 20;
                btn_Home.Width = System.Windows.SystemParameters.WorkArea.Height / 20;

                btn_JasminCam.Height = System.Windows.SystemParameters.WorkArea.Height / 20;
                btn_JasminCam.Height = System.Windows.SystemParameters.WorkArea.Height / 20;

                indicator_traffic_bun.Height = System.Windows.SystemParameters.WorkArea.Height / 17;
                indicator_traffic_bun.Width = System.Windows.SystemParameters.WorkArea.Height / 17;

                indicator_traffic_bun.Opacity = 0.8;

            }

            indicator_traffic_bun.Width = indicator_traffic_bun.Height;
            btn_JasminCam.Width = btn_JasminCam.Height;
            btn_Settings.Width = btn_Settings.Height;
            btn_Home.Width = btn_Home.Height;
            LoginWindow login = new LoginWindow(this);
            login.Show();
            //}


        }

        //gets the modelinfo and adds them to the interface la income
        private void ModelInfo()
        {
            string ModelInfo_now = string.Empty;
            try
            {
                ModelInfo_now = Horex_Servers_Api_calls.GetModelInfo(key, CNP, Horex_Servers_Api_calls.GetPerioadaActuala());

                string name = ModelInfo_now.Split('"')[9];//era 9 numele
                
                if (name.Contains(" "))
                    Dispatcher.Invoke(() => txt_hello.Text = name.Split(' ')[0]);
                else
                    Dispatcher.Invoke(() => txt_hello.Text = name);

                name = ModelInfo_now.Split('"')[13];
                Dispatcher.Invoke(() => txt_girl_name.Text = name);
                Dispatcher.Invoke(() => txt_azi_incasari_actual.Text = ModelInfo_now.Split('"')[41]+'$');
                Dispatcher.Invoke(() => ellipsa_azi_incasari_actual.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetCurrentIncomeColors(ModelInfo_now.Split('"')[41])));

                Dispatcher.Invoke(() => txt_azi_ore_anterior.Text = ModelInfo_now.Split('"')[37]);
                Dispatcher.Invoke(() => ellipsa_azi_ore_anterior.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetCurrentHourColors(ModelInfo_now.Split('"')[37])));
                email = ModelInfo_now.Split('"')[51];
                password = ModelInfo_now.Split('"')[55];
                double pauza = 0;
                if (ModelInfo_now.Contains("offline"))
                {
                    string onlinedate = ModelInfo_now.Split('"')[25];
                    pauza = Horex_Servers_Api_calls.GetPauza(onlinedate, Convert.ToDouble(ModelInfo_now.Split('"')[33]));//aici facem pauza
                    double hours = Math.Truncate(pauza / 3600);
                    double mnutes = Math.Round(Convert.ToDouble((decimal)pauza / 3600) * 60);
                    if (mnutes > 60)
                    {
                        // hours += Math.Round(mnutes / 60);
                        mnutes = mnutes - Math.Round(mnutes / 60) * 60;
                    }
                    timp_pauza_old = hours.ToString() + ":" + mnutes.ToString();
                }
                Dispatcher.Invoke(() => txt_pauza.Text =timp_pauza_old);
                Dispatcher.Invoke(() => ellipsa_pauza.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetPauzaColors(pauza)));
            }
            catch (IndexOutOfRangeException)
            {

                MessageBox.Show("Server Response to ModelInfo was bad here is the reponsestring:" + ModelInfo_now);
            }

        }
        //gets cartonase,online time,perioada and helps implement pauza
        private void Cartonase()
        {
            string Cartonase_now = "";
            try
            {

                Cartonase_now = Horex_Servers_Api_calls.GetCartonase(key, CNP, Horex_Servers_Api_calls.GetPerioadaActuala());
                Dispatcher.Invoke(() => txt_ore_actuale.Text = Cartonase_now.Split('"')[7]);//set ore azi
                Dispatcher.Invoke(() => ellipse_ore_actual.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetColorsOre(Cartonase_now.Split('"')[7], true)));

                Dispatcher.Invoke(() => txt_incasari_actual.Text = Cartonase_now.Split('"')[11]+'$');//incasari actual
                Dispatcher.Invoke(() => ellipsa_incasari_actual.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetColorsIncasari(Cartonase_now.Split('"')[11], true)));


                Dispatcher.Invoke(() => txt_cartonase.Text = Cartonase_now.Split('"')[14].Substring(1, 1));//set cartonase 
                Dispatcher.Invoke(() => ellipsa_cartonase.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetCartonaseColors(Cartonase_now.Split('"')[14].Substring(1, 1))));

                // media color
                current_hours = Horex_Servers_Api_calls.GetColorsOre(Cartonase_now.Split('"')[7], true);//set color foir traffic bun
            }
            catch (IndexOutOfRangeException)
            {

                MessageBox.Show("Server response to cartonase_now too short" + Cartonase_now);

            }
            string Cartonase_old = " ";
            try
            {
                Cartonase_old = Horex_Servers_Api_calls.GetCartonase(key, CNP, Horex_Servers_Api_calls.GetPerioadaAnterioara());
                Dispatcher.Invoke(() => txt_ore_antecedent.Text = Cartonase_old.Split('"')[7]);//set ore azi
                Dispatcher.Invoke(() => ellipse_ore_antecedent.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetColorsOre(Cartonase_old.Split('"')[7], false)));

                Dispatcher.Invoke(() => txt_incasari_anterior.Text = Cartonase_old.Split('"')[11]+'$');//incasari actual
                Dispatcher.Invoke(() => ellipsa_incasari_anterior.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetColorsIncasari(Cartonase_old.Split('"')[11], false)));


                CalcDolarPerHour(Cartonase_now, Cartonase_old);

                old_hours = Horex_Servers_Api_calls.GetColorsOre(Cartonase_old.Split('"')[7], false);//set color foir traffic bun
            }
            catch (IndexOutOfRangeException)
            {

                MessageBox.Show("Server response to cartonase_old too short" + Cartonase_now);

            }
            catch (FormatException)
            {
                old_hours = Horex_Servers_Api_calls.GetColorsOre("130", false);//consideram perioada anterioara verde pentru ore
                CalcDolarPerHour(Cartonase_now, Cartonase_now);//calculam perioada actuala de 2 ori cel mai bine
            }
        }

        //implements bounce rate for both periods
        private void BounceRate()
        {
            string Bopunce_now = string.Empty;
            try
            {
                Bopunce_now = Horex_Servers_Api_calls.GetBounceRate(key, CNP, 1);
                if (Bopunce_now.Contains("BounceRate"))
                {
                    number_of_accounts = 0;
                    Double max = 0;
                    for (int i = 0; i < Math.Round(Convert.ToDouble((Bopunce_now.Split('"').Length - 18) / 20)); i++)
                    {
                        try
                        {
                            var temp = Convert.ToDouble(Bopunce_now.Split('"')[15 + i * 34]);
                            if (temp > max && temp < 2)
                                max = temp;
                            number_of_accounts++;
                        }
                        catch
                        {

                        }
                    }
                    Dispatcher.Invoke(() => txt_bounce_rate_actual.Text = (Math.Round(max, 4) * 100).ToString() + "%");
                    Dispatcher.Invoke(() => ellipsa_bounce_rate_actual.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetBounceColors(Math.Round(max, 4))));
                }
                else
                    Dispatcher.Invoke(() => ellipsa_bounce_rate_actual.Fill = new SolidColorBrush(System.Windows.Media.Colors.DarkRed));

            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Couldn't get BounceRate now because response was too short: " + Bopunce_now);
            }

            string Bopunce_old = string.Empty;
            try
            {
                Bopunce_old = Horex_Servers_Api_calls.GetBounceRate(key, CNP, 2);
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
                    Dispatcher.Invoke(() => txt_bounce_rate_anterior.Text = (Math.Round(max, 4) * 100).ToString() + "%");
                    Dispatcher.Invoke(() => ellipsa_bounce_rate_anterior.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetBounceColors(Math.Round(max, 4))));

                }
                else
                    Dispatcher.Invoke(() => ellipsa_bounce_rate_anterior.Fill = new SolidColorBrush(System.Windows.Media.Colors.DarkRed));
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Couldn't get BounceRate old because response was too short: " + Bopunce_old);
            }
            catch (FormatException)
            {

            }

        }
        //changes the color of the Traffic ubn indicator we need crossthread acces
        //e huge dar e cam ca un switch ciudat
        //ar trebuii switch sa nu mai fie assa redunant

        private void CalcDolarPerHour(string Cartonase_now, string Cartonase_old)
        {
            double dummy = (Convert.ToDouble(Cartonase_now.Split('"')[3]) / 3600);
            double incasari_pe_ora_new;
            if (dummy > 0)
            {
                incasari_pe_ora_new = Convert.ToDouble(Cartonase_now.Split('"')[11]) / dummy;
                incasari_pe_ora_new = Math.Round(incasari_pe_ora_new, 2);
                Dispatcher.Invoke(() => txt_incasari_medii_actual.Text = incasari_pe_ora_new.ToString() + "$/H");
                Dispatcher.Invoke(() => ellipsa_incasari_medii_actual.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetColorsDOlarPerHOur(incasari_pe_ora_new.ToString())));
            }
            else
            {
                Dispatcher.Invoke(() => txt_incasari_medii_actual.Text = "0$/H");
                Dispatcher.Invoke(() => ellipsa_incasari_medii_actual.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetColorsDOlarPerHOur(0.ToString())));
            }
            dummy = (Convert.ToDouble(Cartonase_old.Split('"')[3]) / 3600);
            if (dummy > 0)
            {
                double incasari_pe_ora_old = Convert.ToDouble(Cartonase_old.Split('"')[11]) / dummy;
                incasari_pe_ora_old = Math.Round(incasari_pe_ora_old, 2);

                Dispatcher.Invoke(() => txt_incasari_medii_anterior.Text = incasari_pe_ora_old.ToString() + "$/H");
                Dispatcher.Invoke(() => ellipsa_incasari_medii_anterior.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetColorsDOlarPerHOur(incasari_pe_ora_old.ToString())));
            }
            else
            {
                Dispatcher.Invoke(() => txt_incasari_medii_anterior.Text = "0$/H");
                Dispatcher.Invoke(() => ellipsa_incasari_medii_anterior.Fill = new SolidColorBrush(Horex_Servers_Api_calls.GetColorsDOlarPerHOur(0.ToString())));

            }
        }
        private void get_data_and_upload_interface()
        {
            if (!CNP.Equals(string.Empty))
            {
                //we set profile to basic standard image
                //we have exception for when the file is not the or is used by another process

                ModelInfo();
                Cartonase();
                BounceRate();
                try
                {
                        Dispatcher.Invoke(() => indicator_traffic_bun.Fill = Horex_Servers_Api_calls.GetIndicatorTrafficBun(old_hours, current_hours));
                }
                catch(FileNotFoundException)
                {
                    String s = "It looks like the App cannot find icon_traffic_.....png  please make sure that all 5 pictures are in the same folder as the Application";
                }
                catch(UnauthorizedAccessException)
                {
                    String s = "It looks like the app cannot acces the .png files ";
                }
                Thread.Sleep(1000 * 5);
                this.get_data_and_upload_interface();
            }
            /*
            else
            {
                               
              
                    LoginWindow login = new LoginWindow(this);
                    login.Show();
                
            }*/
        }
        public void setCNP(string CNP)
        {

            if (Thread.CurrentThread != backgorundThread && backgorundThread != null)
                backgorundThread.Abort();
            /*
            if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt"))
            {
                string[] dummy = { CNP };
                System.IO.File.WriteAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt", dummy);
            }
            else
            {
                var saveFile = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt");
                saveFile.Close();
                string[] dummy = { CNP };
                System.IO.File.WriteAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "\\CNP.txt", dummy);
            }*/
            this.CNP = CNP;
            Horex_Servers_Api_calls.GetProfilePIcture(key, CNP);


            try
            {
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                   Horex_Servers_Api_calls.LoadImage(AppDomain.CurrentDomain.BaseDirectory + "profilepicture.jpg");
                Dispatcher.Invoke(() => ProfilePicture.Fill = myBrush);

            }
            catch (AccessViolationException)
            {
                MessageBox.Show("It looks like we cannot upload the standard picture because it used by another process");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Please put the profilepicture.jpg picture in the same folder as the App");
            }
            if (Thread.CurrentThread != backgorundThread)
            {

                backgorundThread = new Thread(() => this.get_data_and_upload_interface());
                backgorundThread.Start();
            }

        }

        private void Btn_Settings_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow(this);
            login.Show();
        }

        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("chrome.exe", "https://horex.beststudios.ro/");

        }

        private void Btn_JasminCam_Click(object sender, RoutedEventArgs e)
        {
            //jcam.exe -pass "super secret" -user "user@user.com"
            if (!CNP.Equals(string.Empty))
            {
                if (number_of_accounts >= 2)
                {
                    //JCam.exe -noupdate -dev -secure -user "user@user.com" -pass "super secret"
                    ProcessStartInfo info = new ProcessStartInfo("cmd", "/C start C:\\Progra~1\\JasminCam\\App\\JCam.exe  -noupdate -dev -secure -user " + '"' + email + '"' + " -pass " + '"' + password + '"');
                    // ProcessStartInfo info = new ProcessStartInfo("cmd", "/C start C:\\Progra~1\\JasminCam\\App\\JCam.exe  -pass " +'"'+ password +'"'+ " -user " +'"'+ email+'"');
                    info.CreateNoWindow = true;
                    info.UseShellExecute = false;
                    Process.Start(info);

                     info = new ProcessStartInfo("cmd", "/C start C:\\Progra~1\\JasminCam\\App\\JCam.exe  -noupdate -dev -secure -user " + '"' + email + '"' + " -pass " + '"' + password + '"');
                    // ProcessStartInfo info = new ProcessStartInfo("cmd", "/C start C:\\Progra~1\\JasminCam\\App\\JCam.exe  -pass " +'"'+ password +'"'+ " -user " +'"'+ email+'"');
                    info.CreateNoWindow = true;
                    info.UseShellExecute = false;
                    Process.Start(info);
                }
                else
                {
                    ProcessStartInfo info = new ProcessStartInfo("cmd", "/C start C:\\Progra~1\\JasminCam\\App\\JCam.exe  -noupdate -dev -secure -user " + '"' + email + '"' + " -pass " + '"' + password + '"');
                    // ProcessStartInfo info = new ProcessStartInfo("cmd", "/C start C:\\Progra~1\\JasminCam\\App\\JCam.exe  -pass " +'"'+ password +'"'+ " -user " +'"'+ email+'"');
                    info.CreateNoWindow = true;
                    info.UseShellExecute = false;
                    Process.Start(info);
                }
            }
        }

        private void AppBarWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                backgorundThread.Abort();
            }
            catch (Exception)
            { MessageBox.Show("Couln't kill the background Thread"); }
        }
        public void BringToFront(Canvas pParent, TextBlock pToMove)
        {
            try
            {
                int currentIndex = Canvas.GetZIndex(pToMove);
                int zIndex = 0;
                int maxZ = 0;
                UserControl child;
                for (int i = 0; i < pParent.Children.Count; i++)
                {
                    if (pParent.Children[i] is UserControl &&
                        pParent.Children[i] != pToMove)
                    {
                        child = pParent.Children[i] as UserControl;
                        zIndex = Canvas.GetZIndex(child);
                        maxZ = Math.Max(maxZ, zIndex);
                        if (zIndex > currentIndex)
                        {
                            Canvas.SetZIndex(child, zIndex - 1);
                        }
                    }
                }
                Canvas.SetZIndex(pToMove, maxZ);
            }
            catch (Exception)
            {
               
            }
        }
    }
}
