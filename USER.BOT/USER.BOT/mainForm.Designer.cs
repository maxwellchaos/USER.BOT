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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(354, 18);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(30, 31);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(435, 202);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // buttonSelebrate
            // 
            this.buttonSelebrate.Location = new System.Drawing.Point(982, 108);
            this.buttonSelebrate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSelebrate.Name = "buttonSelebrate";
            this.buttonSelebrate.Size = new System.Drawing.Size(384, 34);
            this.buttonSelebrate.TabIndex = 12;
            this.buttonSelebrate.Text = "Поздравление с праздником";
            this.buttonSelebrate.UseVisualStyleBackColor = true;
            // 
            // buttonGetPopularPost
            // 
            this.buttonGetPopularPost.Location = new System.Drawing.Point(982, 471);
            this.buttonGetPopularPost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGetPopularPost.Name = "buttonGetPopularPost";
            this.buttonGetPopularPost.Size = new System.Drawing.Size(387, 37);
            this.buttonGetPopularPost.TabIndex = 11;
            this.buttonGetPopularPost.Text = "Самый популярный пост";
            this.buttonGetPopularPost.UseVisualStyleBackColor = true;
            this.buttonGetPopularPost.Click += new System.EventHandler(this.buttonGetPopularPost_Click);
            // 
            // buttonTextBot
            // 
            this.buttonTextBot.Location = new System.Drawing.Point(982, 63);
            this.buttonTextBot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonTextBot.Name = "buttonTextBot";
            this.buttonTextBot.Size = new System.Drawing.Size(384, 35);
            this.buttonTextBot.TabIndex = 10;
            this.buttonTextBot.Text = "Бот автоответчик для группы";
            this.buttonTextBot.UseVisualStyleBackColor = true;
            // 
            // buttonLiking
            // 
            this.buttonLiking.Location = new System.Drawing.Point(982, 18);
            this.buttonLiking.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonLiking.Name = "buttonLiking";
            this.buttonLiking.Size = new System.Drawing.Size(384, 35);
            this.buttonLiking.TabIndex = 9;
            this.buttonLiking.Text = "Массовый лайкинг";
            this.buttonLiking.UseVisualStyleBackColor = true;
            // 
            // pictureBoxAvatar
            // 
            this.pictureBoxAvatar.Location = new System.Drawing.Point(18, 18);
            this.pictureBoxAvatar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxAvatar.Name = "pictureBoxAvatar";
            this.pictureBoxAvatar.Size = new System.Drawing.Size(165, 166);
            this.pictureBoxAvatar.TabIndex = 15;
            this.pictureBoxAvatar.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(192, 78);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(84, 20);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "labelName";
            // 
            // labelFamily
            // 
            this.labelFamily.AutoSize = true;
            this.labelFamily.Location = new System.Drawing.Point(192, 34);
            this.labelFamily.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFamily.Name = "labelFamily";
            this.labelFamily.Size = new System.Drawing.Size(87, 20);
            this.labelFamily.TabIndex = 13;
            this.labelFamily.Text = "labelFamily";
            // 
            // buttonMassComment
            // 
            this.buttonMassComment.Location = new System.Drawing.Point(982, 151);
            this.buttonMassComment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonMassComment.Name = "buttonMassComment";
            this.buttonMassComment.Size = new System.Drawing.Size(384, 34);
            this.buttonMassComment.TabIndex = 16;
            this.buttonMassComment.Text = "Массовый комментинг";
            this.buttonMassComment.UseVisualStyleBackColor = true;
            // 
            // buttonFindComments
            // 
            this.buttonFindComments.Location = new System.Drawing.Point(982, 194);
            this.buttonFindComments.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFindComments.Name = "buttonFindComments";
            this.buttonFindComments.Size = new System.Drawing.Size(384, 34);
            this.buttonFindComments.TabIndex = 17;
            this.buttonFindComments.Text = "Поиск неотвеченных комментариев";
            this.buttonFindComments.UseVisualStyleBackColor = true;
            // 
            // buttonGDZ
            // 
            this.buttonGDZ.Location = new System.Drawing.Point(982, 237);
            this.buttonGDZ.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGDZ.Name = "buttonGDZ";
            this.buttonGDZ.Size = new System.Drawing.Size(384, 34);
            this.buttonGDZ.TabIndex = 18;
            this.buttonGDZ.Text = "ГДЗ бот";
            this.buttonGDZ.UseVisualStyleBackColor = true;
            this.buttonGDZ.Click += new System.EventHandler(this.buttonGDZ_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 585);
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
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
    }
}

