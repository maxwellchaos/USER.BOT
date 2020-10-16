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
      public  string access_token;
        public string user_id;


        public formgdz()
        {
            InitializeComponent();
        }



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Random rnd = new Random();

            if (webBrowser1.Url.ToString().Contains("access_token"))
            {
                label1.Text = webBrowser1.Url.ToString();
                string[] separator = new string[2];
                separator[0] = "#";
                separator[1] = "&";
                string[] param = webBrowser1.Url.ToString().Split(separator, StringSplitOptions.None);
                textBox1.Text = param[1];
                access_token = param[1];
                label5.Text = access_token;
                string request = "https://api.vk.com/method/account.getProfileInfo?" + param[1] + "&v=5.124";

                WebClient client = new WebClient();
                //string answer = client.DownloadString(request);
                string answer = Encoding.UTF8.GetString(client.DownloadData(request));
                label3.Text = answer;

                getProfiltinfo gpi = JsonConvert.DeserializeObject<getProfiltinfo>(answer);
                user_id = gpi.response.id.ToString();
                //label4.Text = gpi.response.last_name;
                //label5.Text = gpi.response.first_name;
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
                        string reqeuest6 = "https://api.vk.com/method/messages.send?message= https://gdz.ru/class-6/matematika/a-g-merzlyak/&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
                        string answer6 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest6));
                    }
                    if (item.last_message.text == "2")
                    {
                        random_id = rnd.Next();
                        string reqeuest6 = "https://api.vk.com/method/messages.send?message= https://gdz.ru/class-6/russkii_yazik/baranov-2008//&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
                        string answer6 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest6));
                    }
                    if (item.last_message.text != "1" && item.last_message.text != "2")
                    {
                        random_id = rnd.Next();
                        string reqeuest7 = "https://api.vk.com/method/messages.send?message=непонял вопроса&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
                        string answer7 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest7));
                        random_id = rnd.Next();
                        string reqeuest3 = "https://api.vk.com/method/messages.send?message=Я знаю зачем ты пришёл, я всё устрою ты главное следуй инструкциям которые я тебе дам&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
                        random_id = rnd.Next();
                        string reqeuest5 = "https://api.vk.com/method/messages.send?message=Выбири предмет 1=математика 2=русский&user_id=" + item.last_message.from_id.ToString() + "&random_id=" + random_id + "&access_token=e2da676d069c28cfce6428a770c3e3413f85260468038237ff5a07c2a57975602a0bd8828786c116d27b3&v=5.124";
                        string answer3 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest3));
                        string answer5 = Encoding.UTF8.GetString(wc.DownloadData(reqeuest5));
                    }



                }

            }

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
            webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=7617031&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends+groups&response_type=token&v=5.124&state=123456");
        }
    }
}
