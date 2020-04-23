using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Main_Bar
{
    class Horex_Servers_Api_calls
    {
        private Horex_Servers_Api_calls() { }

        //we use this guy to make sure we rfree the image form the meory

        /// <summary>
        /// campuri pe care le folosim in API calls
        /// </summary>
        #region fields
        static public string Email { get; }
        static public string Password { get; }
        static private int perioada = 2;
        #endregion
        /// <summary>
        /// luam de pe naet datele
        /// </summary>

        #region get_online_data
        static public void SetPerioada(int perioad)
        {
            perioada = perioad;
        }
        static public BitmapImage LoadImage(string myImageFile)
        {
            try { 
            BitmapImage myRetVal = null;
            if (myImageFile != null)
            {
                BitmapImage image = new BitmapImage();
                using (System.IO.FileStream stream = System.IO.File.OpenRead(myImageFile))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                myRetVal = image;
            }
                return myRetVal;
            }
            catch(FileNotFoundException)
            {
              
            }
            return null;
        }
        static public void GetProfilePIcture(string key, string CNP)//gets the profile picture and saves it under "profilepicture.jpg"
        {

            string Uri = "https://horex.beststudios.ro/v1/index.php?key=" + key + "&action=profileImg&modelCNP=" + CNP;
            HttpWebRequest httprequest = (HttpWebRequest)WebRequest.Create(Uri);
            X509Store store2 = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            store2.Open(OpenFlags.ReadOnly);


            X509Certificate2Collection collection2 = store2.Certificates.Find(X509FindType.FindBySubjectName, "Horex Client Best Studios", true);

            httprequest.ClientCertificates = collection2;


            httprequest.Method = "POST";

            // returned values are returned as a stream, then read into a string
            using (HttpWebResponse lxResponse = (HttpWebResponse)httprequest.GetResponse())
            {
                using (System.IO.BinaryReader reader = new System.IO.BinaryReader(lxResponse.GetResponseStream()))
                {
                    if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "profilepicture.jpg"))
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "profilepicture.jpg");

                    Byte[] lnByte = reader.ReadBytes(1 * 1024 * 1024 * 10);
                    using (System.IO.FileStream lxFS = new System.IO.FileStream("profilepicture.jpg", System.IO.FileMode.Create))
                    {
                        lxFS.Write(lnByte, 0, lnByte.Length);
                    }
                }

                // System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                //  image.Source = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "profilepicture.jpg");

            }
        }
        static public string GetModelInfo(string key, string CNP, string date)//ia model info avem multe acolo 
        {
            string Uri = "https://horex.beststudios.ro/v1/index.php?key=" + key + "&action=modelInfo&modelCNP=" + CNP + date;
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Uri);
            X509Store store2 = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            store2.Open(OpenFlags.ReadOnly);


            X509Certificate2Collection collection2 = store2.Certificates.Find(X509FindType.FindBySubjectName, "Horex Client Best Studios", true);

            Request.ClientCertificates = collection2;


            Request.Method = "POST";

            // returned values are returned as a stream, then read into a string
            String stringResponse = string.Empty;
            HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            using (System.IO.StreamReader ResponseStream = new System.IO.StreamReader(Response.GetResponseStream()))
            {
                stringResponse = ResponseStream.ReadToEnd();
                ResponseStream.Close();
            }

            return stringResponse;
        }
        static public string GetCartonase(string key, string CNP, string date)//cartonase+perioada curenta
        {
            string Uri = "https://horex.beststudios.ro/v1/index.php?key=" + key + "&action=periodData&modelCNP=" + CNP + date;
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Uri);
            X509Store store2 = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            store2.Open(OpenFlags.ReadOnly);


            X509Certificate2Collection collection2 = store2.Certificates.Find(X509FindType.FindBySubjectName, "Horex Client Best Studios", true);

            Request.ClientCertificates = collection2;


            Request.Method = "POST";
            // returned values are returned as a stream, then read into a string
            String stringResponse = string.Empty;
            HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            using (System.IO.StreamReader ResponseStream = new System.IO.StreamReader(Response.GetResponseStream()))
            {
                stringResponse = ResponseStream.ReadToEnd();
                ResponseStream.Close();
            }
            return stringResponse;
        }
        static public string GetBounceRate(string key, string CNP, int scadem)//bouncerate si numarul de iduri
        {
            
            DateTime dataFormat = DateTime.Now;
            string dummy = dataFormat.ToString("MM/dd/yyyy HH:mm:ss").Split(' ')[0];
            string[] spliteddata;
            if (dummy.Contains("/"))
                spliteddata = dummy.Split('/');
            else
                spliteddata = dummy.Split('-');
            if (Convert.ToInt32(spliteddata[0]) - scadem < 0)
            {
                spliteddata[0] = Convert.ToString(12 - scadem);
                spliteddata[2] = Convert.ToString(Convert.ToInt32(spliteddata[2]) - 1);
            }
            else
                spliteddata[0] = Convert.ToString(Convert.ToInt32(spliteddata[0]) - scadem);
            string data = "&month=" + spliteddata[0] + "&year=" + spliteddata[2];// + "&period=";
            /*if (Convert.ToInt32(spliteddata[1]) > 15)
            {
                data += '2';
            }
            else
            {
                data += '1';
            }*/
            string Uri = "https://horex.beststudios.ro/v1/index.php?key=" + key + "&action=jasminReportData&modelCNP=" + CNP + data;
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Uri);
            X509Store store2 = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            store2.Open(OpenFlags.ReadOnly);


            X509Certificate2Collection collection2 = store2.Certificates.Find(X509FindType.FindBySubjectName, "Horex Client Best Studios", true);

            Request.ClientCertificates = collection2;


            Request.Method = "POST";
            // returned values are returned as a stream, then read into a string
            String stringResponse = string.Empty;
            HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            using (System.IO.StreamReader ResponseStream = new System.IO.StreamReader(Response.GetResponseStream()))
            {
                stringResponse = ResponseStream.ReadToEnd();
                ResponseStream.Close();
            }
            return stringResponse;

        }
        static public string GetPerioadaActuala()//calculeaza perioada actuala
        {
            string data = string.Empty;
            DateTime dataFormat = DateTime.Now;
            string dummy = dataFormat.ToString("MM/dd/yyyy HH:mm:ss").Split(' ')[0];
            string[] spliteddata;
            if(dummy.Contains("/"))
                spliteddata = dummy.Split('/');
            else
                spliteddata = dummy.Split('-');
            data = "&month=" + spliteddata[0] + "&year=" + spliteddata[2] + "&period=";
            if (Convert.ToInt32(spliteddata[1]) > 15)
            {
                data += '2';
                SetPerioada(2);
            }
            else
            {
                data += '1';
                SetPerioada(1);
            }
            return data;
        }
        static public string GetPerioadaAnterioara()//calculeaza perioada anterioara
        {
            string data = string.Empty;
            if (perioada == 2)
            {
                DateTime dataFormat = DateTime.Now;
                string dummy = dataFormat.ToString("MM/dd/yyyy HH:mm:ss").Split(' ')[0];
                string[] spliteddata;
                if (dummy.Contains("/"))
                    spliteddata = dummy.Split('/');
                else
                    spliteddata = dummy.Split('-');
                data = "&month=" + spliteddata[0] + "&year=" + spliteddata[2] + "&period=";
                data += '1';
            }
            else
            {
                DateTime dataFormat = DateTime.Now;
                string dummy = dataFormat.ToString("MM/dd/yyyy HH:mm:ss").Split(' ')[0];
                string[] spliteddata;
                if (dummy.Contains("/"))
                    spliteddata = dummy.Split('/');
                else
                    spliteddata = dummy.Split('-');
                data = "&month=" + Substract1(spliteddata[0]) + "&year=" + spliteddata[2] + "&period=";
                data += '2';

            }
            return data;
        }
        static private string Substract1(string target)//scade 1 si returneaza string
        {
            int nr = Convert.ToInt32(target) - 1;
            if (nr != 0)
                return Convert.ToString(nr);
            else
                return 12.ToString();
        }
        static public double GetPauza(string onlinedate, double online_time)//calcueaaz pauzza
        {
           
            DateTime secondDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            System.TimeSpan diff = secondDate.Subtract(DateTime.Parse(onlinedate));
            return diff.TotalSeconds - online_time;
        }
        #endregion
        /// <summary>
        /// setam culorile la cercuri
        /// Setting the colors in the ellipses
        /// </summary>
        #region Colorss
        static public System.Windows.Media.Color GetColorsOre(string ore, bool perioada_curenta)//set color la ore perioada curenta/antecedenta
        {
            int val_ore = Convert.ToInt32(ore.Split(':')[0]);
            if (perioada_curenta)
                val_ore = val_ore / numar_zile_perioada_curenta() * numar_total_zile_perioada_actuala();
            if (val_ore < 40)
                return System.Windows.Media.Colors.Red;
            if (val_ore < 90)
                return System.Windows.Media.Colors.Red;
            if (val_ore < 110)
                return System.Windows.Media.Colors.OrangeRed;
            if (val_ore < 120)
                return System.Windows.Media.Colors.Yellow;
            return System.Windows.Media.Colors.Green;

        }
        static public System.Windows.Media.Color GetColorsIncasari(string incasari, bool perioada_curenta)//set color la incasari perioada curenta/antecedenta
        {
            var val_incasari = Convert.ToDouble(incasari);
            if (perioada_curenta)
                val_incasari = val_incasari / numar_zile_perioada_curenta() * numar_total_zile_perioada_actuala();
            if (val_incasari < 1000)
                return System.Windows.Media.Colors.Black;
            if (val_incasari < 2000)
                return System.Windows.Media.Colors.Red;
            if (val_incasari < 4000)
                return System.Windows.Media.Colors.OrangeRed;
            if (val_incasari < 6000)
                return System.Windows.Media.Colors.Yellow;
            if (val_incasari < 8000)
                return System.Windows.Media.Colors.LightGreen;
            return System.Windows.Media.Colors.Green;
        }
        static public System.Windows.Media.Color GetColorsDOlarPerHOur(string incasari)//set color la Dolari pe ora perioada curenta/antecedenta
        {
            var val_incasari = Convert.ToDouble(incasari);
            if (val_incasari < 12)
                return System.Windows.Media.Colors.Black;
            if (val_incasari < 22)
                return System.Windows.Media.Colors.Red;
            if (val_incasari < 50)
                return System.Windows.Media.Colors.OrangeRed;
            if (val_incasari < 75)
                return System.Windows.Media.Colors.Yellow;
            if (val_incasari < 100)
                return System.Windows.Media.Colors.LightGreen;
            return System.Windows.Media.Colors.Green;
        }
        static public System.Windows.Media.Color GetBounceColors(double val_incasari)//set color la bouncerate perioada curenta/antecedenta
        {
            if (val_incasari < 0.65)
                return System.Windows.Media.Colors.Green;
            if (val_incasari < 0.70)
                return System.Windows.Media.Colors.Yellow;
            if (val_incasari < 0.75)
                return System.Windows.Media.Colors.OrangeRed;
            return System.Windows.Media.Colors.Red;
        }
        static public System.Windows.Media.Color GetCurrentIncomeColors(string val)//set color la iincome perioada curenta/antecedenta
        {
            var val_incasari = Convert.ToDouble(val);
            if (val_incasari < 100)
                return System.Windows.Media.Colors.Black;
            if (val_incasari < 200)
                return System.Windows.Media.Colors.Red;
            if (val_incasari < 400)
                return System.Windows.Media.Colors.OrangeRed;
            if (val_incasari < 600)
                return System.Windows.Media.Colors.Yellow;
            if (val_incasari < 1000)
                return System.Windows.Media.Colors.LightGreen;
            return System.Windows.Media.Colors.Green;
        }
        static public System.Windows.Media.Color GetCurrentHourColors(string val)//set color la seeiunea curenta 
        {
            var val_incasari = Convert.ToDouble(val.Split(':')[0]);
            if (val_incasari < 5)
                return System.Windows.Media.Colors.Black;
            if (val_incasari < 7)
                return System.Windows.Media.Colors.Red;
            if (val_incasari < 8)
                return System.Windows.Media.Colors.OrangeRed;
            if (val_incasari < 10)
                return System.Windows.Media.Colors.Yellow;
            if (val_incasari < 12)
                return System.Windows.Media.Colors.LightGreen;
            return System.Windows.Media.Colors.Green;
        }
        static public System.Windows.Media.Color GetPauzaColors(double val_incasari)//set color la pauza
        {
            if (val_incasari < 30 * 60)
                return System.Windows.Media.Colors.Green;
            if (val_incasari < 60 * 60)
                return System.Windows.Media.Colors.Yellow;
            if (val_incasari < 90 * 60)
                return System.Windows.Media.Colors.OrangeRed;
            return System.Windows.Media.Colors.Red;
        }
        static public System.Windows.Media.Color GetCartonaseColors(string val)//set color la cartonase
        {
            var val_incasari = Convert.ToDouble(val);
            if (val_incasari == 3)
                return System.Windows.Media.Colors.Red;
            if (val_incasari == 2)
                return System.Windows.Media.Colors.Yellow;
            if (val_incasari == 1)
                return System.Windows.Media.Colors.LightGreen;
            return System.Windows.Media.Colors.Green;
        }

        #endregion
        static int numar_zile_perioada_curenta()
        {
            int numar_de_zile = 0;
            DateTime dateTime = DateTime.UtcNow.Date;
            string data_curenta = dateTime.ToString("dd/MM/yyyy");
            if (data_curenta.Contains("/"))
            {
                if (Convert.ToInt32(data_curenta.Split('/')[0]) > 15)
                    numar_de_zile = Convert.ToInt32(data_curenta.Split('/')[0]) - 15;
                else
                    numar_de_zile = Convert.ToInt32(data_curenta.Split('/')[0]);
            }
            else
            {
                if (Convert.ToInt32(data_curenta.Split('-')[0]) > 15)
                    numar_de_zile = Convert.ToInt32(data_curenta.Split('-')[0]) - 15;
                else
                    numar_de_zile = Convert.ToInt32(data_curenta.Split('-')[0]);
            }
          
            return numar_de_zile;

        }
        static int numar_total_zile_perioada_actuala()
        {

            int numar_de_zile = 0;
            DateTime dateTime = DateTime.UtcNow.Date;
            string data_curenta = dateTime.ToString("dd/MM/yyyy");
            int numar_total = 0;
            if (data_curenta.Contains("/"))
            {
                 numar_total = DateTime.DaysInMonth(Convert.ToInt32(data_curenta.Split('/')[2]), Convert.ToInt32(data_curenta.Split('/')[1]));
                if (Convert.ToInt32(data_curenta.Split('/')[0]) > 15)
                    numar_de_zile = numar_total - 15;
                else
                    numar_de_zile = 15;
            }
            else
            {
                numar_total = DateTime.DaysInMonth(Convert.ToInt32(data_curenta.Split('-')[2]), Convert.ToInt32(data_curenta.Split('-')[1]));
                if (Convert.ToInt32(data_curenta.Split('-')[0]) > 15)
                    numar_de_zile = numar_total - 15;
                else
                    numar_de_zile = 15;
            }
            return numar_de_zile;
        }
        static public ImageBrush GetIndicatorTrafficBun(System.Windows.Media.Color old_hours, System.Windows.Media.Color current_hours)
        {
            ImageBrush indicator=new ImageBrush();
            //Red
            #region perioada antecedenta rosie
            if (old_hours == Colors.Red)
            {
                if (current_hours == Colors.DarkGreen)
                {
                    indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_slab.png");
                }
                indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_f_slab.png");

            }

            #endregion

            //Orange
            #region perioada antecedenta protocalie
            if (old_hours == System.Windows.Media.Colors.Orange)
            {
                if (current_hours == System.Windows.Media.Colors.Red)
                {
                    indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_f_slab.png");

                }
                if (current_hours == System.Windows.Media.Colors.Green)
                {

                    indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_mediu.png");

                }
                indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_slab.png");
                return indicator;
            }
            #endregion

            //Yellow
            #region perioada antecedenta galbena
            if (old_hours == System.Windows.Media.Colors.Yellow)
            {
                if (current_hours == System.Windows.Media.Colors.Red)
                {
                    indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_f_slab.png");

                }
                if (current_hours == System.Windows.Media.Colors.Orange)
                {
                    indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_slab.png");

                }
                indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_mediu.png");
                return indicator;
            }
            #endregion

            //LightGreen
            #region perioada antecedenta verde deschis
            if (old_hours == System.Windows.Media.Colors.LightGreen)
            {
                if (current_hours == System.Windows.Media.Colors.Red)
                {
                    indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_f_slab.png");

                }
                if (current_hours == System.Windows.Media.Colors.Orange)
                {
                    indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_slab.png");

                }
                if (current_hours == System.Windows.Media.Colors.Yellow)
                {
                    indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_mediu.png");

                }

                indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_bun.png");
                return indicator;
            }
            #endregion

            //Green
            #region perioada antecedenta verde
            if (current_hours == System.Windows.Media.Colors.Red)
            {
                indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_f_slab.png");

            }
            if (current_hours == System.Windows.Media.Colors.Orange)
            {
                indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_slab.png");

            }
            if (current_hours == System.Windows.Media.Colors.Yellow)
            {
                indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_mediu.png");
            } 
            if (current_hours == System.Windows.Media.Colors.LightGreen)
            {
                indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_bun.png");
            }
               
            indicator.ImageSource = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "icon_traffic_f_bun.png");
            return indicator;
            #endregion



        }
    }
}
