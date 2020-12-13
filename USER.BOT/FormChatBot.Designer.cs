namespace USER.BOT
{
    partial class FormChatBot
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
            this.ButtonChatBot = new System.Windows.Forms.Button();
            this.City1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Name1 = new System.Windows.Forms.Label();
            this.Name2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ButtonChatBot
            // 
            this.ButtonChatBot.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ButtonChatBot.Cursor = System.Windows.Forms.Cursors.Default;
            this.ButtonChatBot.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonChatBot.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ButtonChatBot.Location = new System.Drawing.Point(830, 376);
            this.ButtonChatBot.Name = "ButtonChatBot";
            this.ButtonChatBot.Size = new System.Drawing.Size(215, 112);
            this.ButtonChatBot.TabIndex = 0;
            this.ButtonChatBot.Text = "Начать";
            this.ButtonChatBot.UseVisualStyleBackColor = false;
            this.ButtonChatBot.Visible = false;
            this.ButtonChatBot.Click += new System.EventHandler(this.ButtonChatBot_Click);
            // 
            // City1
            // 
            this.City1.AutoSize = true;
            this.City1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.City1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.City1.Location = new System.Drawing.Point(12, 401);
            this.City1.Name = "City1";
            this.City1.Size = new System.Drawing.Size(70, 25);
            this.City1.TabIndex = 1;
            this.City1.Text = "label1";
            this.City1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(12, 463);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // Name1
            // 
            this.Name1.AutoSize = true;
            this.Name1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Name1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name1.Location = new System.Drawing.Point(14, 376);
            this.Name1.Name = "Name1";
            this.Name1.Size = new System.Drawing.Size(40, 13);
            this.Name1.TabIndex = 3;
            this.Name1.Text = "Город:";
            this.Name1.Visible = false;
            // 
            // Name2
            // 
            this.Name2.AutoSize = true;
            this.Name2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name2.Location = new System.Drawing.Point(14, 441);
            this.Name2.Name = "Name2";
            this.Name2.Size = new System.Drawing.Size(99, 13);
            this.Name2.TabIndex = 4;
            this.Name2.Text = "Последняя Буква:";
            this.Name2.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(978, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "Нажми!!!";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormChatBot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1057, 500);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Name2);
            this.Controls.Add(this.Name1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.City1);
            this.Controls.Add(this.ButtonChatBot);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "FormChatBot";
            this.Text = "FormChatBot";
            this.Load += new System.EventHandler(this.FormChatBot_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonChatBot;
        private System.Windows.Forms.Label City1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Name1;
        private System.Windows.Forms.Label Name2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;

    }
}