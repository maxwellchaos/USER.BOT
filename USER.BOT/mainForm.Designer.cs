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
            this.buttonSelebrate = new System.Windows.Forms.Button();
            this.ButtonChatBot = new System.Windows.Forms.Button();
            this.buttonLiking = new System.Windows.Forms.Button();
            this.pictureBoxAvatar = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelFamily = new System.Windows.Forms.Label();
            this.buttonMassComment = new System.Windows.Forms.Button();
            this.buttonFindComments = new System.Windows.Forms.Button();
            this.buttonGDZ = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSelebrate
            // 
            this.buttonSelebrate.Location = new System.Drawing.Point(655, 41);
            this.buttonSelebrate.Name = "buttonSelebrate";
            this.buttonSelebrate.Size = new System.Drawing.Size(256, 22);
            this.buttonSelebrate.TabIndex = 12;
            this.buttonSelebrate.Text = "Поздравление с праздником";
            this.buttonSelebrate.UseVisualStyleBackColor = true;
            // 
            // ButtonChatBot
            // 
            this.ButtonChatBot.Location = new System.Drawing.Point(655, 219);
            this.ButtonChatBot.Name = "ButtonChatBot";
            this.ButtonChatBot.Size = new System.Drawing.Size(258, 63);
            this.ButtonChatBot.TabIndex = 11;
            this.ButtonChatBot.Text = "Бот автоответчик для группы";
            this.ButtonChatBot.UseVisualStyleBackColor = true;
            this.ButtonChatBot.Click += new System.EventHandler(this.ButtonChatBot_Click);
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
            this.labelName.Location = new System.Drawing.Point(128, 64);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(57, 13);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "labelName";
            // 
            // labelFamily
            // 
            this.labelFamily.AutoSize = true;
            this.labelFamily.Location = new System.Drawing.Point(128, 51);
            this.labelFamily.Name = "labelFamily";
            this.labelFamily.Size = new System.Drawing.Size(58, 13);
            this.labelFamily.TabIndex = 13;
            this.labelFamily.Text = "labelFamily";
            // 
            // buttonMassComment
            // 
            this.buttonMassComment.Location = new System.Drawing.Point(655, 69);
            this.buttonMassComment.Name = "buttonMassComment";
            this.buttonMassComment.Size = new System.Drawing.Size(256, 22);
            this.buttonMassComment.TabIndex = 16;
            this.buttonMassComment.Text = "Массовый комментинг";
            this.buttonMassComment.UseVisualStyleBackColor = true;
            // 
            // buttonFindComments
            // 
            this.buttonFindComments.Location = new System.Drawing.Point(655, 98);
            this.buttonFindComments.Name = "buttonFindComments";
            this.buttonFindComments.Size = new System.Drawing.Size(256, 22);
            this.buttonFindComments.TabIndex = 17;
            this.buttonFindComments.Text = "Поиск неотвеченных комментариев";
            this.buttonFindComments.UseVisualStyleBackColor = true;
            // 
            // buttonGDZ
            // 
            this.buttonGDZ.Location = new System.Drawing.Point(655, 126);
            this.buttonGDZ.Name = "buttonGDZ";
            this.buttonGDZ.Size = new System.Drawing.Size(256, 22);
            this.buttonGDZ.TabIndex = 18;
            this.buttonGDZ.Text = "ГДЗ бот";
            this.buttonGDZ.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(305, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(250, 145);
            this.webBrowser1.TabIndex = 19;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 380);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.buttonGDZ);
            this.Controls.Add(this.buttonFindComments);
            this.Controls.Add(this.buttonMassComment);
            this.Controls.Add(this.pictureBoxAvatar);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelFamily);
            this.Controls.Add(this.buttonSelebrate);
            this.Controls.Add(this.ButtonChatBot);
            this.Controls.Add(this.buttonLiking);
            this.Name = "mainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSelebrate;
        private System.Windows.Forms.Button ButtonChatBot;
        private System.Windows.Forms.Button buttonLiking;
        private System.Windows.Forms.PictureBox pictureBoxAvatar;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelFamily;
        private System.Windows.Forms.Button buttonMassComment;
        private System.Windows.Forms.Button buttonFindComments;
        private System.Windows.Forms.Button buttonGDZ;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

