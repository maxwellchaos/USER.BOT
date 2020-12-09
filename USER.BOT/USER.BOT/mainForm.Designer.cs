namespace USER.BOT
{
    partial class mainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.buttonSelebrate = new System.Windows.Forms.Button();
            this.buttonGetPopularPost = new System.Windows.Forms.Button();
            this.buttonTextBot = new System.Windows.Forms.Button();
            this.buttonLiking = new System.Windows.Forms.Button();
            this.pictureBoxAvatar = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelFamily = new System.Windows.Forms.Label();
            this.buttonMassComment = new System.Windows.Forms.Button();
            this.buttonFindComments = new System.Windows.Forms.Button();
            this.buttonGDZ = new System.Windows.Forms.Button();
            this.Ban_friends = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(236, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(290, 131);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // buttonSelebrate
            // 
            this.buttonSelebrate.Location = new System.Drawing.Point(655, 70);
            this.buttonSelebrate.Name = "buttonSelebrate";
            this.buttonSelebrate.Size = new System.Drawing.Size(256, 22);
            this.buttonSelebrate.TabIndex = 12;
            this.buttonSelebrate.Text = "Поздравление с праздником";
            this.buttonSelebrate.UseVisualStyleBackColor = true;
            this.buttonSelebrate.Click += new System.EventHandler(this.ButtonSelebrate_Click);
            // 
            // buttonGetPopularPost
            // 
            this.buttonGetPopularPost.Location = new System.Drawing.Point(655, 296);
            this.buttonGetPopularPost.Name = "buttonGetPopularPost";
            this.buttonGetPopularPost.Size = new System.Drawing.Size(258, 24);
            this.buttonGetPopularPost.TabIndex = 11;
            this.buttonGetPopularPost.Text = "Самый популярный пост";
            this.buttonGetPopularPost.UseVisualStyleBackColor = true;
            this.buttonGetPopularPost.Click += new System.EventHandler(this.buttonGetPopularPost_Click);
            // 
            // buttonTextBot
            // 
            this.buttonTextBot.Location = new System.Drawing.Point(655, 41);
            this.buttonTextBot.Name = "buttonTextBot";
            this.buttonTextBot.Size = new System.Drawing.Size(256, 23);
            this.buttonTextBot.TabIndex = 10;
            this.buttonTextBot.Text = "Бот автоответчик для группы";
            this.buttonTextBot.UseVisualStyleBackColor = true;
            // 
            // buttonLiking
            // 
            this.buttonLiking.Location = new System.Drawing.Point(655, 12);
            this.buttonLiking.Name = "buttonLiking";
            this.buttonLiking.Size = new System.Drawing.Size(256, 23);
            this.buttonLiking.TabIndex = 9;
            this.buttonLiking.Text = "Массовый лайкинг";
            this.buttonLiking.UseVisualStyleBackColor = true;
            // 
            // pictureBoxAvatar
            // 
            this.pictureBoxAvatar.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxAvatar.Name = "pictureBoxAvatar";
            this.pictureBoxAvatar.Size = new System.Drawing.Size(110, 108);
            this.pictureBoxAvatar.TabIndex = 15;
            this.pictureBoxAvatar.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(128, 51);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(57, 13);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "labelName";
            // 
            // labelFamily
            // 
            this.labelFamily.AutoSize = true;
            this.labelFamily.Location = new System.Drawing.Point(128, 22);
            this.labelFamily.Name = "labelFamily";
            this.labelFamily.Size = new System.Drawing.Size(58, 13);
            this.labelFamily.TabIndex = 13;
            this.labelFamily.Text = "labelFamily";
            // 
            // buttonMassComment
            // 
            this.buttonMassComment.Location = new System.Drawing.Point(655, 98);
            this.buttonMassComment.Name = "buttonMassComment";
            this.buttonMassComment.Size = new System.Drawing.Size(256, 22);
            this.buttonMassComment.TabIndex = 16;
            this.buttonMassComment.Text = "Массовый комментинг";
            this.buttonMassComment.UseVisualStyleBackColor = true;
            // 
            // buttonFindComments
            // 
            this.buttonFindComments.Location = new System.Drawing.Point(655, 126);
            this.buttonFindComments.Name = "buttonFindComments";
            this.buttonFindComments.Size = new System.Drawing.Size(256, 22);
            this.buttonFindComments.TabIndex = 17;
            this.buttonFindComments.Text = "Поиск неотвеченных комментариев";
            this.buttonFindComments.UseVisualStyleBackColor = true;
            // 
            // buttonGDZ
            // 
            this.buttonGDZ.Location = new System.Drawing.Point(655, 154);
            this.buttonGDZ.Name = "buttonGDZ";
            this.buttonGDZ.Size = new System.Drawing.Size(256, 22);
            this.buttonGDZ.TabIndex = 18;
            this.buttonGDZ.Text = "ГДЗ бот";
            this.buttonGDZ.UseVisualStyleBackColor = true;
            // 
            // Ban_friends
            // 
            this.Ban_friends.Location = new System.Drawing.Point(655, 182);
            this.Ban_friends.Name = "Ban_friends";
            this.Ban_friends.Size = new System.Drawing.Size(256, 22);
            this.Ban_friends.TabIndex = 19;
            this.Ban_friends.Text = "бан удалённых друзей";
            this.Ban_friends.UseVisualStyleBackColor = true;
            this.Ban_friends.Click += new System.EventHandler(this.Ban_friends_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 371);
            this.listView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(10, 10);
            this.listView1.TabIndex = 20;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 159;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 124;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(655, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Подожди...";
            this.label1.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(658, 249);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(123, 23);
            this.progressBar1.TabIndex = 22;
            this.progressBar1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(655, 326);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(146, 17);
            this.checkBox1.TabIndex = 23;
            this.checkBox1.Text = "Чат-игра бот(вкл\\выкл)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checkBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox1_MouseDown);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(655, 353);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(243, 20);
            this.textBox1.TabIndex = 24;
            this.textBox1.Text = "Суда введи access token сообщества";
            this.textBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 380);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Ban_friends);
            this.Controls.Add(this.buttonGDZ);
            this.Controls.Add(this.buttonFindComments);
            this.Controls.Add(this.buttonMassComment);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.pictureBoxAvatar);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelFamily);
            this.Controls.Add(this.buttonSelebrate);
            this.Controls.Add(this.buttonGetPopularPost);
            this.Controls.Add(this.buttonTextBot);
            this.Controls.Add(this.buttonLiking);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button buttonSelebrate;
        private System.Windows.Forms.Button buttonGetPopularPost;
        private System.Windows.Forms.Button buttonTextBot;
        private System.Windows.Forms.Button buttonLiking;
        private System.Windows.Forms.PictureBox pictureBoxAvatar;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelFamily;
        private System.Windows.Forms.Button buttonMassComment;
        private System.Windows.Forms.Button buttonFindComments;
        private System.Windows.Forms.Button buttonGDZ;
        private System.Windows.Forms.Button Ban_friends;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

