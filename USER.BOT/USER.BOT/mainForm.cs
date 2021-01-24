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
            textBox2.Text = Properties.Settings.Default.id_Groups;
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

        private void Timer1_Tick(object sender, EventArgs e)
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

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Properties.Settings.Default.ChatBot = true;
                checkBox1.Text = "Чат-игра бот вкл";
            }
            if (checkBox1.Checked == false)
            {
                checkBox1.Text = "Чат-игра бот выкл";
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
                    random_id = last.last_message.random_id.ToString();
                    string SendMessages5 = "https://api.vk.com/method/storage.get?keys=balance&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                    string AnswerSendMessages5 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages5));
                    MessagesGet rtf2 = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages5);
                    foreach (MessagesGet.Response key in rtf2.response)
                    {
                        if (key.value == "" || key.value == null)
                        {
                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=1&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                        }
                        else
                        {
                            int balance = Convert.ToInt32(key.value);
                            if (last.last_message.text == "Старт" || last.last_message.text == "старт")
                            {

                                string SendMessages30 = "https://api.vk.com/method/storage.get?keys=users&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages30 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages30));
                                MessagesGet rtf60 = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages30);
                                foreach (MessagesGet.Response key1 in rtf60.response)
                                {
                                    string[] users = key1.value.Split(new[] { "-"}, StringSplitOptions.RemoveEmptyEntries);
                                    if (!users.Contains(last.last_message.peer_id.ToString()))
                                    {
                                        key1.value += last.last_message.peer_id.ToString();

                                        string SendMessages31 = "https://api.vk.com/method/storage.set?&key=users&value= " + key1.value + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages31 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages31));
                                    }
                                }
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }

                                string SendMessages = "https://api.vk.com/method/storage.set?key=balance&value=1&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                string SendMessages1 = "https://api.vk.com/method/messages.send?message=Тебе дали стартовый баланс в 1₽. Напиши Баланс, чтобы узнать 'баланс'." + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                            else if (last.last_message.text == "Баланс" || last.last_message.text == "баланс")
                            {
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }

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
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }

                                string SendMessages2 = "https://api.vk.com/method/storage.getKeys?user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages2 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages2));

                                string SendMessages1 = "https://api.vk.com/method/storage.get?keys=balance&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                MessagesGet rtf = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages1);
                                int casino = random.Next(1, 3);

                                if (casino == 1)
                                {
                                    Properties.Settings.Default.lose += balance;
                                    balance = 1;

                                    string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=10&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));

                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Ставка не выиграла. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                                else if (casino == 2)
                                {
                                    balance *= 2;
                                    Properties.Settings.Default.income += balance;

                                    string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));

                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Ставка выиграла. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                }
                            }
                            // завод, промышленость, магазины, услуги, фонды 
                            // чёрный рынок
                            else if (last.last_message.text == "Купить бизнес" || last.last_message.text == "купить бизнес")
                            {//статистика
                             //уведомление о сгорании денег
                             //сгорание денег
                             //больше денег
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }

                                string SendMessages = "https://api.vk.com/method/messages.send?message=Выбери бизнес: \r\n" +
                                    "Торговая точка: цена 1 000₽; доход 50₽ в день\r\n" +
                                    "Магазин выпечки: цена 50 000₽; доход 100₽ в день\r\n" +
                                    "Магазин цветов: цена 100 000₽; доход 1 000₽ в день\r\n" +
                                    "Магазин обуви: цена 150 000₽; доход 2 000₽ в день\r\n" +
                                    "Магазин продуктов: цена 150 000₽; доход 2 000₽ в день\r\n" +
                                    "Магнит: цена 1 000 000₽; доход 20 000₽ в день\r\n" +
                                    "Пятёрочка: цена 1 000 000₽; доход 20 000₽ в день\r\n" +
                                    "Торговый центр: цена 500 000₽; доход 6 000₽ в день\r\n" +
                                    "Завод: цена 1 500 000₽; доход 50 000₽ в день\r\n" +
                                    "Яблоко: цена  500 000 000₽; доход 100 000₽ в день\r\n" +
                                    "Гугл: цена 1 000 000 000₽; доход 1 000 000₽ в день\r\n" +
                                    "Напиши купить и название бизнеса с изменением падежа" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                            }
                            else if (last.last_message.text == "Купить торговую точку" || last.last_message.text == "купить торговую точку")
                            {
                                if (balance >= 1000)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 1000;
                                            Properties.Settings.Default.biznes++;

                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили Торговую точку, теперь ты получаешь 50₽ в день. Баланс: " + balance + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 10 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text.ToLower() == "купить магазин выпечки")
                            {
                                if (balance >= 50000)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date1" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 50000;
                                            Properties.Settings.Default.biznes++;

                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date1&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили Магазин выпечки, теперь ты получаешь 100₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 50 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text.ToLower() == "купить магазин цветов")
                            {
                                if (balance >= 100000)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date2" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 100000;
                                            Properties.Settings.Default.biznes++;

                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date2&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили Магазин цветов, теперь ты получаешь 1000₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя уже есть магазин цветов!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 100 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text.ToLower() == "купить магазин обуви")
                            {
                                if (balance >= 150000)
                                {
                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date3" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (Properties.Settings.Default.startname == 0)
                                        {
                                            Properties.Settings.Default.startname = last.last_message.peer_id;
                                        }

                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 150000;
                                            Properties.Settings.Default.biznes++;

                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date3&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили Магазин обуви, теперь ты получаешь 2000₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя уже есть магазин обуви!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 150 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text.ToLower() == "купить магазин продуктов")
                            {
                                if (balance >= 150000)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date4" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 150000;
                                            Properties.Settings.Default.biznes++;

                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date4&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили Магазин продуктов, теперь ты получаешь 2000₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя уже есть магазин продуктов!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 150 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text.ToLower() == "купить магнит")
                            {
                                if (balance >= 1000000)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date5" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 1000000;
                                            Properties.Settings.Default.biznes++;

                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date5&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили сеть магазинов магнит, теперь ты получаешь 20000₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя уже есть сеть магазинов магнит!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 1 000 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text.ToLower() == "купить пятёрочку")
                            {
                                if (balance >= 1000000)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date6" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 1000000;
                                            Properties.Settings.Default.biznes++;

                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date6&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили сеть магазинов пятёрочка, теперь ты получаешь 20000₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя уже есть сеть магазинов пятёрочка!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 1 000 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text.ToLower() == "купить торговый центр")
                            {
                                if (balance >= 500000)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date7" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 500000;
                                            Properties.Settings.Default.biznes++;
                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date7&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили торговый центр, теперь ты получаешь 6 000₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя уже есть торговый центр!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 500 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text.ToLower() == "купить завод")
                            {
                                if (balance >= 1500000)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date8" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 1500000;
                                            Properties.Settings.Default.biznes++;
                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date8&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили завод, теперь ты получаешь 50 000₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя уже есть завод!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 1 500 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text.ToLower() == "купить яблоко")
                            {
                                if (balance >= 500000000)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date9" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 500000000;
                                            Properties.Settings.Default.biznes++;
                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date9&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили магазин техники яблоко, теперь ты получаешь 100 000₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя уже есть яблоко!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 500 000 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text.ToLower() == "купить гугл")
                            {
                                if (balance >= 1000000000)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date10" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d1 in rtf1.response)
                                    {
                                        if (d1.value == "" || d1.value == null)
                                        {
                                            balance -= 1000000000;
                                            Properties.Settings.Default.biznes++;
                                            DateTime date11 = DateTime.Now;
                                            string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                            string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date10&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Вы купили гугл, теперь ты получаешь 1 000 000₽ в день. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя уже есть гугл!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                }
                                else
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег. Стоимость 1 000 000 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            else if (last.last_message.text == "Доход" || last.last_message.text == "доход")
                            {
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }

                                DateTime date11 = DateTime.Now;
                                string datenow11 = date11.Day.ToString() + "." + date11.Month;

                                string SendMessages2 = "https://api.vk.com/method/storage.getKeys?user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages2 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages2));

                                string CasinoMessages = "https://api.vk.com/method/storage.get?keys=last_date" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));
                                MessagesGet rtf1 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages);

                                foreach (MessagesGet.Response d1 in rtf1.response)
                                {
                                    string CasinoMessage = "https://api.vk.com/method/storage.get?keys=last_date" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessage = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessage));
                                    MessagesGet rtf = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessage);

                                    foreach (MessagesGet.Response d in rtf.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 50;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от торговой точки: 50₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string SendMessages3 = "https://api.vk.com/method/storage.getKeys?user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages3 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages3));

                                    string CasinoMessages3 = "https://api.vk.com/method/storage.get?keys=last_date1" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages3 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages3));
                                    MessagesGet rtf3 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages3);
                                    foreach (MessagesGet.Response d in rtf3.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 100;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от магазина выпечки: 100₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date1&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string CasinoMessages4 = "https://api.vk.com/method/storage.get?keys=last_date2" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages4 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages4));
                                    MessagesGet rtf4 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages4);
                                    foreach (MessagesGet.Response d in rtf4.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 1000;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от магазина цветов: 1 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date2&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date3" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    MessagesGet rtf6 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                    foreach (MessagesGet.Response d in rtf6.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 2000;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от магазина обуви: 2 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date3&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string CasinoMessages6 = "https://api.vk.com/method/storage.get?keys=last_date4" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages6 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages6));
                                    MessagesGet rtf7 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages6);
                                    foreach (MessagesGet.Response d in rtf7.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 2000;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от магазина продуктов: 2 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date4&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string CasinoMessages7 = "https://api.vk.com/method/storage.get?keys=last_date5" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages7 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages7));
                                    MessagesGet rtf8 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages7);
                                    foreach (MessagesGet.Response d in rtf8.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 20000;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от сети магазино магнит: 20 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date5&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string CasinoMessages8 = "https://api.vk.com/method/storage.get?keys=last_date6" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages8 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages8));
                                    MessagesGet rtf9 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages8);
                                    foreach (MessagesGet.Response d in rtf9.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 20000;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от сети магазино пятёрочка: 20 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date6&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string CasinoMessages9 = "https://api.vk.com/method/storage.get?keys=last_date7" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages9 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages9));
                                    MessagesGet rtf10 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages9);
                                    foreach (MessagesGet.Response d in rtf10.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 6000;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от торгового центра: 6 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date7&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string CasinoMessages10 = "https://api.vk.com/method/storage.get?keys=last_date8" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages10 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages10));
                                    MessagesGet rtf11 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages10);
                                    foreach (MessagesGet.Response d in rtf11.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 50000;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от завода: 50 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date8&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string CasinoMessages11 = "https://api.vk.com/method/storage.get?keys=last_date9" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages11 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages11));
                                    MessagesGet rtf12 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages11);
                                    foreach (MessagesGet.Response d in rtf12.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 100000;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от яблока: 100 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date9&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string CasinoMessages12 = "https://api.vk.com/method/storage.get?keys=last_date10" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages12 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages12));
                                    MessagesGet rtf13 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages12);
                                    foreach (MessagesGet.Response d in rtf13.response)
                                    {
                                        if (d.value != datenow11)
                                        {
                                            if (d.value != "")
                                            {
                                                balance += 1000000;
                                                Properties.Settings.Default.income += balance;

                                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                                string SendMessages = "https://api.vk.com/method/messages.send?message=Ваш доход от гуглп: 1 000 000₽. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date10&value=" + datenow11 + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                            }
                                        }
                                        else
                                        {
                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Сегодня ты уже забрал свои деньги" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                    }
                                    string SendMessages33 = "https://api.vk.com/method/messages.send?message=Твой баланс: " + balance + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages33 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages33));
                                }
                            }
                            else if (last.last_message.text == "Дай млн" || last.last_message.text == "дай млн")
                            {
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }

                                balance += 1000000;
                                Properties.Settings.Default.income += balance;
                                string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                string SendMessages = "https://api.vk.com/method/messages.send?message=Держи свой млн. Баланс: " + balance + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                            }
                            else if (last.last_message.text == "Привет" || last.last_message.text == "привет")
                            {
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }

                                string SendMessages = "https://api.vk.com/method/messages.send?message=Приветствую тебя" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                            }
                            else if (last.last_message.text == "Как дела" || last.last_message.text == "как дела" || last.last_message.text == "Как дела?" || last.last_message.text == "как дела?")
                            {
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }

                                string SendMessages = "https://api.vk.com/method/messages.send?message=Пойдёт" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                            }
                            else if (last.last_message.text == "Начать" || last.last_message.text == "начать")
                            {
                                string SendMessages30 = "https://api.vk.com/method/storage.get?keys=users&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages30 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages30));
                                MessagesGet rtf60 = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages30);
                                foreach (MessagesGet.Response key1 in rtf60.response)
                                {
                                    string[] users = key1.value.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (!users.Contains(last.last_message.peer_id.ToString()))
                                    {
                                        key1.value += last.last_message.peer_id.ToString();

                                        string SendMessages31 = "https://api.vk.com/method/storage.set?&key=users&value= " + key1.value + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages31 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages31));
                                    }
                                }
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }

                                string SendMessages8 = "https://api.vk.com/method/storage.set?key=balance&value=1&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages8 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages8));

                                string SendMessages = "https://api.vk.com/method/messages.send?message=Привет! Напиши «старт», чтобы получить свой стартовый капитал или Напиши «купить бизнес», чтобы ознакомиться с покупкой бизнеса. Ты можешь делать ставки, только напиши «ставка на всё»(Дисклеймер: это просто игра. ₽ - это игровая валюта и ничего более). Так же ты можешь играть в «камень, ножницы, бумага», написав: камень, ножницы или бумагу. Напиши «help» или «помоги», чтобы узнать весь список команд и их действия." + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                            }
                            else if (last.last_message.text == "Ножницы" || last.last_message.text == "ножницы")
                            {
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }
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

                                    balance += 100;
                                    Properties.Settings.Default.income += balance;
                                    string SendMessages8 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages8 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages8));
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
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }
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

                                    balance += 100;
                                    Properties.Settings.Default.income += balance;
                                    string SendMessages8 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages8 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages8));
                                }
                            }
                            else if (last.last_message.text == "Камень" || last.last_message.text == "камень")
                            {
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }
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

                                    balance += 100;
                                    Properties.Settings.Default.income += balance;
                                    string SendMessages8 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages8 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages8));

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
                                    "Камень, ножницы или бумага - ты начинаешь играешь в кмн с ботом\r\n" +
                                    "Ты не существующую команду, я узнаю сколько символов в твоём сообщении, умножу на 3 и прибавлю к твоему балансу" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                            }
                            else if (last.last_message.text == "удалить" || last.last_message.text == "Удалить")
                            {
                                if (Properties.Settings.Default.startname == 0)
                                {
                                    Properties.Settings.Default.startname = last.last_message.peer_id;
                                }
                                string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date&value=" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                string SendMessages2 = "https://api.vk.com/method/storage.set?key=last_date1&value=" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages2 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages2));

                                string SendMessages4 = "https://api.vk.com/method/storage.set?key=last_date2&value=" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages4 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages4));

                                string SendMessages9 = "https://api.vk.com/method/storage.set?key=last_date3&value=" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages9 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages9));

                                string SendMessages6 = "https://api.vk.com/method/storage.set?key=last_date4&value=" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages6 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages6));

                                string SendMessages7 = "https://api.vk.com/method/storage.set?key=last_date5&value=" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages7 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages7));

                                string SendMessages8 = "https://api.vk.com/method/storage.set?key=last_date6&value=" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages8 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages8));

                                string SendMessages10 = "https://api.vk.com/method/storage.set?key=last_date7&value=" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages10 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages10));

                                string SendMessages11 = "https://api.vk.com/method/storage.set?key=last_date8&value=" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages11 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages11));

                                string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=1" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));

                                string SendMessages3 = "https://api.vk.com/method/messages.send?message=Теперь ты сможешь начать игру занова" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerSendMessages3 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages3));
                            }
                            else
                            {
                                string CasinoMessages4 = "https://api.vk.com/method/storage.get?key=save&value=" + last.last_message + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages4 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages4));
                                MessagesGet rtf8 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages4);
                                foreach (MessagesGet.Response d in rtf8.response)
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }
                                    if (last.last_message.text != d.value)
                                    {
                                        string click = last.last_message.text;
                                        int a = click.Length;
                                        int b = a * 3;
                                        balance += b;
                                        Properties.Settings.Default.income += b;
                                        string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                        string SendMessages3 = "https://api.vk.com/method/messages.send?message=Ты написал " + a + " символов, теперь твой баланс " + balance + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages3 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages3));

                                        string CasinoMessages5 = "https://api.vk.com/method/storage.set?key=save&value=" + last.last_message + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                    }
                                    else
                                    {
                                        string SendMessages3 = "https://api.vk.com/method/messages.send?message=АХАХАХА! Копировать и вставить, попробуй что нибудь получше!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages3 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages3));
                                    }
                                }
                                //int end = random.Next(1, 4);
                                //if (end == 1)
                                //{
                                //    messagesEnd = "Я не понимаю :(";
                                //}
                                //else if (end == 2)
                                //{
                                //    messagesEnd = "Что это?";
                                //}
                                //else if (end == 3)
                                //{
                                //    messagesEnd = "Я так не умею :(";
                                //}

                                //string SendMessages = "https://api.vk.com/method/messages.send?message=" + messagesEnd + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                //string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                            }

                        }
                    }
                }

                DateTime date = DateTime.Now;
                int hour = date.Hour;
                if (hour < 18)
                {
                    Properties.Settings.Default.startname = 0;
                    Properties.Settings.Default.income = 0;
                    Properties.Settings.Default.lose = 0;
                    Properties.Settings.Default.biznes = 0;
                    Properties.Settings.Default.stats = false;
                    Properties.Settings.Default.fire = false;
                }
                if(hour >= 22 && Properties.Settings.Default.fire == false)
                {
                    string SendMessages30 = "https://api.vk.com/method/storage.get?keys=users&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                    string AnswerSendMessages30 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages30));
                    MessagesGet rtf60 = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages30);
                    foreach (MessagesGet.Response key1 in rtf60.response)
                    {
                        DateTime date11 = DateTime.Now;
                        string datenow11 = date11.Day.ToString() + "." + date11.Month;

                        string[] users = key1.value.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        int max = users.Length;
                        for (int i = 0; i <= max; i++)
                        {
                            string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                            MessagesGet rtf12 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                            foreach (MessagesGet.Response d1 in rtf12.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));
                                }
                            }
                            string CasinoMessages6 = "https://api.vk.com/method/storage.get?keys=last_date2" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages6 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages6));
                            MessagesGet rtf2 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages6);
                            foreach (MessagesGet.Response d1 in rtf2.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date2&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                }
                            }
                            string CasinoMessages7 = "https://api.vk.com/method/storage.get?keys=last_date3" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages7 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages7));
                            MessagesGet rtf3 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages7);
                            foreach (MessagesGet.Response d1 in rtf3.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date3&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                }
                            }
                            string CasinoMessages8 = "https://api.vk.com/method/storage.get?keys=last_date4" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages8 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages8));
                            MessagesGet rtf4 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages8);
                            foreach (MessagesGet.Response d1 in rtf4.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date4&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                }
                            }
                            string CasinoMessages9 = "https://api.vk.com/method/storage.get?keys=last_date5" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages9 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages9));
                            MessagesGet rtf6 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages9);
                            foreach (MessagesGet.Response d1 in rtf6.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date5&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                }
                            }
                            string CasinoMessages1 = "https://api.vk.com/method/storage.get?keys=last_date6" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages1 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages1));
                            MessagesGet rtf = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages1);
                            foreach (MessagesGet.Response d1 in rtf.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date6&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                }
                            }
                            string CasinoMessages2 = "https://api.vk.com/method/storage.get?keys=last_date7" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));
                            MessagesGet rtf7 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages2);
                            foreach (MessagesGet.Response d1 in rtf7.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date7&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                }
                            }
                            string CasinoMessages3 = "https://api.vk.com/method/storage.get?keys=last_date8" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages3 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages3));
                            MessagesGet rtf8 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages3);
                            foreach (MessagesGet.Response d1 in rtf8.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date8&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                }
                            }
                            string CasinoMessages4 = "https://api.vk.com/method/storage.get?keys=last_date9" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages4 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages4));
                            MessagesGet rtf80 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages4);
                            foreach (MessagesGet.Response d1 in rtf80.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date9&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                }
                            }
                            string CasinoMessages10 = "https://api.vk.com/method/storage.get?keys=last_date10" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages10 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages10));
                            MessagesGet rtf10 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages10);
                            foreach (MessagesGet.Response d1 in rtf10.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date10&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                }
                            }
                            string CasinoMessages11 = "https://api.vk.com/method/storage.get?keys=last_date11" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages11 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages11));
                            MessagesGet rtf11 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages11);
                            foreach (MessagesGet.Response d1 in rtf11.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                    string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date11&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                }
                            }
                        }
                    }
                    Properties.Settings.Default.fire = true;
                }
                if (hour >= 18 && Properties.Settings.Default.stats == false)
                {

                    string BanFriendsId = "https://api.vk.com/method/users.get?user_id=" + Properties.Settings.Default.startname + "&fields=deactivated" + "&" + access_token + "&v=5.124";
                    string AnswerBanFriends = Encoding.UTF8.GetString(cl.DownloadData(BanFriendsId));
                    BanFriends rtf1 = JsonConvert.DeserializeObject<BanFriends>(AnswerBanFriends);
                    string one_name = "";
                    foreach (BanFriends.ResponseBanFriends item1 in rtf1.response)
                    {
                        one_name = item1.first_name + " " + item1.last_name;
                    }
                    string best = "Сегодня первым начал играть @id" + Properties.Settings.Default.startname + "(" + one_name + ").\r\n" +
                        "Сумма заработка всех пользователей: " + Properties.Settings.Default.income + "₽.\r\n"
                        + "Куплено бизнесов " + Properties.Settings.Default.biznes+ ".\r\n" +
                        "Проигранно " + Properties.Settings.Default.lose + "₽ в казино.";

                    string Request = "https://api.vk.com/method/wall.post?message=" + best + "&owner_id=-" + Properties.Settings.Default.id_Groups + "&" + access_token + "&v=5.124";
                    string AnswerFriends1 = Encoding.UTF8.GetString(cl.DownloadData(Request));
                    Properties.Settings.Default.stats = true;

                    string SendMessages30 = "https://api.vk.com/method/storage.get?keys=users&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                    string AnswerSendMessages30 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages30));
                    MessagesGet rtf60 = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages30);
                    foreach (MessagesGet.Response key1 in rtf60.response)
                    {
                        DateTime date11 = DateTime.Now;
                        string datenow11 = date11.Day.ToString() + "." + date11.Month;

                        string[] users = key1.value.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        int max = users.Length;
                        for (int i = 0; i <= max; i++)
                        {
                            string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                            MessagesGet rtf12 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                            foreach (MessagesGet.Response d1 in rtf12.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            string CasinoMessages6 = "https://api.vk.com/method/storage.get?keys=last_date2" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages6 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages6));
                            MessagesGet rtf2 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages6);
                            foreach (MessagesGet.Response d1 in rtf2.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            string CasinoMessages7 = "https://api.vk.com/method/storage.get?keys=last_date3" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages7 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages7));
                            MessagesGet rtf3 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages7);
                            foreach (MessagesGet.Response d1 in rtf3.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            string CasinoMessages8 = "https://api.vk.com/method/storage.get?keys=last_date4" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages8 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages8));
                            MessagesGet rtf4 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages8);
                            foreach (MessagesGet.Response d1 in rtf4.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            string CasinoMessages9 = "https://api.vk.com/method/storage.get?keys=last_date5" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages9 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages9));
                            MessagesGet rtf6 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages9);
                            foreach (MessagesGet.Response d1 in rtf6.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            string CasinoMessages1 = "https://api.vk.com/method/storage.get?keys=last_date6" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages1 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages1));
                            MessagesGet rtf = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages1);
                            foreach (MessagesGet.Response d1 in rtf.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            string CasinoMessages2 = "https://api.vk.com/method/storage.get?keys=last_date7" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));
                            MessagesGet rtf7 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages2);
                            foreach (MessagesGet.Response d1 in rtf7.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            string CasinoMessages3 = "https://api.vk.com/method/storage.get?keys=last_date8" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages3 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages3));
                            MessagesGet rtf8 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages3);
                            foreach (MessagesGet.Response d1 in rtf8.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            string CasinoMessages4 = "https://api.vk.com/method/storage.get?keys=last_date9" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages4 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages4));
                            MessagesGet rtf80 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages4);
                            foreach (MessagesGet.Response d1 in rtf80.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            string CasinoMessages10 = "https://api.vk.com/method/storage.get?keys=last_date10" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages10 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages10));
                            MessagesGet rtf10 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages10);
                            foreach (MessagesGet.Response d1 in rtf10.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
                            string CasinoMessages11 = "https://api.vk.com/method/storage.get?keys=last_date11" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                            string AnswerCasinoMessages11 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages11));
                            MessagesGet rtf11 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages11);
                            foreach (MessagesGet.Response d1 in rtf11.response)
                            {
                                if (d1.value != datenow11)
                                {
                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                            }
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
            WebClient cl = new WebClient();

            string SendMessages31 = "https://api.vk.com/method/storage.set?&key=users&value= &user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
            string AnswerSendMessages31 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages31));
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
                MessageBox.Show("введи access token!\r\n" +
                    "Как найти access token сообщества?\r\n" +
                    "1. Создать сообщество или найти сообщество где ты руковадитель\r\n" +
                    "2. Зайти в настройки сообщества=>работа с API \r\n" +
                    "3. создай если нет\r\n"+
                    "4. Скопировать access token и вставить ниже");
            }
        }


        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.id_Groups = textBox2.Text;
            if (textBox2.Text == "")
            {
                checkBox1.Checked = false;
                Properties.Settings.Default.ChatBot = false;
            }
        }

        private void TextBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "Суда введи id сообщества")
            {
                textBox2.Text = "";
            }
        }

        private void ButtonLiking_Click(object sender, EventArgs e)
        {
            LikerForm form = new LikerForm();
            form.access_token = access_token;
            form.users_id = user_id;
            form.ShowDialog();
        }
   


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.ToString().Contains("access_token"))
            {
                string[] param = webBrowser1.Url.ToString().Split(new[] { "#", "&" }, StringSplitOptions.RemoveEmptyEntries);
                access_token = param[1];

                string Request = "https://api.vk.com/method/account.getProfileInfo?" +
                access_token + "&v=5.124";

                WebClient cl = new WebClient();

                string Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));

                GetProfileInfo gpi = JsonConvert.DeserializeObject<GetProfileInfo>(Answer);
                labelFamily.Text = gpi.response.last_name;
                labelName.Text = gpi.response.first_name;
                user_id = gpi.response.id.ToString();

                Request = "https://api.vk.com/method/users.get?fields=photo_100&" +
                access_token + "&v=5.124";


                Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));

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
        }
        private void ButtonLiking_Click(object sender, EventArgs e)
        {
            LikerForm form = new LikerForm();
            LikerForm.access_token = access_token;
            LikerForm.users_id = user_id;
            form.ShowDialog();
        }
    }
}


