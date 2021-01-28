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
        string messagesEnd;
        int co;
        int co1;
        string random_id;
        int count = 0;
        WebClient cl = new WebClient();
        Form_Happy_day form;
        public mainForm()
        {
            InitializeComponent();
        }
        public string GetAnswer(string Request, string AccessTokrn)
        {
            string Req = Request + access_token + "&v=5.124";
            WebClient client = new WebClient();
            string Answer = Encoding.UTF8.GetString(client.DownloadData(Req));
            return Answer;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            Properties.Settings.Default.ChatBot = false;

            webBrowser1.BringToFront();
            webBrowser1.Dock = DockStyle.Fill;
            pictureBoxWait.BringToFront();


            webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=7614304"+
                "&display=page&redirect_uri=https://oauth.vk.com/blank.html&"+
                "scope=friends+groups+wall+photo+docs&"+
                "response_type=token&v=5.124&state=123456");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.ToString().Contains("access_token"))
            {
                string[] param = webBrowser1.Url.ToString().Split(new[] { "#", "&" }, StringSplitOptions.RemoveEmptyEntries);
                access_token = param[1];

                string Request = "https://api.vk.com/method/account.getProfileInfo?" +
                access_token + "&v=5.124";

                string Answer = GetAnswer(Request, access_token);

                GetProfileInfo gpi = JsonConvert.DeserializeObject<GetProfileInfo>(Answer);
                labelFamily.Text = gpi.response.last_name;
                labelName.Text = gpi.response.first_name;
                user_id = gpi.response.id.ToString();

                Request = "https://api.vk.com/method/users.get?fields=photo_100&" +
                access_token + "&v=5.124";

                Answer = GetAnswer(Request, access_token);

                UsersGet ug = JsonConvert.DeserializeObject<UsersGet>(Answer);
                pictureBoxAvatar.ImageLocation = ug.response[0].photo_100;
                user_id = ug.response[0].id.ToString();
                webBrowser1.Hide();
                string SendMessages31 = "https://api.vk.com/method/messages.getHistory?peer_id=" + user_id + "&user_id=" + user_id + "&access_token=7b38bbe3ec8b53c70db962d925ac5b6d3069d39b0aead017338b36c4d9bdf9a1a0284c26b348920d07873&v=5.124";
                string AnswerSendMessages31 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages31));
                Cheak rtf = JsonConvert.DeserializeObject<Cheak>(AnswerSendMessages31);
                foreach (Cheak.Item key in rtf.response.items)
                {
                    try
                    {
                        if (key.text.ToLower() == "оплачено" && key.from_id == -201385065)
                        {
                            panel1.Enabled = true;
                            label2.Visible = false;
                            Properties.Settings.Default.isStart = true;
                        }
                        else if (Properties.Settings.Default.isStart == false && Properties.Settings.Default.isStartTimer > 0)
                        {
                            panel1.Enabled = true;
                            timer2.Enabled = true;
                            label3.Visible = true;
                        }
                    }
                    catch
                    {

                    }
                }
                if (Properties.Settings.Default.PSetting == true)
                {
                    form = new Form_Happy_day();
                    form.access_token = access_token;
                    form.user_id = user_id;
                    form.Show();
                    form.Visible = false;
                }
            }

            pictureBoxWait.Hide();
        }
   private void buttonGDZ_Click(object sender, EventArgs e)
        {
            formgdz frmgdz = new formgdz();
            frmgdz.access_token = this.access_token;
            frmgdz.user_id = user_id;
            frmgdz.Show();

        }

        private void buttonGetPopularPost_Click(object sender, EventArgs e)
        {
            FormMostPopularPost frm = new FormMostPopularPost();
            frm.access_token = this.access_token;
            frm.user_id = user_id;
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
            try
            {
                BanName = "";
                co = 0;
                co1 = 0;
                progressBar1.Value = 0;
                bool check = false;
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
                    ListViewItem lvi = new ListViewItem(LvItem);
                    listView1.Items.Add(lvi);
                    co1++;
                    string BanFriendsId = "https://api.vk.com/method/users.get?user_ids=" + item.id + "&fields=deactivated" + "&" + access_token + "&v=5.124";
                    WebClient cl1 = new WebClient();
                    string AnswerBanFriends = Encoding.UTF8.GetString(cl.DownloadData(BanFriendsId));
                    BanFriends rtf1 = JsonConvert.DeserializeObject<BanFriends>(AnswerBanFriends);

                    Application.DoEvents();
                    Thread.Sleep(300);
                    foreach (BanFriends.ResponseBanFriends item1 in rtf1.response)
                    {
                        if (item1.deactivated == "deleted" || item1.deactivated == "banned")
                        {
                            string BanFriendsId1 = "https://api.vk.com/method/friends.delete?user_id=" + item.id + "&" + access_token + "&v=5.124";
                            WebClient cl11 = new WebClient();
                            string AnswerBanFriends1 = Encoding.UTF8.GetString(cl.DownloadData(BanFriendsId1));
                            BanName = BanName + item1.last_name + " " + item1.first_name + "; ";
                        }
                    }
                    if (BanName != null && check == false && co1 == co)
                    {
                        MessageBox.Show("Эти друзья удаленны из списка друзей: " + BanName, "Внимание!", MessageBoxButtons.OK);
                        label1.Visible = false;
                        progressBar1.Visible = false;
                        check = true;
                    }
                    else if (BanName == null && check == false && co1 == co)
                    {
                        MessageBox.Show("У тебя нет удалённых друзей.", "Внимание!", MessageBoxButtons.OK);
                        label1.Visible = false;
                        progressBar1.Visible = false;
                        check = true;
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (co1 <= co)
                {
                    progressBar1.Value = co1;
                }
                int b = co1 * 300 / 1000;
                int a = co * 300 / 1000;
                int ab = a - b;
                int ab1 = ab / 60;
                label1.Text = "Проверяю наличия удалённых друзей.\r\n Осталось примерно " + ab1.ToString() + " мин или " + ab.ToString() + " сек";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Chat_Bot_Form chat_Bot_Form = new Chat_Bot_Form();
            chat_Bot_Form.access_token = access_token;
            chat_Bot_Form.user_id = user_id;
            chat_Bot_Form.Show();
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/public201385065");
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            int time = Properties.Settings.Default.isStartTimer;
            label3.Text = "Пробная версия версия. Осталось " + (time / 60 ) + " мин " + (time - (60 * (time / 60))) + " сек ";
            time--;
            Properties.Settings.Default.isStartTimer = time;
            if (time < 0)
            {
                label3.Text = "Время пробной версии закончилось";
                timer2.Enabled = false;
                panel1.Enabled = false;
                MessageBox.Show("Внимание", "Пробная версия закончилась. Оплатите!");
                this.Close();
            }
        }

        private void buttonFindComments_Click(object sender, EventArgs e)
        {
            AnswerForm Form = new AnswerForm();
            Form.access_token = access_token;
            Form.user_id = user_id;
            Form.Show();
        }

        private void buttonMassComment_Click(object sender, EventArgs e)
        {
            FormMassComment Form = new FormMassComment();
            Form.access_token = access_token;
            Form.user_id = user_id;
            Form.Show();
        }

        private void buttonTextBot_Click(object sender, EventArgs e)
        {
            FormChatBot Form = new FormChatBot();
            Form.access_token = access_token;
            Form.user_id = user_id;
            Form.Show();
        }
    }    
}