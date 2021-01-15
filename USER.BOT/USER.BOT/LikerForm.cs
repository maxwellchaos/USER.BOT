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
using Microsoft.Win32;

namespace USER.BOT
{
    public partial class LikerForm : Form
    {
        //string
        public static string LikedIDs;
        public static string access_token;
        public static string users_id;
        public static string UserId;
        public static string captcha_get;
        static string sComboboxText;
        static string sTextboxText;
        static string sLabel2Text;
        static string sLabel6Text;
        static string[] TextboxTextLines;
        static string sCaptchaAdress;

        //bool
        static bool buttonIsPressedClean = true;
        static bool bSendCaptcha = false;
        static bool bWaitCaptcha = false;

        //int
        static int CountPostforProgressBar;
        static int LinesCountforProgressbar;
        static int bugs = 0;
        static int likes = 0;
        static int RandomNuber;
        static int CountPost = 0;
        static int LikeTimer = 0;
        static int postCount = 0;
        static int iComboboxText;
        static int TextboxLinesCount = 0;
        static int progresbarMax = 0;

        //another
        static Wallget.Item SavedItem;
        static idGet SavedIg;
        static Thread Ban;
        static CaptchaGet SavedCg;

        public LikerForm()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        public static string GetAnswer(string Request, string AccesToken)
        {
            string ReqForAns = Request + access_token + "&v=5.124";
            WebClient client = new WebClient();
            string Answer = Encoding.UTF8.GetString(client.DownloadData(ReqForAns));
            return Answer;
        }

