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
        public Form_Happy_day()
        {
            InitializeComponent();
        }

        private void Form_Happy_day_Load(object sender, EventArgs e)
        {
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
            if (setting == false)
            {
                if (textBox3.Text != "" && textBox2.Text != "" && textBox4.Text != "")
                {
                    string FriendsId = "https://api.vk.com/method/friends.get?fields=can_post,bdate,sex&" + access_token + "&v=5.124";
                    WebClient cl = new WebClient();
                    string AnswerFriends = Encoding.UTF8.GetString(cl.DownloadData(FriendsId));
                    FriendsGet1 rtf = JsonConvert.DeserializeObject<FriendsGet1>(AnswerFriends);
                    co = rtf.response.count;
                    foreach (FriendsGet1.Item item in rtf.response.items)
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
                                textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name;

                            }
                            else if (date[0] == datenow && item.can_post == 0)
                            {

                                string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox3.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox5.Text + "&" + access_token + "&v=5.124";
                                string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                            }
                        }
                        string datenow1 = date1.Day.ToString() + "." + date1.Month;
                        if (datenow1 == "8.03" && item.sex == 1 && item.can_post == 1)
                        {
                            string Request = "https://api.vk.com/method/wall.post?message=" + textBox2.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox6.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        else if (datenow1 == "8.03" && item.sex == 1 && item.can_post == 0)
                        {
                            string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                            string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                            string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                            string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox2.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox6.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        if (datenow1 == "23.02" && item.sex == 2 && item.can_post == 1)
                        {
                            string Request = "https://api.vk.com/method/wall.post?message=" + textBox4.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox7.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        else if (datenow1 == "23.02" && item.sex == 2 && item.can_post == 0)
                        {
                            string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                            string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                            string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                            string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox4.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox7.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        if (datenow1 == "1.01" && item.can_post == 1)
                        {
                            string Request = "https://api.vk.com/method/wall.post?message=" + textBox8.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox9.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        else if (datenow1 == "1.01" && item.can_post == 0)
                        {
                            string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                            string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                            string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                            string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox8.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox9.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        Application.DoEvents();
                        Thread.Sleep(500);
                    }
                }
                else
                {
                    MessageBox.Show("Тебе сначала нужно написать текст ВСЕХ поздравлений. После изменений перезапустите приложение.", "Ошибка!!!", MessageBoxButtons.OK);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (setting == true)
            {
                if (textBox3.Text != "" && textBox2.Text != "" && textBox4.Text != "")
                {
                    string FriendsId = "https://api.vk.com/method/friends.get?fields=can_post,bdate,sex&" + access_token + "&v=5.124";
                    WebClient cl = new WebClient();
                    string AnswerFriends = Encoding.UTF8.GetString(cl.DownloadData(FriendsId));
                    FriendsGet1 rtf = JsonConvert.DeserializeObject<FriendsGet1>(AnswerFriends);
                    co = rtf.response.count;
                    label1.Text = AnswerFriends;
                    foreach (FriendsGet1.Item item in rtf.response.items)
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
                                textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name;

                            }
                            else if (date[0] == datenow && item.can_post == 0)
                            {

                                string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                                string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                                string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                                string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox3.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox5.Text + "&" + access_token + "&v=5.124";
                                string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                                textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                            }
                        }
                        string datenow1 = date1.Day.ToString() + "." + date1.Month;
                        if (datenow1 == "8.03" && item.sex == 1 && item.can_post == 1)
                        {
                            string Request = "https://api.vk.com/method/wall.post?message=" + textBox2.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox6.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        else if (datenow1 == "8.03" && item.sex == 1 && item.can_post == 0)
                        {
                            string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                            string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                            string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                            string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox2.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox6.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        if (datenow1 == "23.02" && item.sex == 2 && item.can_post == 1)
                        {
                            string Request = "https://api.vk.com/method/wall.post?message=" + textBox4.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox7.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        else if (datenow1 == "23.02" && item.sex == 2 && item.can_post == 0)
                        {
                            string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                            string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                            string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                            string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox4.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox7.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        if (datenow1 == "1.01" && item.can_post == 1)
                        {
                            string Request = "https://api.vk.com/method/wall.post?message=" + textBox8.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox9.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }
                        else if (datenow1 == "1.01" && item.can_post == 0)
                        {
                            string Request = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                            string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                            string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                            string Request1 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox8.Text + "&owner_id=" + item.id + "&" + "attachments=" + textBox9.Text + "&" + access_token + "&v=5.124";
                            string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                            textBox1.Text = textBox1.Text + item.first_name + " " + item.last_name + ", ";
                        }

                        Application.DoEvents();
                        Thread.Sleep(500);
                    }
                }
                else
                {
                    MessageBox.Show("Тебе сначала нужно написать текст ВСЕХ поздравлений.", "Ошибка!!!", MessageBoxButtons.OK);
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Всё что ты написал, отправится ВСЕМ твоим друзьям!!! Ты точно этого хочешь?", "Предупреждение!!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string FriendsId = "https://api.vk.com/method/friends.get?fields=can_post,bdate,sex&" + access_token + "&v=5.124";
                WebClient cl = new WebClient();
                string AnswerFriends = Encoding.UTF8.GetString(cl.DownloadData(FriendsId));
                FriendsGet1 rtf = JsonConvert.DeserializeObject<FriendsGet1>(AnswerFriends);
                co = rtf.response.count;
                foreach (FriendsGet1.Item item in rtf.response.items)
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

                    string Request = "https://api.vk.com/method/wall.post?message=" + textBox1 + "&owner_id=" + item.id + "&" + access_token + "&v=5.124";
                    string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                    listView1.Items[listView1.Items.Count - 1].EnsureVisible();
                    Application.DoEvents();
                    Thread.Sleep(500);
                    if (item.can_post == 0)
                    {
                        string Request1 = "https://api.vk.com/method/wall.get?owner_id=" + item.id + "&" + access_token + "&v=5.124";
                        string AnswerFriends2 = Encoding.UTF8.GetString(cl.DownloadData(Request1));
                        GetProfileInfo rt = JsonConvert.DeserializeObject<GetProfileInfo>(AnswerFriends2);
                        string[] id = AnswerFriends2.Split(new[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);
                        string Request2 = "https://api.vk.com/method/wall.createComment?post_id=" + id[5] + "&message=" + textBox1.Text + "," + item.first_name + "&owner_id=" + item.id + "&" + access_token + "&v=5.124";
                        string AnswerFriends3 = Encoding.UTF8.GetString(cl.DownloadData(Request2));
                    }
                }
            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            if (co1 >= 1)
            {
                progressBar1.Visible = true;
                label12.Visible = true;
            }
            else
            {
                progressBar1.Visible = false;
                label12.Visible = false;
            }
            int b = co1 * 500 / 1000;
            int a = co * 500 /1000;
            int ab = a - b;
            int ab1 = ab / 60;
            label12.Text = "Проверяю наличия праздника. Осталось " + ab1.ToString() + " мин " + ab.ToString()+ " сек";
            progressBar1.Maximum = co;
            progressBar1.Value = co1;

            Properties.Settings.Default.birthday = textBox3.Text;
            Properties.Settings.Default.march8 = textBox2.Text;
            Properties.Settings.Default.february23 = textBox4.Text;
            Properties.Settings.Default.newyear = textBox8.Text;
            if (textBox5.Text.Contains ("http") && textBox5.Text != "")
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
            setting = Properties.Settings.Default.PSetting;
            if (setting == true)
            {
                button1.Visible = true;
                setting = true;
                Properties.Settings.Default.PSetting = true;
                Properties.Settings.Default.Save();
            }
            else if (setting == false)
            {
                button1.Visible = false;
                setting = false;
                Properties.Settings.Default.PSetting = false;
                Properties.Settings.Default.Save();
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                radioButton1.Checked = false;
                Properties.Settings.Default.PSetting = false;
                setting = false;
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
                MessageBox.Show("Изменения войдут в силу после перезагрузки приложения.", "Предупреждение!!!", MessageBoxButtons.OK);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (d == true)
            {
                button1.BackColor = Color.Green;
                button2.BackColor = Color.Red;
                button4.BackColor = Color.Purple;
                listView1.BackColor = Color.Cyan;
                label8.Text = "  Нажав зелёную кнопку(если кнопки нет, зайди в настройки и выбери'по кнопке'), бот проверит есть ли сегодня \r\nу твоих друзей день рождение,\r\nесли есть, бот на стене твоего друга или подруге\r\nоставит пост(от твоего имени), сообщение которого можно изменить нажав на фиолетовую кнопку,\r\nтак же вы сможете добавить фото, если вы укажите ссылку фото(из вк).\r\nЕщё бот отправляет поздравления девочкам на 8 марта или мальчиков на 23 февраля.\r\n" +
                    "  Нажав на красную кнопку, вы отправляет пост всем вашим друзьям с сообщение,\r\n которое вы написали под кнопкой.\r\n" +
                    "  Нажав на фиолетовую кнопку, вы увидите и сможите изменить настройки.\r\n" +
                    "  Голубым цветом показанна таблица. В это таблице появится список ваших друзей,\r\n если вы нажмёте на красную или зелёную кнопку.\r\nЕщё в этой таблице вы сможите увидеть id,\r\nпол(1-женский;2-мужской), дату рождения друзей.\r\nТак же вы сможите увидеть можно ли вам оставлять пост на странице друга,\r\nесли нет вы всё равно позравите друга или подругу в коментариях под пследним постом.\r\n" +
                    "  Нажмите ещё раз на кнопку'Как пользоваться?'\r\nчтобы скрыть текст изменить цвет кнопок на изначальное.";
                d = false;
            }
            else if (d == false)
            {
                button1.BackColor = Color.LightGray;
                button2.BackColor = Color.LightGray;
                button4.BackColor = Color.LightGray;
                listView1.BackColor = Color.White;
                label8.Text = "";
                d = true;
            }
        }

    }
}
