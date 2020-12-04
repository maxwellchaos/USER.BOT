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
        public string postID;
        public string commentID;
        public int target = 0;
        public int postCount;
        public int progress;
        public int stage;

        public AnswerForm()
        {
            InitializeComponent();
        }

        private void AnswerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            if (stage == 2)
            {
                if (target == 1)
                {
                    progressBar1.Maximum = progress;
                    progressBar1.Value = progressBar1.Maximum;

                    if (labelOutput.Text != "")
                    {
                        WallGet.Item itemWG = new WallGet.Item();
                        itemWG.id = Convert.ToInt16(postID);
                        Request = "https://api.vk.com/method/wall.getComments?owner_id=327011638&post_id=" + itemWG.id + "&" +
                            access_token + "&v=5.124";
                        cl = new WebClient();
                        Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                        CommentsGet csg = JsonConvert.DeserializeObject<CommentsGet>(Answer);
                        listView2.Items.Clear();

                        labelOutput.Text = "Пожалуйста подождите...";
                        buttonInput.Enabled = false;

                        foreach (CommentsGet.Item itemCG in csg.response.items)
                        {
                            string[] LVitem2 = new string[5];

                            Request = "https://api.vk.com/method/users.get?user_ids=" + itemCG.from_id + "&" +
                                    access_token + "&v=5.124";
                            cl = new WebClient();
                            Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            UsersGet ug = JsonConvert.DeserializeObject<UsersGet>(Answer);

                            Application.DoEvents();

                            LVitem2[0] = itemCG.id.ToString();
                            LVitem2[2] = ug.response[0].first_name;
                            LVitem2[3] = ug.response[0].last_name;
                            LVitem2[4] = itemCG.text;

                            ListViewItem lvi2 = new ListViewItem(LVitem2);
                            listView2.Items.Add(lvi2);

                            string id = itemCG.id.ToString();
                            Request = "https://api.vk.com/method/wall.getComments?owner_id=" + user_id + "&post_id=" + itemWG.id + "&comment_id=" +
                                id + "&" + access_token + "&v=5.124";
                            cl = new WebClient();
                            Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                            CommentsGet2 csg2 = JsonConvert.DeserializeObject<CommentsGet2>(Answer);

                            Thread.Sleep(500);

                            progressBar1.Value--;

                            foreach (CommentsGet2.Item itemCsG2 in csg2.response.items)
                            {
                                Request = "https://api.vk.com/method/users.get?user_ids=" + itemCsG2.from_id + "&" +
                                    access_token + "&v=5.124";
                                cl = new WebClient();
                                Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                                ug = JsonConvert.DeserializeObject<UsersGet>(Answer);

                                Application.DoEvents();

                                LVitem2[0] = itemCsG2.id.ToString();
                                LVitem2[1] = id.ToString();
                                LVitem2[2] = ug.response[0].first_name;
                                LVitem2[3] = ug.response[0].last_name;
                                LVitem2[4] = itemCsG2.text;

                                lvi2 = new ListViewItem(LVitem2);
                                listView2.Items.Add(lvi2);

                                Thread.Sleep(500);

                                progressBar1.Value--;
                            }
                        }
                    }
                    labelOutput.Text = "Выберите комментарий, ответ на который хотите отправить" + "\r\n" +
                        "Для этого нажмите на ID соответствующего комментария" + "\r\n" + "Либо выберите другую запись";
                    buttonInput.Enabled = true;
                }
                else
                {
                    if (textBoxInput.Text != "")
                    {
                        Request = "https://api.vk.com/method/wall.createComment?owner_id=327011638&post_id=" + postID + "&message=" +
                            textBoxInput.Text + "&reply_to_comment=" + commentID + "&" + access_token + "&v=5.124";
                        cl = new WebClient();
                        Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                        labelOutput.Text = "Сообщение отправлено";
                        textBoxInput.Text = "";
                    }
                    else
                    {
                        labelOutput.Text = "Введите сообщение";
                    }
                }
            }

            if (stage == 0)
            {
                stage = 1;
                Request = "https://api.vk.com/method/wall.get?" +
                access_token + "&v=5.124";
                cl = new WebClient();
                Answer = Encoding.UTF8.GetString(cl.DownloadData(Request));
                wg = JsonConvert.DeserializeObject<WallGet>(Answer);

                labelOutput.Text = "Пожалуйста подождите...";
                buttonInput.Enabled = false;

                foreach (WallGet.Item itemWG in wg.response.items)
                {
                    if (itemWG.comments.count > 0)
                    {
                        progress++;
                    }
                }
                progressBar1.Maximum = progress;
                progressBar1.Value = progressBar1.Maximum;

                foreach (WallGet.Item itemWG in wg.response.items)
                {
                    Application.DoEvents();
                    Thread.Sleep(500);

                    if (itemWG.comments.count > 0)
                    {
                        string[] LVitem = new string[4];
                        LVitem[0] = itemWG.id.ToString();
                        LVitem[1] = itemWG.comments.count.ToString();
                        LVitem[2] = itemWG.text;
                        if (itemWG.attachments != null)
                        {
                            foreach (WallGet.Attachment attachmentsWG in itemWG.attachments)
                            {
                                if (attachmentsWG.type == "photo")
                                {
                                    LVitem[3] = itemWG.attachments[0].photo.sizes[0].url;
                                }
                            }
                        }

                        ListViewItem lvi = new ListViewItem(LVitem);
                        listView1.Items.Add(lvi);
                        progressBar1.Value--;
                    }
                }
                labelOutput.Text = "Выберите запись, комментарии к которой хотите увидеть, " + "\r\n" +
                    "нажав на соответствующее ID, и нажмите на кнопку" + "\r\n" + "для вывода";
                stage = 2;
                buttonInput.Enabled = true;
                buttonInput.Text = "Вывод";
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                labelOutput.Text = "Выбрана запись с ID: " + listView1.SelectedItems[0].Text + "\r\n" +
                    "Нажмите кнопку для вывода комментариев";
                postID = listView1.SelectedItems[0].Text;

                progress = Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text);
                
                target = 1;

                if (listView1.SelectedItems[0].SubItems[3].Text != "")
                {
                    pictureBox1.ImageLocation = listView1.SelectedItems[0].SubItems[3].Text;
                }
                else
                {
                    pictureBox1.Image = null;
                }

                buttonInput.Text = "Вывод";
                textBoxInput.Enabled = false;
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                labelOutput.Text = "Выбран комментарий с ID: " + listView2.SelectedItems[0].Text + "\r\n" + 
                    "Введите текст ответа и нажмите кнопку для отправки";
                commentID = listView2.SelectedItems[0].Text;
                target = 2;

                textBoxInput.Enabled = true;
            }
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            buttonInput.Text = "Отправить";
        }

        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            //listView1.Columns[e.ColumnIndex].Width = 100;
        }

        private void listView1_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
        }
    }
}
