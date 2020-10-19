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
    public partial class mainForm : Form
    {
        string AccessToken;
        string UserID;

        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.BringToFront();
            webBrowser1.Dock = DockStyle.Fill;

            webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=7614304"+
                "&display=page&redirect_uri=https://oauth.vk.com/blank.html&"+
                "scope=friends+groups+wall+photo&"+
                "response_type=token&v=5.124&state=123456");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.ToString().Contains("access_token"))
            {
                string[] param = webBrowser1.Url.ToString().Split(new[] { "#", "&" }, StringSplitOptions.RemoveEmptyEntries);
                AccessToken = param[1];

                string Request = "https://api.vk.com/method/account.getProfileInfo?" +
                AccessToken + "&v=5.124";

                WebClient cl = new WebClient();
               
                string Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));

                GetProfileInfo gpi = JsonConvert.DeserializeObject<GetProfileInfo>(Answer);
                labelFamily.Text = gpi.response.last_name;
                labelName.Text = gpi.response.first_name;
                UserID = gpi.response.id.ToString();

                Request = "https://api.vk.com/method/users.get?fields=photo_100&" +
                AccessToken + "&v=5.124";


                Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));

                UsersGet ug = JsonConvert.DeserializeObject<UsersGet>(Answer);
                pictureBoxAvatar.ImageLocation = ug.response[0].photo_100;
                UserID = ug.response[0].id.ToString();
                webBrowser1.Hide();
            }
        }

        private void buttonGetPopularPost_Click(object sender, EventArgs e)
        {
            FormMostPopularPost frm = new FormMostPopularPost();
            frm.access_token = this.AccessToken;
            frm.user_id = UserID;
            frm.Show();
        }

        private void ButtonLiking_Click(object sender, EventArgs e)
        {
            LikerForm form = new LikerForm();
            form.access_token = AccessToken;
            form.users_id = UserID;
            form.ShowDialog();
        }
    }
}
