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
    public partial class Chat_Bot_Form : Form
    {
        public string access_token;
        public string user_id;
        string random_id;
        int count = 0;
        WebClient cl = new WebClient();

        public Chat_Bot_Form()
        {
            InitializeComponent();
        }

        private void Chat_Bot_Form_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ChatBot == true)
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
            if(textBox2.Text == "" || textBox2.Text == "0" )
            {
                textBox2.Text = "Суда введи id сообщества";
                Properties.Settings.Default.id_Groups = "Суда введи id сообщества";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                //string SendMessages39 = "https://api.vk.com/method/storage.set?&key=users&value=264743807-56929156-286688521-418227022-380583406-327011638-&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                //string AnswerSendMessages39 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages39));

                if ((textBox1.Text == "" || textBox1.Text == "Суда введи access token сообщества") && (textBox2.Text == "" || textBox2.Text == "Суда введи id сообщества"))
                {
                    checkBox1.Checked = false;
                    MessageBox.Show("введи access token и id сообщества!\r\n" +
    "Как найти access token сообщества?\r\n" +
    "1. Создать сообщество или найти сообщество где ты руковадитель\r\n" +
    "2. Зайти в настройки сообщества=>работа с API \r\n" +
    "3. создай если нет\r\n" +
    "4. Скопировать access token и вставить ниже\r\n \r\n" +
    "Как найти id сообщества?\r\n" +
    "1. Зайти на страницу сообщества\r\n" +
    "2. Нажать на адресную строку браузера\r\n" +
    "3. Скопировать цифры поле слова public");

                }
                Properties.Settings.Default.ChatBot = true;
                checkBox1.Text = "Чат-игра бот вкл";
                if (count == 0)
                {
                    try
                    {
                        string SendMessages30 = "https://api.vk.com/method/storage.get?keys=users&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                        string AnswerSendMessages30 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages30));
                        MessagesGet rtf60 = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages30);
                        foreach (MessagesGet.Response key1 in rtf60.response)
                        {
                            try
                            {
                                string[] users = key1.value.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                                int a = users.Length;
                                for (int i = 0; i < a; i++)
                                {
                                    if (users[i].Contains(user_id))
                                    {
                                        count++;
                                        break;
                                    }
                                }
                                if (count == 0)
                                {
                                    key1.value += user_id.ToString() + "-";

                                    string SendMessages31 = "https://api.vk.com/method/storage.set?&key=users&value= " + key1.value + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages31 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages31));
                                }
                            }
                            catch (Exception ex)
                            {
                                timer1.Enabled = false;
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        timer1.Enabled = false;
                    }

                }
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
                try
                {
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
                                if (last.last_message.text.ToLower() == "старт")
                                {
                                    count = 0;
                                    string SendMessages30 = "https://api.vk.com/method/storage.get?keys=users&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages30 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages30));
                                    MessagesGet rtf60 = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages30);
                                    foreach (MessagesGet.Response key1 in rtf60.response)
                                    {
                                        string[] users = key1.value.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                                        int a = users.Length;
                                        for (int i = 0; i < a; i++)
                                        {
                                            if (users[i].Contains(user_id))
                                            {
                                                count++;
                                                break;
                                            }
                                        }
                                        if (count == 0)
                                        {
                                            key1.value += user_id.ToString() + "-";

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
                                else if (last.last_message.text.ToLower() == "баланс")
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
                                else if (last.last_message.text.ToLower() == "ставка на всё" || last.last_message.text.ToLower() == "ставка на все")
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string SendMessages2 = "https://api.vk.com/method/storage.getKeys?user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages2 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages2));

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
                                else if (last.last_message.text.Contains("ставка ")  || last.last_message.text.Contains("Cтавка "))
                                {
                                    string[] cash = null;
                                    if (last.last_message.text.Contains("ставка "))
                                    {
                                        cash = last.last_message.text.Split(new[] { "ставка" }, StringSplitOptions.RemoveEmptyEntries);
                                    }
                                    if(last.last_message.text.Contains("Cтавка "))
                                    {
                                        cash = last.last_message.text.Split(new[] { "Ставка" }, StringSplitOptions.RemoveEmptyEntries);
                                    }
                                    try
                                    {
                                        int a = Convert.ToInt32(cash[0]);
                                    if (a > balance)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=У тебя не хватает денег, чтобы сделать эту ставку&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                    }
                                    else if (a <= 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Ноль это не ставка&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                    }
                                    else
                                    {
                                        if (Properties.Settings.Default.startname == 0)
                                        {
                                            Properties.Settings.Default.startname = last.last_message.peer_id;
                                        }

                                        string SendMessages2 = "https://api.vk.com/method/storage.getKeys?user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages2 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages2));

                                        int casino = random.Next(1, 3);

                                        if (casino == 1)
                                        {
                                            Properties.Settings.Default.lose += balance;
                                                balance -= a;
                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=10&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Ставка не выиграла. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        }
                                        else if (casino == 2)
                                        {
                                            balance += a * 2;
                                            Properties.Settings.Default.income += balance;

                                            string CasinoMessages = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages));

                                            string SendMessages = "https://api.vk.com/method/messages.send?message=Ставка выиграла. Баланс: " + balance + "₽&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                        }
                                    }
                                }
                                    catch (Exception ex)
                {
                    string CasinoMessages4 = "https://api.vk.com/method/storage.get?key=save" + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                }
            }
                                // завод, промышленость, магазины, услуги, фонды 
                                // чёрный рынок
                                else if (last.last_message.text.ToLower() == "купить бизнес" || last.last_message.text.ToLower() == "бизнес"
                                    || last.last_message.text.ToLower() == "бизнесы")
                                {   //статистика
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
                                else if (last.last_message.text.ToLower() == "купить торговую точку")
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
                                else if (last.last_message.text.ToLower() == "доход")
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
                                else if (last.last_message.text.ToLower() == "дай млн")
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }
                                    string SendMessages4 = "https://api.vk.com/method/messages.send?message=Бан!&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages4 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages4));

                                    Application.DoEvents();
                                    Thread.Sleep(500);

                                    string SendMessages6 = "https://api.vk.com/method/messages.send?message=АХАХАХА. Шутка!&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages6 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages6));

                                    balance += 1000000;
                                    Properties.Settings.Default.income += balance;
                                    string CasinoMessages2 = "https://api.vk.com/method/storage.set?key=balance&value=" + balance + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));

                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Держи свой млн. Баланс: " + balance + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                                else if (last.last_message.text.ToLower() == "привет")
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Приветствую тебя" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                                else if (last.last_message.text.ToLower() == "как дела" || last.last_message.text.ToLower() == "как дела?")
                                {
                                    if (Properties.Settings.Default.startname == 0)
                                    {
                                        Properties.Settings.Default.startname = last.last_message.peer_id;
                                    }

                                    string SendMessages = "https://api.vk.com/method/messages.send?message=Пойдёт" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                }
                                else if (last.last_message.text.ToLower() == "начать")
                                {
                                    count = 0;
                                    string SendMessages30 = "https://api.vk.com/method/storage.get?keys=users&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                    string AnswerSendMessages30 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages30));
                                    MessagesGet rtf60 = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages30);
                                    foreach (MessagesGet.Response key1 in rtf60.response)
                                    {
                                        string[] users = key1.value.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                                        int a = users.Length;
                                        for (int i = 0; i < a; i++)
                                        {
                                            if (users[i].Contains(user_id))
                                            {
                                                count++;
                                                break;
                                            }
                                        }
                                        if (count == 0)
                                        {
                                            key1.value += user_id.ToString() + "-";

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
                                else if (last.last_message.text.ToLower() == "ножницы")
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
                                else if (last.last_message.text.ToLower() == "бумага")
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
                                else if (last.last_message.text.ToLower() == "камень")
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
                                else if (last.last_message.text.ToLower() == "помоги" || last.last_message.text.ToLower() == "help" || last.last_message.text.ToLower() == "помощь")
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
                                else if (last.last_message.text.ToLower() == "Удалить")
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
                                    string CasinoMessages4 = "https://api.vk.com/method/storage.get?key=save" +"&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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

                                            string CasinoMessages5 = "https://api.vk.com/method/storage.set?key=save&value=" + last.last_message.text + "&user_id=" + last.last_message.peer_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                        }
                                        else
                                        {
                                            string SendMessages3 = "https://api.vk.com/method/messages.send?message=АХАХАХА! Копировать и вставить, попробуй что нибудь получше!" + "&random_id=" + last.last_message.random_id + "&peer_id=" + last.last_message.peer_id + "" + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                            string AnswerSendMessages3 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages3));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }


                DateTime date = DateTime.Now;
                int hour = date.Hour;
                if (hour < 20)
                {
                    Properties.Settings.Default.startname = 0;
                    Properties.Settings.Default.income = 0;
                    Properties.Settings.Default.lose = 0;
                    Properties.Settings.Default.biznes = 0;
                    Properties.Settings.Default.stats = false;
                    Properties.Settings.Default.fire = false;
                }
                if (hour >= 22 && Properties.Settings.Default.fire == false)
                {
                    string SendMessages30 = "https://api.vk.com/method/storage.get?keys=users&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                    string AnswerSendMessages30 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages30));
                    MessagesGet rtf60 = JsonConvert.DeserializeObject<MessagesGet>(AnswerSendMessages30);
                    foreach (MessagesGet.Response key1 in rtf60.response)
                    {
                        try
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от торговой точки прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от магазина выпечки прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от магазина цветов прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от магазина обуви прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от магазина продуктов прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от Магнита прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от Пятёрочки прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от тогргового центра прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от Завода прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от Гугла прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
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
                                    if (d1.value != datenow11 && (d1.value != "" && d1.value != null))
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Прибыль от Яблока прогорела" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));

                                        string SendMessages1 = "https://api.vk.com/method/storage.set?key=last_date11&value=" + datenow11 + "&user_id=" + user_id + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages1 = Encoding.UTF8.GetString(cl.DownloadData(SendMessages1));

                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    Properties.Settings.Default.fire = true;
                }
                if (hour >= 20 && Properties.Settings.Default.stats == false)
                {
                    try
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
                            + "Куплено бизнесов " + Properties.Settings.Default.biznes + ".\r\n" +
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
                                int check = 0;
                                string CasinoMessages5 = "https://api.vk.com/method/storage.get?keys=last_date" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages5 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages5));
                                MessagesGet rtf12 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages5);
                                foreach (MessagesGet.Response d1 in rtf12.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                                string CasinoMessages6 = "https://api.vk.com/method/storage.get?keys=last_date2" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages6 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages6));
                                MessagesGet rtf2 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages6);
                                foreach (MessagesGet.Response d1 in rtf2.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                                string CasinoMessages7 = "https://api.vk.com/method/storage.get?keys=last_date3" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages7 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages7));
                                MessagesGet rtf3 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages7);
                                foreach (MessagesGet.Response d1 in rtf3.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                                string CasinoMessages8 = "https://api.vk.com/method/storage.get?keys=last_date4" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages8 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages8));
                                MessagesGet rtf4 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages8);
                                foreach (MessagesGet.Response d1 in rtf4.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                                string CasinoMessages9 = "https://api.vk.com/method/storage.get?keys=last_date5" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages9 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages9));
                                MessagesGet rtf6 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages9);
                                foreach (MessagesGet.Response d1 in rtf6.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                                string CasinoMessages1 = "https://api.vk.com/method/storage.get?keys=last_date6" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages1 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages1));
                                MessagesGet rtf = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages1);
                                foreach (MessagesGet.Response d1 in rtf.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                                string CasinoMessages2 = "https://api.vk.com/method/storage.get?keys=last_date7" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages2 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages2));
                                MessagesGet rtf7 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages2);
                                foreach (MessagesGet.Response d1 in rtf7.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                                string CasinoMessages3 = "https://api.vk.com/method/storage.get?keys=last_date8" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages3 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages3));
                                MessagesGet rtf8 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages3);
                                foreach (MessagesGet.Response d1 in rtf8.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                                string CasinoMessages4 = "https://api.vk.com/method/storage.get?keys=last_date9" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages4 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages4));
                                MessagesGet rtf80 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages4);
                                foreach (MessagesGet.Response d1 in rtf80.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                                string CasinoMessages10 = "https://api.vk.com/method/storage.get?keys=last_date10" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages10 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages10));
                                MessagesGet rtf10 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages10);
                                foreach (MessagesGet.Response d1 in rtf10.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                                string CasinoMessages11 = "https://api.vk.com/method/storage.get?keys=last_date11" + "&user_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                string AnswerCasinoMessages11 = Encoding.UTF8.GetString(cl.DownloadData(CasinoMessages11));
                                MessagesGet rtf11 = JsonConvert.DeserializeObject<MessagesGet>(AnswerCasinoMessages11);
                                foreach (MessagesGet.Response d1 in rtf11.response)
                                {
                                    if (d1.value != datenow11 && check == 0)
                                    {
                                        string SendMessages = "https://api.vk.com/method/messages.send?message=Если ты сегодня не заберёшь прибыль с бизнеса до 10 вечера, прибырь прогорит" + "&random_id=" + random_id + "&peer_id=" + users[i] + "&access_token=" + Properties.Settings.Default.TokenChatBot + "&v=5.124";
                                        string AnswerSendMessages = Encoding.UTF8.GetString(cl.DownloadData(SendMessages));
                                        check++;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //string err = "";
                        //timer1.Enabled = false;
                        //if (ex.Message == "Ссылка на объект не указывает на экземпляр объекта.")
                        //{
                        //    err = "Не верный id сообщества.";
                        //}
                        //MessageBox.Show(err, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Суда введи access token сообщества")
            {
                textBox1.Text = "";
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if(textBox2.Text == "Суда введи id сообщества")
            {
                textBox2.Text = "";
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.TokenChatBot = textBox1.Text;
            checkBox1.Checked = false;
            timer1.Enabled = true;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.id_Groups = textBox2.Text;
            checkBox1.Checked = false;
            timer1.Enabled = true;
        }
    }
}
