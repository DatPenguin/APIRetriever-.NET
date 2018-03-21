using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.AcceptButton = button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.WebClient wc = new System.Net.WebClient();

            JObject json_wt = JObject.Parse(wc.DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + textBox1.Text + "&appid=60098300f6125b63aa03fa46379c0b72&units=metric"));
            String wt_description = json_wt.SelectToken("weather[0].description").ToString();
            String wt_temp = json_wt.SelectToken("main.temp").ToString();

            JObject json_wp = JObject.Parse(wc.DownloadString("https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro=&explaintext=&titles=" + textBox1.Text));
            String wp_name = json_wp.SelectToken("query.pages").First.Last.SelectToken("title").ToString();
            String wp_description = json_wp.SelectToken("query.pages").First.Last.SelectToken("extract").ToString();

            label9.Text = wp_name;
            growLabel1.Text = wp_description;
            label7.Text = wt_description.First().ToString().ToUpper() + wt_description.Substring(1);
            label6.Text = wt_temp + " °C";

            label4.Text = "Weather";
            label5.Text = "Temperature";
        }
    }
}
