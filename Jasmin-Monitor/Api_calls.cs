using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Jasmin_Monitor
{
    class Api_calstring
    {
        //this class cannot be implemented-SingleTon
        private Api_calstring() { }
        /// <summary>
        /// campuri pe care le folosim in API calls
        /// </summary>
        #region fields
        static private string email = "";
        static private string password = "";
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
       static  private BitmapImage LoadImage(string myImageFile)
        {
            BitmapImage myRetVal = null;
            if (myImageFile != null)
            {
                BitmapImage image = new BitmapImage();
                using (FileStream stream = File.OpenRead(myImageFile))
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
        static public System.Windows.Controls.Image GetProfilePIcture(string key, string CNP)//gets the profile picture and saves it under "profilepicture.jpg"
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
                using (BinaryReader reader = new BinaryReader(lxResponse.GetResponseStream()))
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "profilepicture.jpg"))
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + "profilepicture.jpg");
                        
                    Byte[] lnByte = reader.ReadBytes(1 * 1024 * 1024 * 10);
                    using (FileStream lxFS = new FileStream("profilepicture.jpg", FileMode.Create))
                    {
                        lxFS.Write(lnByte, 0, lnByte.Length);
                      
                    }
                
.
              
            }
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            image.Source = LoadImage(AppDomain.CurrentDomain.BaseDirectory+"profilepicture.jpg");
            return image;

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
            using (StreamReader ResponseStream = new StreamReader(Response.GetResponseStream()))
            {
                stringResponse = ResponseStream.ReadToEnd();
                email = stringResponse.Split('{')[1].Split('"')[7];
                password = stringResponse.Split('{')[1].Split('"')[9];
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
            using (StreamReader ResponseStream = new StreamReader(Response.GetResponseStream()))
            {
                stringResponse = ResponseStream.ReadToEnd();
                ResponseStream.Close();
            }
            return stringResponse;
        }
        static public string GetBounceRate(string key, string CNP, int scadem)//bouncerate si numarul de iduri
        {
            DateTime dataFormat = DateTime.Now;
            string[] spliteddata = dataFormat.ToString().Split(' ')[0].Split('/');
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
            using (StreamReader ResponseStream = new StreamReader(Response.GetResponseStream()))
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
            string[] spliteddata = dataFormat.ToString().Split(' ')[0].Split('/');
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
                string[] spliteddata = dataFormat.ToString().Split(' ')[0].Split('/');
                data = "&month=" + spliteddata[0] + "&year=" + spliteddata[2] + "&period=";
                data += '1';
            }
            else
            {
                DateTime dataFormat = DateTime.Now;
                string[] spliteddata = dataFormat.ToString().Split(' ')[0].Split('/');
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
        static public double GetPauza(string onlinedate)//calcueaaz pauzza
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime secondDate = DateTime.Parse(date);
            DateTime firstDate = DateTime.Parse(onlinedate);
            System.TimeSpan diff = secondDate.Subtract(firstDate);
            return diff.TotalSeconds;
        }
        #endregion
        /// <summary>
        /// setam culorile la cercuri
        /// Setting the colors in the ellipses
        /// </summary>
        #region Colors
        static public System.Drawing.Color GetColorOre(string ore)//set color la ore perioada curenta/antecedenta
        {
            int val_ore = Convert.ToInt32(ore.Split(':')[0]);
            if (val_ore < 40)
                return System.Drawing.Color.Black;
            if (val_ore < 90)
                return System.Drawing.Color.Red;
            if (val_ore < 110)
                return System.Drawing.Color.Orange;
            if (val_ore < 120)
                return System.Drawing.Color.Yellow;
            return System.Drawing.Color.DarkGreen;

        }
        static public System.Drawing.Color GetColorIncasari(string incasari)//set color la incasari perioada curenta/antecedenta
        {
            var val_incasari = Convert.ToDouble(incasari);
            if (val_incasari < 1000)
                return System.Drawing.Color.Black;
            if (val_incasari < 2000)
                return System.Drawing.Color.Red;
            if (val_incasari < 4000)
                return System.Drawing.Color.Orange;
            if (val_incasari < 6000)
                return System.Drawing.Color.Yellow;
            if (val_incasari < 8000)
                return System.Drawing.Color.LightGreen;
            return System.Drawing.Color.DarkGreen;
        }
        static public System.Drawing.Color GetColorDOlarPerHOur(string incasari)//set color la Dolari pe ora perioada curenta/antecedenta
        {
            var val_incasari = Convert.ToDouble(incasari);
            if (val_incasari < 12)
                return System.Drawing.Color.Black;
            if (val_incasari < 22)
                return System.Drawing.Color.Red;
            if (val_incasari < 50)
                return System.Drawing.Color.Orange;
            if (val_incasari < 75)
                return System.Drawing.Color.Yellow;
            if (val_incasari < 100)
                return System.Drawing.Color.LightGreen;
            return System.Drawing.Color.DarkGreen;
        }
        static public System.Drawing.Color GetBounceCOlor(double val_incasari)//set color la bouncerate perioada curenta/antecedenta
        {
            if (val_incasari < 0.65)
                return System.Drawing.Color.Green;
            if (val_incasari < 0.70)
                return System.Drawing.Color.Yellow;
            if (val_incasari < 75)
                return System.Drawing.Color.Orange;
            return System.Drawing.Color.Black;
        }
        static public System.Drawing.Color GetCurrentIncomeColor(string val)//set color la iincome perioada curenta/antecedenta
        {
            var val_incasari = Convert.ToDouble(val);
            if (val_incasari < 100)
                return System.Drawing.Color.Black;
            if (val_incasari < 200)
                return System.Drawing.Color.Red;
            if (val_incasari < 400)
                return System.Drawing.Color.Orange;
            if (val_incasari < 600)
                return System.Drawing.Color.Yellow;
            if (val_incasari < 1000)
                return System.Drawing.Color.LightGreen;
            return System.Drawing.Color.DarkGreen;
        }
        static public System.Drawing.Color GetCurrentHourColor(string val)//set color la seeiunea curenta 
        {
            var val_incasari = Convert.ToDouble(val.Split(':')[0]);
            if (val_incasari < 5)
                return System.Drawing.Color.Black;
            if (val_incasari < 7)
                return System.Drawing.Color.Red;
            if (val_incasari < 8)
                return System.Drawing.Color.Orange;
            if (val_incasari < 10)
                return System.Drawing.Color.Yellow;
            if (val_incasari < 12)
                return System.Drawing.Color.LightGreen;
            return System.Drawing.Color.DarkGreen;
        }
        static public System.Drawing.Color GetPauzaCOlor(double val_incasari)//set color la pauza
        {
            if (val_incasari < 30 * 60)
                return System.Drawing.Color.Green;
            if (val_incasari < 60 * 60)
                return System.Drawing.Color.Yellow;
            if (val_incasari < 90 * 60)
                return System.Drawing.Color.Orange;
            return System.Drawing.Color.Black;
        }
        static public System.Drawing.Color GetCartonaseColor(string val)//set color la cartonase
        {
            var val_incasari = Convert.ToDouble(val);
            if (val_incasari == 3)
                return System.Drawing.Color.Red;
            if (val_incasari == 2)
                return System.Drawing.Color.Yellow;
            if (val_incasari == 1)
                return System.Drawing.Color.LightGreen;
            return System.Drawing.Color.DarkGreen;
        }

        #endregion
    }
}