        static void Thread_Liker()
        {
            buttonIsPressedClean = true;

            likes = 0;

            sLabel2Text = "0/0";

            //Создание рандома *1
            Random Rnd = new Random();

            //Выделение id
            for (int i = 0; i < TextboxLinesCount; i++)
            {
                LikedIDs = LikedIDs + "TextboxText:" + sTextboxText + "\r\n";

                string[] param = TextboxTextLines[i].Split(new[] { "//", "/" }, StringSplitOptions.RemoveEmptyEntries);

                if (param.Length == 0)
                {
                    continue;
                }

                UserId = param[param.Length - 1];
                TextboxTextLines[i] = UserId;

                //Запрос на получение id
                string Request2 = "https://api.vk.com/method/utils.resolveScreenName?screen_name=" + UserId + "&";
                string Answer2 = GetAnswer(Request2, access_token);

                if (Answer2 == "{\"response\":[]}")
                {
                    continue;
                }
                else
                {
                    idGet ig = JsonConvert.DeserializeObject<idGet>(Answer2);

                    //Запрос на получение информации о стене
                    string Request = "https://api.vk.com/method/wall.get?count=100&" + "owner_id=" + ig.response.object_id + "&";
                    string Answer = GetAnswer(Request, access_token);

                    Wallget wg = JsonConvert.DeserializeObject<Wallget>(Answer);

                    int Offset = 0;

                    Offset += wg.response.items.Count;

                    if (sComboboxText == "Все")
                    {
                        CountPost = wg.response.items.Count;
                    }
                    else
                    {
                        CountPost = iComboboxText;
                    }

                    if (CountPost > wg.response.items.Count)
                    {
                        CountPost = wg.response.items.Count;
                    }

                    progresbarMax = CountPost;//Установить максимум прогрессбара

                    postCount = 0;//Сколько постов отлайкано

                    CountPostforProgressBar = CountPost;

                    LinesCountforProgressbar = 0;

                    LikeTimer = CountPost * 4 * (TextboxLinesCount - i);

                    while (Offset <= wg.response.count)
                    {
                        //*1
                        RandomNuber = Rnd.Next(2000, 4000);

                        //Повторный запрос на получение информации о стене 
                        Request = "https://api.vk.com/method/wall.get?count=100&Offset=" + Offset.ToString() + "&owner_id=" +
                            ig.response.object_id + "&";

                        Answer = GetAnswer(Request, access_token);

                        wg = JsonConvert.DeserializeObject<Wallget>(Answer);
                        //LikedIDs = LikedIDs + "Owner: " + ig.response.object_id.ToString() + " Count:" + wg.response.items.Count.ToString()+ "\r\n";
                        Offset += wg.response.items.Count;

                        //Массовый лайкинг
                        foreach (Wallget.Item item in wg.response.items)
                        {
                            LikedIDs = LikedIDs + "Count post: " + CountPost.ToString() + " " + iComboboxText.ToString() + "\r\n";

                            if (CountPost <= 0)
                            {
                                break;
                            }

                            //Cooldown
                            Thread.Sleep(RandomNuber);

                            string Request1 = "https://api.vk.com/method/likes.add?type=post&item_id=" +
                                item.id + "&owner_id=" + ig.response.object_id + "&";
                            string Answer1 = GetAnswer(Request1, access_token);

                            //Получение и отправка капчи
                            if (Answer1.Contains("rror"))
                            {

                                if (Answer1.Contains("aptcha"))
                                {
                                    CaptchaGet cg = JsonConvert.DeserializeObject<CaptchaGet>(Answer1);

                                    bWaitCaptcha = true;
                                    bSendCaptcha = false;
                                    sCaptchaAdress = cg.error.captcha_img;

                                    SavedIg = ig;
                                    SavedItem = item;
                                    SavedCg = cg;

                                    while (bWaitCaptcha == true)
                                    {
                                        Thread.Sleep(1000);
                                    }
                                }
                                else
                                {
                                    bugs++;
                                }
                            }
                            else
                            {
                                likes++;
                            }

                            sLabel2Text = bugs.ToString() + "/" + likes.ToString();
                            sLabel6Text = LikeTimer.ToString() + " секунд";

                            //*
                            postCount = postCount + 1;
                            CountPost -= 1;
                            LikeTimer -= 4;
                        }
                    }
                }

                LinesCountforProgressbar -= 1;
            }

            sLabel6Text = "Все посты пролайканы";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            LinesCountforProgressbar = textBox3.Lines.Length;

            progresbarMax = 0;

            postCount = 0;

            timerLike.Enabled = true;

            sComboboxText = comboBox1.Text;

            timerLike_Tick(sender, e);

            if (Ban != null)
            {
                if (Ban.IsAlive)
                {
                    return;
                }
            }

            Ban = new Thread(new ThreadStart(Thread_Liker));
            Ban.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bWaitCaptcha = false;

            if (textBox2.Text == "")
            {

            }
            else
            {
                //Отправка капчи
                string Request5 = "https://api.vk.com/method/likes.add?type=post&item_id=" +
                    SavedItem.id + "&captcha_sid=" + SavedCg.error.captcha_sid + "&captcha_key=" + textBox2.Text + "&owner_id=" +
                    SavedIg.response.object_id + "&";
                string Answer5 = GetAnswer(Request5, access_token);
                textBox2.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = !panel3.Visible;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            while (buttonIsPressedClean == true)
            {
                textBox3.Text = "";
                break;
            }

            buttonIsPressedClean = false;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (button2.Enabled == true)
            {
                textBox2.Text = "";
            }
        }

        private void timerLike_Tick(object sender, EventArgs e)
        {
            button1.Enabled = false;

            if (Ban != null)
            {
                if (!Ban.IsAlive)
                {
                    button1.Enabled = true;
                }
            }

            progressBar1.Maximum = progresbarMax;

            progressBar1.Value = postCount;

            TextboxLinesCount = textBox3.Lines.Length;

            if (sComboboxText == "Все")
            {
                sComboboxText = "Все";
            }
            else
            {
                iComboboxText = Convert.ToInt32(sComboboxText);
            }

            sTextboxText = textBox3.Text;

            label6.Text = sLabel6Text;

            label2.Text = sLabel2Text;

            TextboxTextLines = textBox3.Lines;

            if (bSendCaptcha == false)
            {
                webBrowser1.Navigate(sCaptchaAdress);
                bSendCaptcha = true;
            }

            if (bWaitCaptcha == true)
            {
                button2.Enabled = true;
                bWaitCaptcha = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void TextBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }
    }
}
