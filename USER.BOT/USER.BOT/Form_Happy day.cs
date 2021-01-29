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
using System.Threading;

namespace USER.BOT
{
    public partial class Form_Happy_day : Form
    {
        public string access_token;
        public string user_id;
        bool setting;
        bool d;
        int co;
        int co1;
        int x;
        string happyday_name;
        string happy8_name;
        string happy23_name;
        string happynewyear_name;
        string happyday_text;
        string happy8_text;
        string happy23_text;
        string happynewyear_text;


        public Form_Happy_day()
        {
            InitializeComponent();
        }

        private void Form_Happy_day_Load(object sender, EventArgs e)
        {
            happyday_text = textBox3.Text;
            happy8_text = textBox2.Text;
            happy23_text = textBox4.Text;
            happynewyear_text = textBox8.Text;


            DateTime date11 = DateTime.Now;
            string datenow11 = date11.Day.ToString() + "." + date11.Month;
            if (Properties.Settings.Default.WhatDay != datenow11)
            {
                Properties.Settings.Default.Pstart = true;
            }


            d = true;

            textBox3.Text = Properties.Settings.Default.birthday;
            textBox2.Text = Properties.Settings.Default.march8;
            textBox4.Text = Properties.Settings.Default.february23;
            textBox8.Text = Properties.Settings.Default.newyear;

            setting = Properties.Settings.Default.PSetting;
            if (setting == true)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (setting == false)
            {
                if (textBox3.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox8.Text != "")
                {
                    x = 1;
                    string FriendsId = "https://api.vk.com/method/friends.get?fields=can_post,bdate,sex&" + access_token + "&v=5.124";
                    WebClient cl = new WebClient();
                    string AnswerFriends = Encoding.UTF8.GetString(cl.DownloadData(FriendsId));
                    FriendsGet1 rtf = JsonConvert.DeserializeObject<FriendsGet1>(AnswerFriends);
                    co = rtf.response.count;
                    label1.Text = AnswerFriends;
                    foreach (FriendsGet1.Item item in rtf.response.items)
                    {
                        try
                        {

                            string[] LvItem = new string[5];
                            LvItem[0] = item.first_name + " " + item.last_name;
                            LvItem[1] = item.id.ToString();
                            LvItem[2] = item.sex.ToString();
                            LvItem[3] = item.bdate;
                            LvItem[4] = item.can_post.ToString();
                            co1++;
                            ListViewItem lvi = new ListViewItem(LvItem);
                            listView1.Items.Add(lvi);

                            DateTime date1 = DateTime.Now;
                            string datenow = date1.Day.ToString() + "." + date1.Month;
                            Properties.Settings.Default.WhatDay = datenow;
                            Properties.Settings.Default.Save();
                            listView1.Items[listView1.Items.Count - 1].EnsureVisible();
                            if (item.bdate != null)
                            {
                                string[] date = item.bdate.Split(new[] { ".20", ".19" }, StringSplitOptions.RemoveEmptyEntries);
                                if (date[0] == datenow && item.can_post == 1)
                                {

                                    string Request = "https://api.vk.com/method/wall.post?message=" + textBox3.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox5.Text + "&" + access_token + "&v=5.124";
                                    string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                    happyday_name = happyday_name + item.first_name + " " + item.last_name;

                                }
                                else if (date[0] == datenow && item.can_post == 0)
                                {

                                    string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                    string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                    GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                    string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                    string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox3.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox5.Text + "&" + access_token + "&v=5.124";
                                    string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                    happyday_name = happyday_name + item.first_name + " " + item.last_name + ", ";
                                }
                            }
                            string datenow1 = date1.Day.ToString() + "." + date1.Month;
                            if (datenow1 == "8.03" && item.sex == 1 && item.can_post == 1)
                            {
                                string Request = "https://api.vk.com/method/wall.post?message=" + textBox2.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox6.Text + "&" + access_token + "&v=5.124";
                                string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                happy8_name = happy8_name + item.first_name + " " + item.last_name + ", ";
                            }
                            else if (datenow1 == "8.03" && item.sex == 1 && item.can_post == 0)
                            {
                                string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox2.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox6.Text + "&" + access_token + "&v=5.124";
                                string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                happy8_name = happy8_name + item.first_name + " " + item.last_name + ", ";
                            }
                            if (datenow1 == "23.02" && item.sex == 2 && item.can_post == 1)
                            {
                                string Request = "https://api.vk.com/method/wall.post?message=" + textBox4.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox7.Text + "&" + access_token + "&v=5.124";
                                string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                happy23_name = happy23_name + item.first_name + " " + item.last_name + ", ";
                            }
                            else if (datenow1 == "23.02" && item.sex == 2 && item.can_post == 0)
                            {
                                string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox4.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox7.Text + "&" + access_token + "&v=5.124";
                                string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                happy23_name = happy23_name + item.first_name + " " + item.last_name + ", ";
                            }
                            if (datenow1 == "1.01" && item.can_post == 1)
                            {
                                string Request = "https://api.vk.com/method/wall.post?message=" + textBox8.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox9.Text + "&" + access_token + "&v=5.124";
                                string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                happynewyear_name = happynewyear_name + item.first_name + " " + item.last_name + ", ";
                            }
                            else if (datenow1 == "1.01" && item.can_post == 0)
                            {
                                string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox8.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox9.Text + "&" + access_token + "&v=5.124";
                                string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                happynewyear_name = happynewyear_name + item.first_name + " " + item.last_name + ", ";
                            }
                            Application.DoEvents();
                            Thread.Sleep(50);
                            if (co == co1)
                            {
                                if (happyday_name != null)
                                {
                                    if (MessageBox.Show("Молодец, сегодня ты не забыл поздравить с днём рождения: " + happyday_name + "с текстом (" + happyday_text + ")", "Внимание!!!", MessageBoxButtons.OK) == DialogResult.OK)
                                    {
                                        Properties.Settings.Default.Pstart = false;
                                    }
                                }
                                else if (happyday_name != null && happy8_name != null)
                                {
                                    if (MessageBox.Show("Молодец, сегодня ты не забыл поздравить с днём рождения: " + happyday_name + "с текстом (" + happy8_text + ") и так же ты не забыл поздравить всех подруг с 8 мартом, с текстом (" + happy8_text + ")", "Внимание!!!", MessageBoxButtons.OK) == DialogResult.OK)
                                    {
                                        Properties.Settings.Default.Pstart = false;
                                    }
                                }
                                else if (happyday_name != null && happy23_name != null)
                                {
                                    if (MessageBox.Show("Молодец, сегодня ты не забыл поздравить с днём рождения: " + happyday_name + "с текстом (" + happy23_text + ") и так же ты не забыл поздравить всех мужчин с 23 февраля, с текстом (" + happy23_text + ")", "Внимание!!!", MessageBoxButtons.OK) == DialogResult.OK)
                                    {
                                        Properties.Settings.Default.Pstart = false;
                                    }
                                }
                                else if (happyday_name != null && happynewyear_name != null)
                                {
                                    if (MessageBox.Show("Молодец, сегодня ты не забыл поздравить с днём рождения: " + happyday_name + "с текстом (" + happynewyear_text + ") и так же ты не забыл поздравить всех друзей с новым годом, с текстом (" + happynewyear_text + "). С новым годом!!!", "Внимание!!!", MessageBoxButtons.OK) == DialogResult.OK)
                                    {
                                        Properties.Settings.Default.Pstart = false;
                                    }
                                }
                                else if (happyday_name == null && happynewyear_name == null && happy23_name == null && happy8_name == null)
                                {
                                    if (MessageBox.Show("Сегодня скучный день. У твоих друзей сегодня нет праздника.", "Внимание!!!", MessageBoxButtons.OK) == DialogResult.OK)
                                    {
                                        Properties.Settings.Default.Pstart = false;
                                    }
                                }
                                else if (happynewyear_name != null)
                                {
                                    if (MessageBox.Show("Молодец, сегодня ты не забыл поздравить всех друзей с новым годом, с текстом (" + happynewyear_text + "). С новым годом!!!", "Внимание!!!", MessageBoxButtons.OK) == DialogResult.OK)
                                    {
                                        Properties.Settings.Default.Pstart = false;
                                    }
                                }
                                else if (happy23_name != null)
                                {
                                    if (MessageBox.Show("Молодец, сегодня ты не забыл поздравить всех мужчин с 23 февраля, с текстом (" + happy23_text + ")", "Внимание!!!", MessageBoxButtons.OK) == DialogResult.OK)
                                    {
                                        Properties.Settings.Default.Pstart = false;
                                    }
                                }
                                else if (happyday_name != null && happy8_name != null)
                                {
                                    if (MessageBox.Show("Молодец, сегодня ты не забыл поздравить всех подруг с 8 мартом, с текстом (" + happy8_text + ")", "Внимание!!!", MessageBoxButtons.OK) == DialogResult.OK)
                                    {
                                        Properties.Settings.Default.Pstart = false;
                                    }
                                }
                            }
                        }
                        catch(Exception ex) { MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
                else
                {
                    MessageBox.Show("Тебе сначала нужно написать текст ВСЕХ поздравлений.", "Ошибка!!!", MessageBoxButtons.OK);
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            happyday_text = textBox3.Text;
            happy8_text = textBox2.Text;
            happy23_text = textBox4.Text;
            happynewyear_text = textBox8.Text;

            DateTime date11 = DateTime.Now;
            string datenow11 = date11.Day.ToString() + "." + date11.Month;
            if (Properties.Settings.Default.WhatDay != datenow11)
            {
                Properties.Settings.Default.Pstart = true;
            }

            if (setting == true && Properties.Settings.Default.Pstart != false)
            {
                if (textBox3.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox8.Text != "")
                {
                    x = 2;
                    string FriendsId = "https://api.vk.com/method/friends.get?fields=can_post,bdate,sex&" + access_token + "&v=5.124";
                    WebClient cl = new WebClient();
                    string AnswerFriends = Encoding.UTF8.GetString(cl.DownloadData(FriendsId));
                    FriendsGet1 rtf = JsonConvert.DeserializeObject<FriendsGet1>(AnswerFriends);
                    co = rtf.response.count;
                    foreach (FriendsGet1.Item item in rtf.response.items)
                    {
                        try
                        {
                            if (rtf.response.count != co1)
                            {
                                string[] LvItem = new string[5];
                                LvItem[0] = item.first_name + " " + item.last_name;
                                LvItem[1] = item.id.ToString();
                                LvItem[2] = item.sex.ToString();
                                LvItem[3] = item.bdate;
                                LvItem[4] = item.can_post.ToString();
                                co1++;
                                ListViewItem lvi = new ListViewItem(LvItem);
                                listView1.Items.Add(lvi);


                                DateTime date1 = DateTime.Now;
                                string datenow = date1.Day.ToString() + "." + date1.Month;
                                Properties.Settings.Default.WhatDay = datenow;
                                Properties.Settings.Default.Save();
                                listView1.Items[listView1.Items.Count - 1].EnsureVisible();
                                if (item.bdate != null)
                                {
                                    string Text = textBox3.Text;
                                    string url = textBox5.Text;

                                    string[] date = item.bdate.Split(new[] { ".20", ".19" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (date[0] == datenow && item.can_post == 1)
                                    {
                                        string Request = "https://api.vk.com/method/wall.post?message=" + Text + "&owner_id=" + item.id + "&" + "attachments=" + url + "&" + access_token + "&v=5.124";
                                        string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                        happyday_name = happyday_name + item.first_name + " " + item.last_name;

                                    }
                                    else if (date[0] == datenow && item.can_post == 0)
                                    {

                                        string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                        string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                        GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                        string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                        string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + Text + "&owner_id=" + item.id + "&" + "attachments=" + url + "&" + access_token + "&v=5.124";
                                        string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                        happyday_name = happyday_name + item.first_name + " " + item.last_name + ", ";
                                    }
                                }
                                string datenow1 = date1.Day.ToString() + "." + date1.Month;
                                if (datenow1 == "8.03" && item.sex == 1 && item.can_post == 1)
                                {
                                    string Text = textBox2.Text;
                                    string url = textBox6.Text;

                                    string Request = "https://api.vk.com/method/wall.post?message=" + Text + "&owner_id=" + item.id + "&" + "attachments=" + url + "&" + access_token + "&v=5.124";
                                    string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                    happy8_name = happy8_name + item.first_name + " " + item.last_name + ", ";
                                }
                                else if (datenow1 == "8.03" && item.sex == 1 && item.can_post == 0)
                                {
                                    string Text = textBox2.Text;
                                    string url = textBox6.Text;

                                    string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                    string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                    GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                    string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                    string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + Text + "&owner_id=" + item.id + "&" + "attachments=" + url + "&" + access_token + "&v=5.124";
                                    string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                    happy8_name = happy8_name + item.first_name + " " + item.last_name + ", ";
                                }
                                if (datenow1 == "23.02" && item.sex == 2 && item.can_post == 1)
                                {
                                    string Text = textBox4.Text;
                                    string url = textBox7.Text;

                                    string Request = "https://api.vk.com/method/wall.post?message=" + Text + "&owner_id=" + item.id + "&" + "attachments=" + url + "&" + access_token + "&v=5.124";
                                    string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                    happy23_name = happy23_name + item.first_name + " " + item.last_name + ", ";
                                }
                                else if (datenow1 == "23.02" && item.sex == 2 && item.can_post == 0)
                                {
                                    string Text = textBox4.Text;
                                    string url = textBox7.Text;

                                    string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                    string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                    GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                    string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                    string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + Text + "&owner_id=" + item.id + "&" + "attachments=" + url + "&" + access_token + "&v=5.124";
                                    string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                    happy23_name = happy23_name + item.first_name + " " + item.last_name + ", ";
                                }
                                if (datenow1 == "1.01" && item.can_post == 1)
                                {
                                    string Text = textBox8.Text;
                                    string url = textBox9.Text;

                                    string Request = "https://api.vk.com/method/wall.post?message=" + Text + "&owner_id=" + item.id + "&" + "attachments=" + url + "&" + access_token + "&v=5.124";
                                    string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                    happynewyear_name = happynewyear_name + item.first_name + " " + item.last_name + ", ";
                                }
                                else if (datenow1 == "1.01" && item.can_post == 0)
                                {
                                    string Text = textBox8.Text;
                                    string url = textBox9.Text;

                                    string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                    string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                    GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                    string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                    string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + Text + "&owner_id=" + item.id + "&" + "attachments=" + url + "&" + access_token + "&v=5.124";
                                    string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                    happynewyear_name = happynewyear_name + item.first_name + " " + item.last_name + ", ";
                                }
                                Application.DoEvents();
                                Thread.Sleep(50);
                                if (happyday_name != null && Properties.Settings.Default.Pstart != false)
                                {
                                    USER_BOT.BalloonTipIcon = ToolTipIcon.Info;
                                    USER_BOT.BalloonTipTitle = "Внимание!!!";
                                    USER_BOT.BalloonTipText = "Молодец, сегодня ты поздравил с днём рождения: " + happyday_name + "с текстом (" + happyday_text + ")";
                                    USER_BOT.ShowBalloonTip(100000);
                                    Properties.Settings.Default.Pstart = false;
                                }
                                else if (happyday_name != null && happy8_name != null && Properties.Settings.Default.Pstart != false)
                                {
                                    USER_BOT.BalloonTipIcon = ToolTipIcon.Info;
                                    USER_BOT.BalloonTipTitle = "Внимание!!!";
                                    USER_BOT.BalloonTipText = "Молодец, сегодня ты не забыл поздравить с днём рождения: " + happyday_name + "с текстом (" + happy8_text + ") и так же ты не забыл поздравить всех подруг с 8 мартом, с текстом (" + happy8_text + ")";
                                    USER_BOT.ShowBalloonTip(100000);
                                    Properties.Settings.Default.Pstart = false;
                                }
                                else if (happyday_name != null && happy23_name != null && Properties.Settings.Default.Pstart != false)
                                {
                                    USER_BOT.BalloonTipIcon = ToolTipIcon.Info;
                                    USER_BOT.BalloonTipTitle = "Внимание!!!";
                                    USER_BOT.BalloonTipText = "Молодец, сегодня ты не забыл поздравить с днём рождения: " + happyday_name + "с текстом (" + happy23_text + ") и так же ты не забыл поздравить всех мужчин с 23 февраля, с текстом (" + happy23_text + ")";
                                    USER_BOT.ShowBalloonTip(100000);
                                    Properties.Settings.Default.Pstart = false;
                                }
                                else if (happyday_name != null && happynewyear_name != null && Properties.Settings.Default.Pstart != false)
                                {
                                    USER_BOT.BalloonTipIcon = ToolTipIcon.Info;
                                    USER_BOT.BalloonTipTitle = "Внимание!!!";
                                    USER_BOT.BalloonTipText = "Молодец, сегодня ты не забыл поздравить с днём рождения: " + happyday_name + "с текстом (" + happynewyear_text + ") и так же ты не забыл поздравить всех друзей с новым годом, с текстом (" + happynewyear_text + "). С новым годом!!!";
                                    USER_BOT.ShowBalloonTip(100000);
                                    Properties.Settings.Default.Pstart = false;
                                }
                                else if (happyday_name == null && happynewyear_name == null && happy23_name == null && happy8_name == null && Properties.Settings.Default.Pstart != false)
                                {
                                    USER_BOT.BalloonTipIcon = ToolTipIcon.Info;
                                    USER_BOT.BalloonTipTitle = "Внимание!!!";
                                    USER_BOT.BalloonTipText = "Сегодня скучный день. У твоих друзей сегодня нет праздника.";
                                    USER_BOT.ShowBalloonTip(100000);
                                    Properties.Settings.Default.Pstart = false;
                                }
                                else if (happynewyear_name != null && Properties.Settings.Default.Pstart != false)
                                {
                                    USER_BOT.BalloonTipIcon = ToolTipIcon.Info;
                                    USER_BOT.BalloonTipTitle = "Внимание!!!";
                                    USER_BOT.BalloonTipText = "Молодец, сегодня ты не забыл поздравить всех друзей с новым годом, с текстом (" + happynewyear_text + "). С новым годом!!!";
                                    USER_BOT.ShowBalloonTip(100000);
                                    Properties.Settings.Default.Pstart = false;
                                }
                                else if (happy23_name != null && Properties.Settings.Default.Pstart != false)
                                {
                                    USER_BOT.BalloonTipIcon = ToolTipIcon.Info;
                                    USER_BOT.BalloonTipTitle = "Внимание!!!";
                                    USER_BOT.BalloonTipText = "Молодец, сегодня ты не забыл поздравить всех мужчин с 23 февраля, с текстом (" + happy23_text + ")";
                                    USER_BOT.ShowBalloonTip(100000);
                                    Properties.Settings.Default.Pstart = false;
                                }
                                else if (happyday_name != null && happy8_name != null && Properties.Settings.Default.Pstart != false)
                                {
                                    USER_BOT.BalloonTipIcon = ToolTipIcon.Info;
                                    USER_BOT.BalloonTipTitle = "Внимание!!!";
                                    USER_BOT.BalloonTipText = "Молодец, сегодня ты не забыл поздравить всех подруг с 8 мартом, с текстом(" + happy8_text + ")";
                                    USER_BOT.ShowBalloonTip(100000);
                                    Properties.Settings.Default.Pstart = false;
                                }

                            }
                            Properties.Settings.Default.Pstart = false;
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            if (label12.Text.Contains("0 сек"))
            {
                label12.Visible = false;
            }
            else
            {
                label12.Visible = true;
            }
            if (Properties.Settings.Default.Pstart == false)
            {
                button1.Text = "Видемо сегодня ты уже поздравил своих друзей :)";
                button1.Enabled = false;
            }
            else
            {
                button1.Text = "Проверка праздника и поздравление с праздником";
                button1.Enabled = true;
            }
            if (x != 2)
            {
                progressBar1.Visible = true;
                label12.Visible = true;
            }
            if (x == 0)
            {
                int b = co1 * 500 / 1000;
                int a = co * 500 / 1000;
                int ab = a - b / 60;
                int ab1 = ab / 60;
                label12.Text = "Проверяю наличия праздника. Осталось примерно " + ab1.ToString() + " мин или " + ab.ToString() + " сек";
            }
            else if (x == 1)
            {
                int b = co1 * 50 / 1000;
                int a = co * 50 / 1000;
                int ab = a - b / 60;
                int ab1 = ab / 60;
                label12.Text = "Проверяю наличия праздника. Осталось примерно " + ab1.ToString() + " мин или " + ab.ToString() + " сек";
            }
            progressBar1.Maximum = co;
            if (co1 <= co) progressBar1.Value = co1;

            try
            {
                Properties.Settings.Default.birthday = textBox3.Text;
                Properties.Settings.Default.march8 = textBox2.Text;
                Properties.Settings.Default.february23 = textBox4.Text;
                Properties.Settings.Default.newyear = textBox8.Text;
                if (textBox5.Text.Contains("http") && textBox5.Text != "")
                {
                    string[] birthday = textBox5.Text.Split(new[] { "=", "%" }, StringSplitOptions.RemoveEmptyEntries);
                    textBox5.Text = birthday[1];
                }
                if (textBox6.Text.Contains("http") && textBox6.Text != "")
                {
                    string[] birthday = textBox6.Text.Split(new[] { "=", "%" }, StringSplitOptions.RemoveEmptyEntries);
                    textBox6.Text = birthday[1];
                }
                if (textBox7.Text.Contains("http") && textBox7.Text != "")
                {
                    string[] birthday = textBox7.Text.Split(new[] { "=", "%" }, StringSplitOptions.RemoveEmptyEntries);
                    textBox7.Text = birthday[1];
                }
                if (textBox9.Text.Contains("http") && textBox9.Text != "")
                {
                    string[] birthday = textBox9.Text.Split(new[] { "=", "%" }, StringSplitOptions.RemoveEmptyEntries);
                    textBox9.Text = birthday[1];
                }
                if (photo_url.Text.Contains("http") && photo_url.Text != "")
                {
                    string[] birthday = photo_url.Text.Split(new[] { "=", "%" }, StringSplitOptions.RemoveEmptyEntries);
                    textBox9.Text = birthday[1];
                }
                setting = Properties.Settings.Default.PSetting;
                if (setting == true)
                {
                    button1.Visible = false;
                    setting = true;
                    Properties.Settings.Default.PSetting = true;
                    Properties.Settings.Default.Save();
                }
                else if (setting == false)
                {
                    button1.Visible = true;
                    setting = false;
                    Properties.Settings.Default.PSetting = false;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton1.Checked = true;
                Properties.Settings.Default.PSetting = true;
                setting = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            button1.BackColor = Color.LightGray;
            button4.BackColor = Color.LightGray;
            button6.BackColor = Color.LightGray;
            listView1.BackColor = Color.White;
            label8.Text = "";
            d = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox8.Text != "")
            {
                if (radioButton2.Checked == true)
                {
                    radioButton1.Checked = false;
                    Properties.Settings.Default.PSetting = false;
                    setting = false;
                    MessageBox.Show("Теперь каждый раз при запуске приложения бот будет поздралять с праздником.", "Внимание!!!", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Тебе с начало нужно написать текст ВСЕХ сообщений(это можно сделать в настройках).", "Внимание!!!", MessageBoxButtons.OK);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (d == true)
            {
                button1.BackColor = Color.Green;
                button4.BackColor = Color.Purple;
                button6.BackColor = Color.Red;
                listView1.BackColor = Color.Cyan;
                label8.Text = "  Нажав зелёную кнопку(если кнопки нет, зайди в настройки и выбери'по кнопке'), бот проверит есть ли сегодня \r\nу твоих друзей день рождение,\r\nесли есть, бот на стене твоего друга или подруге\r\nоставит пост(от твоего имени), сообщение которого можно изменить нажав на фиолетовую кнопку,\r\nтак же вы сможете добавить фото, если вы укажите ссылку фото(из вк).\r\nЕщё бот отправляет поздравления девочкам на 8 марта или мальчиков на 23 февраля.\r\n" +
                    "  Нажав на красную кнопку, вы отправляет праздник(пост) всем вашим друзьям с сообщение,\r\n которое вы написали под кнопкой.\r\n" +
                    "  Нажав на фиолетовую кнопку, вы увидите и сможите изменить настройки.\r\n" +
                    "  Голубым цветом показанна таблица. В это таблице появится список ваших друзей,\r\n если вы нажмёте на красную или зелёную кнопку.\r\nЕщё в этой таблице вы сможите увидеть id,\r\nпол(1-женский;2-мужской), дату рождения друзей.\r\nТак же вы сможите увидеть можно ли вам оставлять пост на странице друга,\r\nесли нет вы всё равно позравите друга или подругу в коментариях под пследним постом.\r\n" +
                    "  Нажмите ещё раз на кнопку'Как пользоваться?'\r\nчтобы скрыть текст изменить цвет кнопок на изначальное.";
                d = false;
            }
            else if (d == false)
            {
                button1.BackColor = Color.LightGray;
                button4.BackColor = Color.LightGray;
                button6.BackColor = Color.LightGray;
                listView1.BackColor = Color.White;
                label8.Text = "";
                d = true;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void USER_BOT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form_Happy_day form = new Form_Happy_day();
            form.Visible = true;

        }

        private void Form_Happy_day_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_Happy_day form = new Form_Happy_day();
            form.Show();
            form.Visible = false;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void Label18_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            happyday_name = "";
            string Text = Text_Holiday.Text;
            string url = photo_url.Text;
            listView1.Items.Clear();
            string FriendsId = "https://api.vk.com/method/friends.get?fields=can_post,bdate,sex&" + access_token + "&v=5.124";
            WebClient cl = new WebClient();
            string AnswerFriends = Encoding.UTF8.GetString(cl.DownloadData(FriendsId));
            FriendsGet1 rtf = JsonConvert.DeserializeObject<FriendsGet1>(AnswerFriends);
            co = rtf.response.count;
            foreach (FriendsGet1.Item item in rtf.response.items)
            {
                Thread.Sleep(300);
                Application.DoEvents();
                try
                {
                    if (rtf.response.count != co1)
                    {
                        string[] LvItem = new string[5];
                        LvItem[0] = item.first_name + " " + item.last_name;
                        LvItem[1] = item.id.ToString();
                        LvItem[2] = item.sex.ToString();
                        LvItem[3] = item.bdate;
                        LvItem[4] = item.can_post.ToString();
                        co1++;
                        ListViewItem lvi = new ListViewItem(LvItem);
                        listView1.Items.Add(lvi);


                        DateTime date1 = DateTime.Now;
                        string datenow = date1.Day.ToString() + "." + date1.Month;
                        Properties.Settings.Default.WhatDay = datenow;
                        Properties.Settings.Default.Save();
                        listView1.Items[listView1.Items.Count - 1].EnsureVisible();
                        if (item.bdate != null)
                        {
                            if (item.can_post == 1)
                            {

                                string Request = "https://api.vk.com/method/wall.post?message=" + Text + "&owner_id=" + item.id + "&" + "attachments=" + url + "&" + access_token + "&v=5.124";
                                string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                happyday_name = happyday_name + item.first_name + " " + item.last_name;

                            }
                            else if (item.can_post == 0)
                            {

                                string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + Text + "&owner_id=" + item.id + "&" + "attachments=" + url + "&" + access_token + "&v=5.124";
                                string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                happyday_name = happyday_name + item.first_name + " " + item.last_name + ", ";
                            }
                        }
                    }
                }
                catch(Exception ex) { MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                if (co == co1)
                {
                    if (happyday_name != null)
                    {
                        if (MessageBox.Show("Молодец, сегодня ты не забыл поздравить с днём рождения: " + happyday_name + "с текстом (" + happyday_text + ")", "Внимание!!!", MessageBoxButtons.OK) == DialogResult.OK)
                        {
                            Properties.Settings.Default.Pstart = false;
                        }
                    }
                }
                }
            }
    }
}
