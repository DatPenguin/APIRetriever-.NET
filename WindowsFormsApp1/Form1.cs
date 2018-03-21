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

using WindowsForms1.ServiceReference1;

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
            APIRetrieverClient service = new APIRetrieverClient();

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
    }
}
