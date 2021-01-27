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

namespace USER.BOT
{
    public partial class FormChatBot : Form
    {
        public string access_token;
        public string GroupAccess_token = "access_token=09c5ce6b65692dc6fdb2684201998d347d52d2dcae7dd03dc052e16f3c08c097a6803427496de41502a8e";
        public string user_id;
        public string messages;

        public FormChatBot()
        {
            InitializeComponent();
        }

        private void ButtonChatBot_Click(object sender, EventArgs e)
        {
            //возвращает запрос на сообщения
            string Request = "https://api.vk.com/method/messages.getConversations?fields=bdate&" +
            GroupAccess_token + "&v=5.124";

            WebClient cl = new WebClient();
            //показывает последнее сообщение
            string Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
            messagesGetConversations mgc = JsonConvert.DeserializeObject<messagesGetConversations>(Answer);
            City1.Text = mgc.response.items[0].last_message.text;
            string lastMessage = mgc.response.items[0].last_message.text;
            int len = lastMessage.Length;
            string lastLetter = lastMessage.Remove(0, len - 1);
            label2.Text = lastLetter;
            Random rnd = new Random();
            int result = rnd.Next(10000000);
            string PredLetter = lastMessage.Remove(0, len - 2);
            label3.Text = PredLetter;

            City1.Visible = true;
            Name1.Visible = true;
            Name2.Visible = true;
            label2.Visible = true;

            //порт к папке с городами
            Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
            string[] txtFile = System.IO.File.ReadAllLines(Application.StartupPath+@"\Документ.txt.txt", Encoding.Default);
            foreach (string City in txtFile)
            {

                Application.DoEvents();
                if (lastLetter.ToUpper() == City[0].ToString())
                {
                    Request = "https://api.vk.com/method/messages.send?message=" + City + "&random_id=" + result.ToString() + "&peer_id=471929958&" +
                    GroupAccess_token + "&v=5.124";
                    WebClient cs = new WebClient();
                    Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                }

            }
            Application.DoEvents();
            if (PredLetter.ToUpper() == label3.ToString())
            {
                Request = "https://api.vk.com/method/messages.send?message=" + label3 + "&random_id=" + result.ToString() + "&peer_id=471929958&" +
                GroupAccess_token + "&v=5.124";
                WebClient cs = new WebClient();
                Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonChatBot.Visible = true;
            button1.Visible = false;
        }

        public string GetAnswer(string messages, string access_token)
        {
            string mes = messages + access_token + "&v=5.124";
            WebClient cl = new WebClient();
            string GetAnswer = Encoding.UTF8.GetString(cl.DownloadData(messages));
            return GetAnswer;
        }

        private void FormChatBot_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //возвращает запрос на сообщения
            string Request = "https://api.vk.com/method/messages.getConversations?filter=unread&fields=bdate&" +
            GroupAccess_token + "&v=5.124";

            WebClient cl = new WebClient();
            //показывает последнее сообщение
            string Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
            messagesGetConversations mgc = JsonConvert.DeserializeObject<messagesGetConversations>(Answer);
            if (mgc.response.count == 0)
            {
                return;
            }
            City1.Text = mgc.response.items[0].last_message.text;
            string lastMessage = mgc.response.items[0].last_message.text;
            int len = lastMessage.Length;
            string lastLetter = lastMessage.Remove(0, len - 1);
            label2.Text = lastLetter;
            Random rnp = new Random();
            int result = rnp.Next(10000000);
            string PredLetter = lastMessage.Remove(0, len - 2);
            PredLetter = PredLetter.Remove(1, 1);
            label3.Text = PredLetter;

            City1.Visible = true;
            Name1.Visible = true;
            Name2.Visible = true;
            Name3.Visible = true;
            label2.Visible = true;
            label3.Visible = true;

            //порт к папке с городами
            Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
            string str = Application.StartupPath + @"\Документ.txt";
            string[] txtFile = System.IO.File.ReadAllLines(@"C:\Users\Виктор\Desktop\Документ.txt.txt", Encoding.Default);
            Random rnd = new Random();
            foreach (string City in txtFile)
            {

                Application.DoEvents();
                int Number = rnd.Next(10);

                if (lastLetter.ToUpper() == City[0].ToString())
                    if (Number < 5)
                    {
                        Request = "https://api.vk.com/method/messages.send?message=" + City + "&random_id=" + result.ToString() + "&peer_id=471929958&" +
                        GroupAccess_token + "&v=5.124";
                        WebClient cs = new WebClient();
                        Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                    }
            }
            foreach (string City in txtFile)
            {

                Application.DoEvents();
                int Number = rnd.Next(10);

                if (PredLetter.ToUpper() == City[0].ToString())
                    if (Number < 5)
                    {
                        Request = "https://api.vk.com/method/messages.send?message=" + City + "&random_id=" + result.ToString() + "&peer_id=471929958&" +
                        GroupAccess_token + "&v=5.124";
                        WebClient cs = new WebClient();
                        Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                    }
            }
        }
    }
}
