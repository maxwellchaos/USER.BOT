using MethodButton.VK;
using MethodButton.VK.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USER.BOT
{
    public partial class FormMostPopularPost : Form
    {
        public string Token { get; private set; } //= "822a716ca03a0848114ea95fa58e137a378a4b549525b1b67ec2d5d63b32face26945aa8983d24faa317f";

        public FormMostPopularPost(string token)
        {
            InitializeComponent();
            this.Token = token;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            int outVal = -1;
            int.TryParse(rangeTextbox.Text, out outVal);
            if (outVal <= 0)
            {
                rangeTextbox.Text = 10 + "";
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            string domain = linkTextbox.Text.ToLower();
            WallOrder current = (WallOrder)comboBox1.SelectedIndex;

            this.BeginInvoke((Action)(() =>
            {
                var vkApi = new VkApi()
                {
                    AccessToken = this.Token
                };

                vkApi.Auth();

                var a = new WallGetArguments
                {
                    Count = int.Parse(rangeTextbox.Text)
                };

                a.Domain = domain;

                if (IsUrl(domain))
                {
                    if (domain.StartsWith("vk.com/"))
                    {
                        a.Domain = domain.Remove(0, 7);
                    }
                    else
                        a.Domain = domain.Remove(0, 7).Split('/')[1];
                }

                int amount = 0;
                WallPost loaded = null;
                Task.Run(() => loaded = vkApi.Wall.GetMostLikedPost(a, current, out amount));
                while (loaded == null)
                {
                    statusLabel.Text = $"Загружаем посты... ({amount + 100} из {a.Count})";
                    Application.DoEvents();
                }

                statusLabel.Text = "Все!";
                Process.Start($"https://vk.com/public{loaded.OwnerId.ToString().Remove(0, 1)}?w=wall{loaded.OwnerId}_{loaded.Id}");
            }));
        }

        private bool IsUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }

}
