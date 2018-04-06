using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Net;

using WindowsForms1.ServiceReference1;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.AcceptButton = button1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                getREST();
            else
                getSOAP();
            getTime();
            getImage();
        }

        private void getImage()
        {
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            String URL = "https://en.wikipedia.org/w/api.php?action=query&prop=pageimages&format=json&pithumbsize=279&titles=" + textBox1.Text;
            string downloadString = client.DownloadString(URL);

            try
            {
                JObject json = JObject.Parse(downloadString);
                string imgUrl = json.SelectToken("query").SelectToken("pages").First().First().SelectToken("thumbnail").SelectToken("source").ToString();
                pictureBox1.Load(imgUrl);
                
            }
            catch (Exception myException)
            {
                Console.Error.WriteLine(myException.ToString());
                growLabel1.Text = myException.ToString();
            }
        }

        private void getREST()
        {
            this.Text = "APIRetriever C# Client [REST]";
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            String URL = "http://localhost:8080/net.ddns.dankest.ws.jersey.first/rest/apiretriever?city=" + textBox1.Text;
            string downloadString = client.DownloadString(URL);

            try
            {
                JObject json = JObject.Parse(downloadString);
                String wt_description = json.GetValue("weather").ToString();
                String wt_temp = json.GetValue("temp").ToString();
                String wp_name = json.GetValue("name").ToString();
                String wp_description = json.GetValue("desc").ToString();

                label9.Text = wp_name;
                growLabel1.Text = wp_description;
                label7.Text = wt_description.First().ToString().ToUpper() + wt_description.Substring(1);
                label6.Text = wt_temp + " °C";

                label4.Text = "Weather";
                label5.Text = "Temperature";
            }
            catch (Exception myException)
            {
                Console.Error.WriteLine(myException.ToString());
                growLabel1.Text = "";
                label4.Text = "";
                label5.Text = "";
                label6.Text = "";
                label7.Text = "";
                label9.Text = "Invalid city name";
            }
        }

        private void getTime()
        {
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            String URL = "http://localhost:8080/net.ddns.dankest.ws.jersey.first/rest/timeretriever";
            string downloadString = client.DownloadString(URL);

            try
            {
                JObject json = JObject.Parse(downloadString);
                String s = " Refreshed : " + json.GetValue("hours") + ":" + json.GetValue("minutes") + ":" + json.GetValue("seconds");

                this.Text += s;
            }
            catch (Exception myException)
            {
                Console.Error.WriteLine(myException.ToString());
            }
        }

        private void getSOAP()
        {
            this.Text = "APIRetriever C# Client [SOAP]";
            APIRetrieverClient service = new APIRetrieverClient();

            try
            {
                JObject json = JObject.Parse(service.getResult(textBox1.Text));
                String wt_description = json.GetValue("weather").ToString();
                String wt_temp = json.GetValue("temp").ToString();
                String wp_name = json.GetValue("name").ToString();
                String wp_description = json.GetValue("desc").ToString();

                label9.Text = wp_name;
                growLabel1.Text = wp_description;
                label7.Text = wt_description.First().ToString().ToUpper() + wt_description.Substring(1);
                label6.Text = wt_temp + " °C";

                label4.Text = "Weather";
                label5.Text = "Temperature";
            }
            catch (Exception myException)
            {
                Console.Error.WriteLine(myException.ToString());
                growLabel1.Text = "";
                label4.Text = "";
                label5.Text = "";
                label6.Text = "";
                label7.Text = "";
                label9.Text = "Invalid city name";
            }
        }

        public void label9_Click(object sender, EventArgs e)
        {

        }
        
    }
}
