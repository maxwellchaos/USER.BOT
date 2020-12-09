﻿using System;
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
            webBrowser1.BringToFront();
            webBrowser1.Dock = DockStyle.Fill;

            webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=7614304" +
                "&display=page&redirect_uri=https://oauth.vk.com/blank.html&" +
                "scope=friends+groups+wall&" +
                "response_type=token&v=5.124&state=123456");
            if(Properties.Settings.Default.ChatBot == true)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
            textBox1.Text = Properties.Settings.Default.TokenChatBot;
            if (textBox1.Text == "")
            {
                textBox1.Text = "Суда введи access token сообщества";
                Properties.Settings.Default.TokenChatBot = "Суда введи access token сообщества";
            }
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
                    if (item1.deactivated == "deleted" || item1.deactivated == "banned")
                    {
                        string BanFriendsId1 = "https://api.vk.com/method/friends.delete?user_id=" + item.id + "&" + access_token + "&v=5.124";
                        WebClient cl11 = new WebClient();
                        string AnswerBanFriends1 = Encoding.UTF8.GetString(cl.DownloadData(BanFriendsId1));
                        BanName = BanName + item1.last_name + " " + item1.first_name + "; ";
                    }
                }
                if (BanName != null && check == false)
                {
                    MessageBox.Show("Эти друзья удаленны из списка друзей: " + BanName, "Внимание!", MessageBoxButtons.OK);
                    label1.Visible = false;
                    progressBar1.Visible = false;
                    check = true;
                }
                else if (BanName == null && check == false)
                {
                    MessageBox.Show("У тебя нет удалённых друзей.", "Внимание!", MessageBoxButtons.OK);
                    label1.Visible = false;
                    progressBar1.Visible = false;
                    check = true;
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
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            for (int d = 2; d < 2; d++)
            {

            }

            if (checkBox1.Checked == true)
            {
                Properties.Settings.Default.ChatBot = true;
            }
            if (checkBox1.Checked == false)
            {
                Properties.Settings.Default.ChatBot = false;
            }
            if (Properties.Settings.Default.ChatBot == true)
            {
                Random random = new Random();
                string NewMessages = "https://api.vk.com/method/messages.getConversations?filter=unread&start_message_id,group_id=-199121760&" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                WebClient cl = new WebClient();
                string AnswerNewMessages = Encoding.UTF8.GetString(cl.DownloadData(NewMessages));
                MessagesNew rtf5 = JsonConvert.DeserializeObject<MessagesNew>(AnswerNewMessages);

                foreach (MessagesNew.Item last in rtf5.response.items)
                {
                    string SendMessages5 = "https://api.vk.com/method/storage.get?keys=balance&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                    string AnswerSendMessages5 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages5));
                    MessagesGet rtf2 = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages5);
                    foreach (MessagesGet.Response key in rtf2.response)
                    {
                        int balance = Convert.ToInt32(key.value);
                        if (last.last_message.text == "Старт" || last.last_message.text == "старт")
                        {
                            string SendMessages = "https://api.vk.com/method/storage.set?key=balance&value=10000&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                            string SendMessages1 = "https://api.vk.com/method/messages.send?message=Тебе дали стартовый баланс в 10000₽. Напиши Баланс, чтобы узнать 'баланс'." + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                        }
                        else if (last.last_message.text == "Баланс" || last.last_message.text == "баланс")
                        {
                            string SendMessages2 = "https://api.vk.com/method/storage.getKeys?user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages2 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages2));

                            string SendMessages1 = "https://api.vk.com/method/storage.get?keys=balance&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            MessagesGet rtf = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages1);

                            string SendMessages = "https://api.vk.com/method/messages.send?message=Баланс: " + key.value + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                        }
                        else if (last.last_message.text == "Ставка на всё" || last.last_message.text == "ставка на всё" || last.last_message.text == "Ставка на все" || last.last_message.text == "ставка на все")
                        {
                            string SendMessages2 = "https://api.vk.com/method/storage.getKeys?user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages2 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages2));

                            string SendMessages1 = "https://api.vk.com/method/storage.get?keys=balance&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            MessagesGet rtf = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages1);
                            int casino = random.Next(1, 4);

                            if (casino == 1 || casino == 2)
                            {
                                balance = 10000;

                                string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=10000&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));

                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ставка не выиграла. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                            }
                            else if (casino == 3)
                            {
                                balance *= 2;

                                string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));

                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ставка выиграла. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                            }
                        }
                        else if (last.last_message.text == "Купить бизнес" || last.last_message.text == "купить бизнес")
                        {
                            string SendMessages = "https://api.vk.com/method/messages.send?message=Выбери бизнес: \r\n" +
                                "Торговая точка на рынке: цена 1 000₽; доход 5000₽ в день\r\n" +
                                "Магазин выпечки: цена 50 000₽; доход 10000₽ в день\r\n" +
                                "Напиши купить и название бизнеса с изменением падежа" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                        }
                        else if ((last.last_message.text == "Купить торговую точку" || last.last_message.text == "купить торговую точку") && balance >= 1000)
                        {
                            string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                            MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                            foreach (MessagesGet.Response d1 in rtf1.response)
                            {
                                if (d1.value == null)
                                {
                                    balance -= 1000;

                                    DateTime date11 = DateTime.Now;
                                    string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили Торговую точку, теперь ты получаешь 5000₽ в день. Баланс: " + balance + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Ты уже купил торговую точку, оставь другим!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }

                            }
                        }
                        //else if ((last.last_message.text == "Купить магазин выпечки" || last.last_message.text == "купить магазин выпечки") && balance >= 1000)
                        else if (( last.last_message.text.ToLower() == "купить магазин выпечки") && balance >= 1000)
                        {
                            string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date1" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                            MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                            foreach (MessagesGet.Response d1 in rtf1.response)
                            {
                                if (d1.value == null)
                                {
                                    balance -= 50000;

                                    DateTime date11 = DateTime.Now;
                                    string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date1&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили Магазин выпечки, теперь ты получаешь 10000₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Ты уже купил один магазин, зачем тебе ещё?" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                        }
                        else if (last.last_message.text == "Доход" || last.last_message.text == "доход")
                        {
                            DateTime date11 = DateTime.Now;
                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                            string SendMessages2 = "https://api.vk.com/method/storage.getKeys?user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages2 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages2));

                            string CasinoMessages = "https://api.vk.com/method/storage.get?keys=last_date" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                            MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages);

                            foreach (MessagesGet.Response d1 in rtf1.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    balance += 5000;

                                    string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от торговой точки: 5000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                                string SendMessages3 = "https://api.vk.com/method/storage.getKeys?user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages3 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages3));

                                string CasinoMessages3 = "https://api.vk.com/method/storage.get?keys=last_date" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages3 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages3));
                                MessagesGet rtf3 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages3);
                                foreach (MessagesGet.Response d in rtf3.response)
                                {
                                    if (d1.value != datenow11)
                                    {
                                        balance += 10000;

                                        string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от магазина выпечки: 5000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                        string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date1&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                    }
                                    else
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                    }
                                }
                            }
                        }
                        else if (last.last_message.text == "Дай млн" || last.last_message.text == "дай млн")
                        {
                            balance += 1000000;
                            string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                            string SendMessages = "https://api.vk.com/method/messages.send?message=Держи свой млн. Баланс: " + balance + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                        }
                        else if (last.last_message.text == "Привет" || last.last_message.text == "привет")
                        {
                            string SendMessages = "https://api.vk.com/method/messages.send?message=Приветствую тебя" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                        }
                        else if (last.last_message.text == "Как дела" || last.last_message.text == "как дела" || last.last_message.text == "Как дела?" || last.last_message.text == "как дела?")
                        {
                            string SendMessages = "https://api.vk.com/method/messages.send?message=Пойдёт" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                        }
                        else if (last.last_message.text == "Начать" || last.last_message.text == "начать")
                        {
                            string SendMessages = "https://api.vk.com/method/messages.send?message=Привет! Напиши «старт», чтобы получить свой стартовый капитал или Напиши «купить бизнес», чтобы ознакомиться с покупкой бизнеса. Ты можешь делать ставки, только напиши «ставка на всё»(Дисклеймер: это просто игра. ₽ - это игровая валюта и ничего более). Так же ты можешь играть в «камень, ножницы, бумага», написав: камень, ножницы или бумагу. Напиши «help» или «помоги», чтобы узнать весь список команд и их действия." + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                        }
                        else if (last.last_message.text == "Ножницы" || last.last_message.text == "ножницы")
                        {
                            int kmn = random.Next(1, 4);
                            // 1 ножницы
                            // 2 бумага
                            // 3 камень
                            if (kmn == 1)
                            {
                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ножницы" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                string SendMessages1 = "https://api.vk.com/method/messages.send?message=Ничья" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                            if (kmn == 2)
                            {
                                string SendMessages = "https://api.vk.com/method/messages.send?message=Бумага" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                string SendMessages1 = "https://api.vk.com/method/messages.send?message=Ты выиграл" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                            if (kmn == 3)
                            {
                                string SendMessages = "https://api.vk.com/method/messages.send?message=Камень" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                string SendMessages1 = "https://api.vk.com/method/messages.send?message=Ты проиграл" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                        }
                        else if (last.last_message.text == "Бумага" || last.last_message.text == "бумага")
                        {
                            int kmn = random.Next(1, 4);
                            // 1 ножницы
                            // 2 бумага
                            // 3 камень
                            if (kmn == 1)
                            {
                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ножницы" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                string SendMessages1 = "https://api.vk.com/method/messages.send?message=Ты проиграл" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                            if (kmn == 2)
                            {
                                string SendMessages = "https://api.vk.com/method/messages.send?message=Бумага" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                string SendMessages1 = "https://api.vk.com/method/messages.send?message=Ничья" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                            if (kmn == 3)
                            {
                                string SendMessages = "https://api.vk.com/method/messages.send?message=Камень" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                string SendMessages1 = "https://api.vk.com/method/messages.send?message=Ты выиграл" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                        }
                        else if (last.last_message.text == "Камень" || last.last_message.text == "камень")
                        {
                            int kmn = random.Next(1, 4);
                            // 1 ножницы
                            // 2 бумага
                            // 3 камень
                            if (kmn == 1)
                            {
                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ножницы" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                string SendMessages1 = "https://api.vk.com/method/messages.send?message=Ты выиграл" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                            if (kmn == 2)
                            {
                                string SendMessages = "https://api.vk.com/method/messages.send?message=Бумага" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                string SendMessages1 = "https://api.vk.com/method/messages.send?message=Ты проиграл" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                            if (kmn == 3)
                            {
                                string SendMessages = "https://api.vk.com/method/messages.send?message=Камень" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                string SendMessages1 = "https://api.vk.com/method/messages.send?message=Ничья" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                        }
                        else if (last.last_message.text.ToLower() == "помоги" || last.last_message.text.ToLower() == "help")
                        {
                            string SendMessages1 = "https://api.vk.com/method/messages.send?message=Начать - ознакомление с игрой\r\n" +
                                "Ставка на всё - ты ставишь все свои деньги на кон, если ты выиграл твой баланс удваивается, если проиграл у тебя всегда останется 10 000\r\n" +
                                "Купить бизнес - знакомление с покупкой бизнеса, цены и доход\r\n" +
                                "Купить `название бизнеса` - если утебя хватает денег ты покупаешь этот бизнес(бизнес приносит определённую сумму раз в день)\r\n" +
                                "доход - получить доход со всех бизнесов\r\n" +
                                "Камень, ножницы или бумага - ты начинаешь играешь в кмн с ботом" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                        }
                        else
                                {
                            int end = random.Next(1, 4);
                            if (end == 1)
                            {
                                messagesEnd = "Я не понимаю :(";
                            }
                            else if (end == 2)
                            {
                                messagesEnd = "Что это?";
                            }
                            else if (end == 3)
                            {
                                messagesEnd = "Я так не умею :(";
                            }

                            string SendMessages = "https://api.vk.com/method/messages.send?message=" + messagesEnd + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                        }
                    }
                }

            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(textBox1.Text == "Суда введи access token сообщества")
            {
                textBox1.Text = "";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.TokenChatBot = textBox1.Text;
            if (textBox1.Text == "")
            {
                checkBox1.Checked = false;
                Properties.Settings.Default.ChatBot = false;
            }
        }

        private void CheckBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox1.Text != "Суда введи access token сообщества" && textBox1.Text != "")
            {
                if (checkBox1.Checked == true)
                {
                    Properties.Settings.Default.ChatBot = true;
                }
                if (checkBox1.Checked == false)
                {
                    Properties.Settings.Default.ChatBot = false;
                }
            }
            else
            {
                MessageBox.Show("введи access token!");
            }
        }
    }    
}