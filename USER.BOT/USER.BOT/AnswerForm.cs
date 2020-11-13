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

namespace USER.BOT
{
    public partial class AnswerForm : Form
    {
        public string access_token;
        public string user_id;
        public string Request;
        public WebClient cl;
        public string Answer;
        public WallGet wg;
        public AnswerForm()
        {
            InitializeComponent();
        }

        private void AnswerForm_Load(object sender, EventArgs e)
        {
            Request = "https://api.vk.com/method/wall.get?" +
                access_token + "&v=5.124";
            cl = new WebClient();
            Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
            wg = JsonConvert.DeserializeObject<WallGet>(Answer);
           
            foreach (WallGet.Item item in wg.response.items)
            {
                Application.DoEvents();
                Thread.Sleep(300);

                if (item.comments.count > 0)
                {
                    //item.attachments[0].photo.
                    
                    string[] LVitem = new string[2];
                    LVitem[0] = item.id.ToString();
                    LVitem[1] = item.text;

                    ListViewItem lvi = new ListViewItem(LVitem);
                    listView1.Items.Add(lvi);
                }
            }
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            if (textBoxOutput.Text != "")
            {
                WallGet.Item itemWG = new WallGet.Item();
                itemWG.id = Convert.ToInt16(textBoxOutput.Text);
                Request = "https://api.vk.com/method/wall.getComments?owner_id=327011638&post_id=" + itemWG.id + "&" +
                    access_token + "&v=5.124";
                cl = new WebClient();
                Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                CommentsGet csg = JsonConvert.DeserializeObject<CommentsGet>(Answer);
                listView2.Items.Clear();

                foreach (CommentsGet.Item itemCG in csg.response.items)
                {
                    string id = itemCG.id.ToString();
                    Request = "https://api.vk.com/method/wall.getComment?owner_id=327011638&comment_id=" + id + "&" +
                        access_token + "&v=5.124";
                    cl = new WebClient();
                    Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                    CommentGet cg = JsonConvert.DeserializeObject<CommentGet>(Answer);

                    if (itemCG.from_id != 327011638)
                    {

                        Request = "https://api.vk.com/method/friends.get?fields=first_name" + "&" +
                        access_token + "&v=5.124";
                        cl = new WebClient();
                        Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                        FriendsGet fg = JsonConvert.DeserializeObject<FriendsGet>(Answer);

                        foreach (FriendsGet.Item itemFG in fg.response.items)
                        {
                            if (itemFG.id == itemCG.from_id)
                            {
                                string[] LVitem2 = new string[5];
                                LVitem2[0] = itemCG.parents_stack.ToString();
                                LVitem2[1] = itemCG.from_id.ToString();
                                LVitem2[2] = itemFG.first_name;
                                LVitem2[3] = itemFG.last_name;
                                LVitem2[4] = itemCG.text;

                                ListViewItem lvi2 = new ListViewItem(LVitem2);
                                listView2.Items.Add(lvi2);
                            }
                        }
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            { 
                textBoxOutput.Text = listView1.SelectedItems[0].Text;//"Выбрана запись с ID: " + 
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                textBoxOutput.Text = listView2.SelectedItems[0].Text;//"Выбран комментарий с ID: " + 
            }
        }
    }
}
