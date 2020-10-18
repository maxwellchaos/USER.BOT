using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
namespace USER.BOT
{
    public partial class formgdz : Form
    {
        public string access_token;
        public string user_id;


        public formgdz()
        {
            InitializeComponent();
        }



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {




        }




        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=7617031&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends+groups&response_type=token&v=5.124&state=123456");

        //}



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void formgdz_Load(object sender, EventArgs e)
        {
          
            
             
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //00(предмет)_00(класс)_00(автор)_0000(упражнение)
            Random rnd = new Random();
            string reqeuest2 = "https://api.vk.com/method/messages.getConversations?access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
            WebClient cl = new WebClient();
            string answer2 = Encoding.UTF8.GetString(cl.DownloadData(reqeuest2));
            messagesgetConversations mgc = JsonConvert.DeserializeObject<messagesgetConversations>(answer2);
            labelgrope.Text = mgc.response.items[0].last_message.text;
            string reqeuest4 = "https://api.vk.com/method/messages.getConversations?filter=unanswered&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
            WebClient wc2 = new WebClient();
            string answer4 = Encoding.UTF8.GetString(wc2.DownloadData(reqeuest4));
            messagesgetConversations mgc1 = JsonConvert.DeserializeObject<messagesgetConversations>(answer4);
            foreach (messagesgetConversations.Item item in mgc1.response.items)
            {
                WebClient wc = new WebClient();
                int random_id = rnd.Next();
                // messagesSend ms = JsonConvert.DeserializeObject<messagesSend>(answer3);
                if (item.last_message.text == "1")
                {
                    random_id = rnd.Next();
                    string reqeuest6 = "https://api.vk.com/method/messages.send?message=https://gdz.ru/class-6/matematika/a-g-merzlyak/&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
                    string answer6 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest6));
                }
                if (item.last_message.text == "2")
                {
                    random_id = rnd.Next();
                    string reqeuest6 = "https://api.vk.com/method/messages.send?attachment=doc-199265164_571585875&message=держии ответ/&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
                    string answer6 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest6));
                }
                if (item.last_message.text == "старт")
                {
                    string reqeuest3 = "https://api.vk.com/method/messages.send?message=Я знаю зачем ты пришёл, я всё устрою ты главное следуй инструкциям которые я тебе дам пример:00(предмет)_00(класс)_00(автор)_0000(упражнение)&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
                    random_id = rnd.Next();
                    string answer3 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest3));
                }
                if (item.last_message.text == "Старт")
                {
                    string reqeuest8 = "https://api.vk.com/method/messages.send?message=Я знаю зачем ты пришёл, я всё устрою ты главное следуй инструкциям которые я тебе дам пример: 00(предмет)_00(класс)_00(автор)_0000(упражнение)&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
                    random_id = rnd.Next();
                    string answer8 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest8));
                }
                if (item.last_message.text != "1" && item.last_message.text != "2" && item.last_message.text != "старт" && item.last_message.text != "Старт")
                {
                    random_id = rnd.Next();
                    string reqeuest7 = "https://api.vk.com/method/messages.send?message=непонял вопроса&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
                    string answer7 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest7));
                    random_id = rnd.Next();

                    string reqeuest5 = "https://api.vk.com/method/messages.send?message=Выбири предмет 1=математика 2=русский&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";

                    string answer5 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest5));
                }

            }
        }
    }
}