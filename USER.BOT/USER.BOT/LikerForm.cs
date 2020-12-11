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
        static string access_token;
        static string users_id;
        static string UserId;
        static string captcha_get;

        static Wallget.Item SavedItem;
        static idGet SavedIg;
        static CaptchaGet SavedCg;

        static bool ButtonIsPressed = true;
        static bool buttonIsPressedClean = true;

        static Thread Ban;

        static int bugs = 0;
        static int likes = 0;
        static int RandomNuber;
        static int CountPost = 0;
        static int cPost;
        static int AllPostCount = 0;
        static int LikeTimer = 0;

        public LikerForm()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        public string GetAnswer(string Request, string AccesToken)
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

            progressBar1.Maximum = AllPostCount;

            label2.Text = "0/0";

            //Создание рандома *1
            Random Rnd = new Random();

            //Выделение id
            for (int i = 0; i < textBox3.Lines.Length; i++)
            {
                if (textBox3.Text.Contains("/"))
                {
                    string[] param = textBox3.Lines[i].Split(new[] { "//", "/" }, StringSplitOptions.RemoveEmptyEntries);
                    UserId = param[param.Length - 1];
                    textBox3.Lines[i] = UserId;

                    //Запрос на получение id
                    string Request2 = "https://api.vk.com/method/utils.resolveScreenName?screen_name=" + UserId + "&";
                    string Answer2 = GetAnswer(Request2, access_token);

                    if (Answer2 == "{\"response\":[]}")
                    {
                        break;
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

                        //Прогресс на прогрессбар*
                        int postCount = 0;
                        progressBar1.Minimum = 0;

                        if (comboBox1.Text == "Все")
                        {
                            CountPost = wg.response.count;
                        }
                        else
                        {
                            CountPost = Convert.ToInt32(comboBox1.Text);
                        }

                        if (CountPost > wg.response.count)
                        {
                            CountPost = wg.response.count;
                            comboBox1.Text = wg.response.count.ToString();
                        }

                        cPost = CountPost;

                        LikeTimer = CountPost * 4 * (textBox3.Lines.Length - i);

                        while (Offset <= wg.response.count)
                        {
                            //*1
                            RandomNuber = Rnd.Next(200, 400);

                            //Повторный запрос на получение информации о стене 
                            Request = "https://api.vk.com/method/wall.get?count=100&Offset=" + Offset.ToString() + "&owner_id=" +
                                ig.response.object_id + "&";
                            Answer = GetAnswer(Request, access_token);

                            wg = JsonConvert.DeserializeObject<Wallget>(Answer);
                            Offset += wg.response.items.Count;

                            //*
                            progressBar1.Maximum = cPost;

                            //Массовый лайкинг
                            foreach (Wallget.Item item in wg.response.items)
                            {

                                if (CountPost <= 0)
                                {
                                    break;
                                }
                                //Cooldown
                                for (int a = 0; a < 10; a++)
                                {
                                    Application.DoEvents();
                                    Thread.Sleep(RandomNuber);
                                }

                                string Request1 = "https://api.vk.com/method/likes.add?type=post&item_id=" +
                                    item.id + "&owner_id=" + ig.response.object_id + "&";
                                string Answer1 = GetAnswer(Request1, access_token);

                                //Получение и отправка капчи
                                if (Answer1.Contains("rror"))
                                {

                                    if (Answer1.Contains("aptcha"))
                                    {
                                        ButtonIsPressed = false;

                                        string Answer4 = "{ \"error\":{ \"error_code\":14,\"error_msg\":\"Captcha needed\",\"request_params\"" +
                                            ":[{ \"key\":\"type\",\"value\":\"post\"},{ \"key\":\"item_id\",\"value\":\"185\"}," +
                                            "{ \"key\":\"owner_id\",\"value\":\"422303825\"},{ \"key\":\"v\",\"value\":\"5.124\"}," +
                                            "{ \"key\":\"method\",\"value\":\"likes.add\"},{ \"key\":\"oauth\",\"value\":\"1\"}]," +
                                            "\"captcha_sid\":\"337894471349\",\"captcha_img\":" +
                                            "\"https://api.vk.com/captcha.php?sid=337894471349&s=1\"}}";
                                        CaptchaGet cg = JsonConvert.DeserializeObject<CaptchaGet>(Answer4);
                                        webBrowser1.Navigate(cg.error.captcha_img);

                                        button2.Enabled = true;

                                        SavedIg = ig;
                                        SavedItem = item;
                                        SavedCg = cg;

                                        while (ButtonIsPressed == false)
                                        {
                                            Application.DoEvents();
                                            Thread.Sleep(10);
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
                                    LikeTimer -= 4;
                                }

                                label2.Text = bugs.ToString() + "/" + likes.ToString();
                                label6.Text = LikeTimer.ToString() + " секунд";

                                //*
                                postCount = postCount + 1;
                                progressBar1.Value = postCount;
                                CountPost -= 1;
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            label6.Text = "Все посты пролайканы";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Ban = new Thread(new ThreadStart(Thread_Liker));
            Ban.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonIsPressed = true;

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

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
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
        }
    }
}
