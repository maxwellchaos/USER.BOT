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
            this.buttonChatBot = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(315, 15);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(27, 25);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(387, 161);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // buttonSelebrate
            // 
            this.buttonSelebrate.Location = new System.Drawing.Point(29, 97);
            this.buttonSelebrate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSelebrate.Name = "buttonSelebrate";
            this.buttonSelebrate.Size = new System.Drawing.Size(341, 27);
            this.buttonSelebrate.TabIndex = 12;
            this.buttonSelebrate.Text = "Поздравление с праздником";
            this.buttonSelebrate.UseVisualStyleBackColor = true;
            this.buttonSelebrate.Click += new System.EventHandler(this.ButtonSelebrate_Click);
            // 
            // buttonGetPopularPost
            // 
            this.buttonGetPopularPost.Location = new System.Drawing.Point(29, 384);
            this.buttonGetPopularPost.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGetPopularPost.Name = "buttonGetPopularPost";
            this.buttonGetPopularPost.Size = new System.Drawing.Size(344, 30);
            this.buttonGetPopularPost.TabIndex = 11;
            this.buttonGetPopularPost.Text = "Самый популярный пост";
            this.buttonGetPopularPost.UseVisualStyleBackColor = true;
            this.buttonGetPopularPost.Click += new System.EventHandler(this.buttonGetPopularPost_Click);
            // 
            // buttonTextBot
            // 
            this.buttonTextBot.Location = new System.Drawing.Point(29, 62);
            this.buttonTextBot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonTextBot.Name = "buttonTextBot";
            this.buttonTextBot.Size = new System.Drawing.Size(341, 28);
            this.buttonTextBot.TabIndex = 10;
            this.buttonTextBot.Text = "Бот автоответчик для группы";
            this.buttonTextBot.UseVisualStyleBackColor = true;
            // 
            // buttonLiking
            // 
            this.buttonLiking.Location = new System.Drawing.Point(29, 26);
            this.buttonLiking.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonLiking.Name = "buttonLiking";
            this.buttonLiking.Size = new System.Drawing.Size(341, 28);
            this.buttonLiking.TabIndex = 9;
            this.buttonLiking.Text = "Массовый лайкинг";
            this.buttonLiking.UseVisualStyleBackColor = true;
            // 
            // pictureBoxAvatar
            // 
            this.pictureBoxAvatar.Location = new System.Drawing.Point(16, 15);
            this.pictureBoxAvatar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxAvatar.Name = "pictureBoxAvatar";
            this.pictureBoxAvatar.Size = new System.Drawing.Size(147, 133);
            this.pictureBoxAvatar.TabIndex = 15;
            this.pictureBoxAvatar.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(171, 63);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(75, 17);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "labelName";
            // 
            // labelFamily
            // 
            this.labelFamily.AutoSize = true;
            this.labelFamily.Location = new System.Drawing.Point(171, 27);
            this.labelFamily.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFamily.Name = "labelFamily";
            this.labelFamily.Size = new System.Drawing.Size(78, 17);
            this.labelFamily.TabIndex = 13;
            this.labelFamily.Text = "labelFamily";
            // 
            // buttonMassComment
            // 
            this.buttonMassComment.Location = new System.Drawing.Point(29, 132);
            this.buttonMassComment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonMassComment.Name = "buttonMassComment";
            this.buttonMassComment.Size = new System.Drawing.Size(341, 27);
            this.buttonMassComment.TabIndex = 16;
            this.buttonMassComment.Text = "Массовый комментинг";
            this.buttonMassComment.UseVisualStyleBackColor = true;
            // 
            // buttonFindComments
            // 
            this.buttonFindComments.Location = new System.Drawing.Point(29, 166);
            this.buttonFindComments.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonFindComments.Name = "buttonFindComments";
            this.buttonFindComments.Size = new System.Drawing.Size(341, 27);
            this.buttonFindComments.TabIndex = 17;
            this.buttonFindComments.Text = "Поиск неотвеченных комментариев";
            this.buttonFindComments.UseVisualStyleBackColor = true;
            // 
            // buttonGDZ
            // 
            this.buttonGDZ.Location = new System.Drawing.Point(29, 201);
            this.buttonGDZ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGDZ.Name = "buttonGDZ";
            this.buttonGDZ.Size = new System.Drawing.Size(341, 27);
            this.buttonGDZ.TabIndex = 18;
            this.buttonGDZ.Text = "ГДЗ бот";
            this.buttonGDZ.UseVisualStyleBackColor = true;
            // 
            // Ban_friends
            // 
            this.Ban_friends.Location = new System.Drawing.Point(29, 271);
            this.Ban_friends.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Ban_friends.Name = "Ban_friends";
            this.Ban_friends.Size = new System.Drawing.Size(341, 27);
            this.Ban_friends.TabIndex = 19;
            this.Ban_friends.Text = "бан удалённых друзей";
            this.Ban_friends.UseVisualStyleBackColor = true;
            this.Ban_friends.Click += new System.EventHandler(this.Ban_friends_Click);
            // 
            // listView1
            // 
            this.listView1.CausesValidation = false;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 457);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(12, 11);
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
            this.label1.Location = new System.Drawing.Point(25, 302);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Подожди...";
            this.label1.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(29, 348);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(164, 28);
            this.progressBar1.TabIndex = 22;
            this.progressBar1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // buttonChatBot
            // 
            this.buttonChatBot.Location = new System.Drawing.Point(28, 236);
            this.buttonChatBot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonChatBot.Name = "buttonChatBot";
            this.buttonChatBot.Size = new System.Drawing.Size(341, 27);
            this.buttonChatBot.TabIndex = 26;
            this.buttonChatBot.Text = "Монополия (чат-бот)";
            this.buttonChatBot.UseVisualStyleBackColor = true;
            this.buttonChatBot.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonChatBot);
            this.panel1.Controls.Add(this.buttonLiking);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.buttonTextBot);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonGetPopularPost);
            this.panel1.Controls.Add(this.buttonSelebrate);
            this.panel1.Controls.Add(this.Ban_friends);
            this.panel1.Controls.Add(this.buttonMassComment);
            this.panel1.Controls.Add(this.buttonGDZ);
            this.panel1.Controls.Add(this.buttonFindComments);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(815, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 471);
            this.panel1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(224, 251);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(444, 75);
            this.label2.TabIndex = 28;
            this.label2.Text = "Требуется оплатить программу! \r\nПосле оплаты перезапусти программу!\r\nЧтобы связат" +
    "ься с продавцом кликни по мне.\r\n";
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 191);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 17);
            this.label3.TabIndex = 29;
            this.label3.Visible = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 523);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.pictureBoxAvatar);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelFamily);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "mainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Button buttonChatBot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label3;
    }
}

