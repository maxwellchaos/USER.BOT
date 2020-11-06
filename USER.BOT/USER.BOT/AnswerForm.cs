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
    public partial class AnswerForm : Form
    {
        public string access_token;
        public string user_id;
        public AnswerForm()
        {
            InitializeComponent();
        }

        private void AnswerForm_Load(object sender, EventArgs e)
        {
            string Request = "https://api.vk.com/method/wall.get?" +
                access_token + "&v=5.124";
            WebClient cl = new WebClient();
            string Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
            WallGet wg = JsonConvert.DeserializeObject<WallGet>(Answer);

            foreach (WallGet.Item item in wg.response.items)
            {
                Application.DoEvents();
                Thread.Sleep(300);

                if (item.comments.count > 0)
                {
                    string[] LVitem = new string[1];
                    LVitem[0] = item.id.ToString() + "\r\n" + item.text;
                    ListViewItem lvi = new ListViewItem(LVitem);
                    listView1.Items.Add(lvi);
                    //добавить фото из записи
                }
            }
            textBoxOutput.Text = "Введите номер поста, комментарии к которому вам нужны";
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            string Request = "https://api.vk.com/method/wall.get?" +
                access_token + "&v=5.124";
            WebClient cl = new WebClient();
            string Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
            WallGet wg = JsonConvert.DeserializeObject<WallGet>(Answer);

            foreach (WallGet.Item itemWG in wg.response.items)
            {
                if (itemWG.id == Convert.ToInt16(textBoxInput.Text))
                {
                    Request = "https://api.vk.com/method/wall.getComments?owner_id=327011638&post_id="+ itemWG.id +"&" +
                        access_token + "&v=5.124";
                    cl = new WebClient();
                    Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                    CommentsGet cg = JsonConvert.DeserializeObject<CommentsGet>(Answer);

                    foreach (CommentsGet.Item itemCG in cg.response.items)
                    {
                        string[] LVitem = new string[1];
                        LVitem[0] = itemCG.id.ToString() + "\r\n" + itemCG.text;
                        ListViewItem lvi = new ListViewItem(LVitem);
                        listView1.Items.Add(lvi);
                    }
                    textBoxOutput.Text = "Введите номер поста, комментарии к которому вам нужны";
                }
            }
            textBoxInput.Text = "";
        }
    }
}
