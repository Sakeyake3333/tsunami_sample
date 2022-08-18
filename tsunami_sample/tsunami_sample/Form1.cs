using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tsunami_sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private readonly HttpClient client = new HttpClient();
        string p2p = "https://api.p2pquake.net/v2/jma/tsunami?limit=1&offset=5&order=-1";
        private async void Form1_Load(object sender, EventArgs e)
        {
            var json = await client.GetStringAsync(p2p);
            var root = JsonConvert.DeserializeObject<tsunami.Root>(json.Trim('[', ']'));

            string w_1 = "", w_2 = "", w_3 = "";
            var w_list = new Dictionary<string, string>()
            {
             {"MajorWarning","大津波警報" },
             {"Warning","津波警報" },
             {"Watch","津波注意報" },
             {"Unknown","不明" }
            };

            foreach (var area in root.areas)
            {
                switch (w_list[area.grade])
                {
                    case "大津波警報":
                        w_3 += area.name + "\n";
                        break;
                    case "津波警報":
                        w_2 += area.name + "\n";
                        break;
                    case "津波注意報":
                        w_1 += area.name + "\n";
                        break;
                }
            }
            if (w_3 != "") majorwarning.Text += w_3;
            if (w_2 != "") warning.Text += w_2;
            if (w_1 != "") watch.Text += w_1;
        }
    }
}
