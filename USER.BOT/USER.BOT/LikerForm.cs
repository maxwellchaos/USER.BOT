﻿using System;
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

        public string access_token;
        public string users_id;
        string UserId;
        public string captcha_get;

        bool ButtonIsPressed = true;

        int bugs = 0;
        int likes = 0;

        public LikerForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {

            }
            if (textBox1.Text == "Вставьте ссылку или id")
            {

            }
            else
            {
                //Выделение id
                string[] param = textBox1.Text.Split(new[] { "//", "/" }, StringSplitOptions.RemoveEmptyEntries);
                UserId = param[param.Length - 1];
                textBox1.Text = UserId;

                //Запрос на получение id
                string Request2 = "https://api.vk.com/method/utils.resolveScreenName?screen_name=" + UserId + "&" + access_token + "&v=5.124";
                WebClient client2 = new WebClient();
                string Answer2 = Encoding.UTF8.GetString(client2.DownloadData(Request2));

                idGet ig = JsonConvert.DeserializeObject<idGet>(Answer2);

                //Запрос на получение информации о стене
                string Request = "https://api.vk.com/method/wall.get?count=100&" + "owner_id=" + ig.response.object_id + "&" + access_token + "&v=5.124";
                WebClient client = new WebClient();
                string Answer = Encoding.UTF8.GetString(client.DownloadData(Request));

                Wallget wg = JsonConvert.DeserializeObject<Wallget>(Answer);
                int Offset = 0;

                Offset += wg.response.items.Count;

                //Прогресс на прогрессбар*
                int postCount = 0;
                progressBar1.Minimum = 0;

                while (Offset <= wg.response.count)
                {
                    //Повторный запрос на получение информации о стене 
                    Request = "https://api.vk.com/method/wall.get?count=100&Offset=" + Offset.ToString() + "&owner_id=" + ig.response.object_id + "&" + access_token + "&v=5.124";
                    client = new WebClient();
                    Answer = Encoding.UTF8.GetString(client.DownloadData(Request));

                    wg = JsonConvert.DeserializeObject<Wallget>(Answer);
                    Offset += wg.response.items.Count;

                    //*
                    progressBar1.Maximum = wg.response.count;

                    //Массовый лайкинг
                    foreach (Wallget.Item item in wg.response.items)
                    {
                        //*
                        postCount = postCount + 1;
                        progressBar1.Value = postCount;

                        //Cooldown
                        for (int i = 0; i < 10; i++)
                        {
                            Application.DoEvents();
                            Thread.Sleep(100);
                        }

                        string Request1 = "https://api.vk.com/method/likes.add?type=post&item_id=" + item.id + "&owner_id=" + ig.response.object_id + "&" + access_token + "&v=5.124";
                        WebClient client1 = new WebClient();
                        string Answer1 = Encoding.UTF8.GetString(client1.DownloadData(Request1));

                        //Получение и отправка капчи
                        if (Answer1.Contains("rror"))
                        {

                            if (Answer1.Contains("aptcha"))
                            {
                                ButtonIsPressed = false;

                                string Answer4 = "{ \"error\":{ \"error_code\":14,\"error_msg\":\"Captcha needed\",\"request_params\":[{ \"key\":\"type\",\"value\":\"post\"},{ \"key\":\"item_id\",\"value\":\"185\"},{ \"key\":\"owner_id\",\"value\":\"422303825\"},{ \"key\":\"v\",\"value\":\"5.124\"},{ \"key\":\"method\",\"value\":\"likes.add\"},{ \"key\":\"oauth\",\"value\":\"1\"}],\"captcha_sid\":\"337894471349\",\"captcha_img\":\"https://api.vk.com/captcha.php?sid=337894471349&s=1\"}}";
                                CaptchaGet cg = JsonConvert.DeserializeObject<CaptchaGet>(Answer4);
                                webBrowser1.Navigate(cg.error.captcha_img);

                                string Request5 = "https://api.vk.com/method/likes.add?type=post&item_id=" + item.id + "&captcha_sid" + cg.error.captcha_sid + "&captcha_key" + textBox2.Text + "&owner_id=" + ig.response.object_id + "&" + access_token + "&v=5.124";
                                WebClient client5 = new WebClient();
                                string Answer5 = Encoding.UTF8.GetString(client5.DownloadData(Request5));

                                while (ButtonIsPressed == false)
                                {
                                    Application.DoEvents();
                                    Thread.Sleep(10);
                                }
                            }
                            else
                            {
                                bugs = bugs + 1;
                            }
                        }
                        else
                        {
                            likes++;
                        }

                        label2.Text = bugs.ToString() + "/" + likes.ToString() + "/" + wg.response.count.ToString();

                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            ButtonIsPressed = true;
        }
    }

}