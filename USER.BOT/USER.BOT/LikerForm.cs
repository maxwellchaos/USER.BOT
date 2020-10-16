using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.Threading;

namespace USER.BOT
{
    public partial class LikerForm : Form
    {

        public string access_token;
        public string users_id;
        string UserId;

        public LikerForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //Выделение id
            string[] param = textBox1.Text.Split(new[] { "//", "/" }, StringSplitOptions.RemoveEmptyEntries);
            UserId = param[param.Length - 1];
            textBox1.Text = UserId;


            //Запрос на получение информации о стене
            string Request = "https://api.vk.com/method/wall.get?count&" + access_token + "&v=5.124";
            WebClient client = new WebClient();
            string Answer = Encoding.UTF8.GetString(client.DownloadData(Request));

            Wallget wg = JsonConvert.DeserializeObject<Wallget>(Answer);


            //Запрос на получение id
            string Request2 = "https://api.vk.com/method/utils.resolveScreenName?screen_name=" + UserId + "&" + access_token + "&v=5.124";
            WebClient client2 = new WebClient();
            string Answer2 = Encoding.UTF8.GetString(client2.DownloadData(Request2));

            idGet ig = JsonConvert.DeserializeObject<idGet>(Answer2);

            //Прогресс на прогрессбар
            int postCount = 0;
            progressBar1.Minimum = postCount;
            progressBar1.Maximum = wg.response.items.Count;

            //Массовый лайкинг
            foreach (Wallget.Item item in wg.response.items)
            {
                postCount = postCount + 1;
                progressBar1.Value = postCount;

                for (int i = 0; i < 10; i++)
                {
                    Application.DoEvents();
                    Thread.Sleep(50);
                }

                string Request1 = "https://api.vk.com/method/likes.add?type=post&item_id=" + item.id + "&owner_id=" + ig.response.object_id + "&" + access_token + "&v=5.124";
                WebClient client1 = new WebClient();
                string Answer1 = Encoding.UTF8.GetString(client1.DownloadData(Request1));
            }
        }
    }
}
