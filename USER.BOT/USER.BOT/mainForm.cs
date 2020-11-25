using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Win32;
using System.Reflection;
using System.Threading;



namespace USER.BOT
{
    public partial class mainForm : Form
    {

        string access_token;
        string user_id;
        string BanName;
        int co;
        int co1;
        Form_Happy_day form;

        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            webBrowser1.BringToFront();
            webBrowser1.Dock = DockStyle.Fill;


            webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=7614304"+
                "&display=page&redirect_uri=https://oauth.vk.com/blank.html&"+
                "scope=friends+groups+wall+photo&"+
                "response_type=token&v=5.124&state=123456");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.ToString().Contains("access_token"))
            {
                string[] param = webBrowser1.Url.ToString().Split(new[] { "#", "&" }, StringSplitOptions.RemoveEmptyEntries);
                AccessToken = param[1];

                string Request = "https://api.vk.com/method/account.getProfileInfo?" +
                AccessToken + "&v=5.124";

                WebClient cl = new WebClient();

                string Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));

                GetProfileInfo gpi = JsonConvert.DeserializeObject<GetProfileInfo>(Answer);
                labelFamily.Text = gpi.response.last_name;
                labelName.Text = gpi.response.first_name;
                UserID = gpi.response.id.ToString();

                Request = "https://api.vk.com/method/users.get?fields=photo_100&" +
                AccessToken + "&v=5.124";


                Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));

                UsersGet ug = JsonConvert.DeserializeObject<UsersGet>(Answer);
                pictureBoxAvatar.ImageLocation = ug.response[0].photo_100;
                UserID = ug.response[0].id.ToString();
                webBrowser1.Hide();

                if (Properties.Settings.Default.PSetting == true)
                {
                    form = new Form_Happy_day();
                    form.access_token = access_token;
                    form.user_id = user_id;
                    form.Show();
                    form.Visible = false;
                }
            }
        }

        private void buttonGetPopularPost_Click(object sender, EventArgs e)
        {
            FormMostPopularPost frm = new FormMostPopularPost();
            frm.access_token = this.AccessToken;
            frm.user_id = UserID;
            frm.Show();
        }

        private void ButtonSelebrate_Click(object sender, EventArgs e)
        {
            form = new Form_Happy_day();
            form.access_token = access_token;
            form.user_id = user_id;
            form.Visible = true;
        }

        private void Ban_friends_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            label1.Visible = true;
            string FriendsId = "https://api.vk.com/method/friends.get?fields=can_post,bdate,sex&" + access_token + "&v=5.124";
            WebClient cl = new WebClient();
            string AnswerFriends = Encoding.UTF8.GetString(cl.DownloadData(FriendsId));
            FriendsGet1 rtf = JsonConvert.DeserializeObject<FriendsGet1>(AnswerFriends);
            progressBar1.Maximum = rtf.response.count;
            co = rtf.response.count;
            foreach (FriendsGet1.Item item in rtf.response.items)
            {
                string[] LvItem = new string[2];
                LvItem[0] = item.last_name + item.first_name;
                LvItem[1] = item.id.ToString();
                co1++;
                ListViewItem lvi = new ListViewItem(LvItem);
                listView1.Items.Add(lvi);
                string BanFriendsId = "https://api.vk.com/method/users.get?user_ids=" + item.id + "&fields=deactivated" + "&" + access_token + "&v=5.124";
                WebClient cl1 = new WebClient();
                string AnswerBanFriends = Encoding.UTF8.GetString(cl.DownloadData(BanFriendsId));
                BanFriends rtf1 = JsonConvert.DeserializeObject<BanFriends>(AnswerBanFriends);

                for (int i = 0; i < rtf.response.count; i = i + 1)
                {
                    Application.DoEvents();
                    Thread.Sleep(100);
                    co1++;
                }
                foreach (BanFriends.ResponseBanFriends item1 in rtf1.response)
                {
                    if(item1.deactivated == "deleted" || item1.deactivated == "banned")
                    {
                        string BanFriendsId1 = "https://api.vk.com/method/friends.delete?user_id=" + item.id +"&" + access_token + "&v=5.124";
                        WebClient cl11 = new WebClient();
                        string AnswerBanFriends1 = Encoding.UTF8.GetString(cl.DownloadData(BanFriendsId1));
                        BanName = BanName + item1.last_name + " " + item1.first_name + "; ";
                    }
                }
                if (BanName != null)
                {
                    MessageBox.Show("Эти друзья удаленны из списка друзей: " + BanName, "Внимание!", MessageBoxButtons.OK);
                    label1.Visible = false;
                    progressBar1.Visible = false;
                }
                else
                {
                    MessageBox.Show("У тебя нет удалённых друзей.", "Внимание!", MessageBoxButtons.OK);
                    label1.Visible = false;
                    progressBar1.Visible = false;
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (co1 <= co)
            {
                progressBar1.Value = co1;
            }
            int b = co1 * 100 / 1000;
            int a = co * 100 / 1000;
            int ab = a - b;
            int ab1 = ab / 60;
            label1.Text = "Проверяю наличия удалённых друзей. Осталось примерно \r\n" + ab1.ToString() + " мин или " + ab.ToString() + " сек";

        private void ButtonLiking_Click(object sender, EventArgs e)
        {
            LikerForm form = new LikerForm();
            form.access_token = access_token;
            form.users_id = users_id;
            form.ShowDialog();
        }
    }
}
