namespace USER.BOT
{
    partial class AnswerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ID1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Comments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Text1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonInput = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.ID2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.parentID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FirstName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Text2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelOutput = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID1,
            this.Comments,
            this.Text1});
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(620, 150);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.listView1_ColumnWidthChanged);
            this.listView1.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView1_ColumnWidthChanging);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // ID1
            // 
            this.ID1.Text = "ID";
            this.ID1.Width = 52;
            // 
            // Comments
            // 
            this.Comments.Text = "Комментарии";
            this.Comments.Width = 87;
            // 
            // Text1
            // 
            this.Text1.Text = "Текст";
            this.Text1.Width = 380;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // textBoxInput
            // 
            this.textBoxInput.Enabled = false;
            this.textBoxInput.Location = new System.Drawing.Point(397, 343);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(391, 27);
            this.textBoxInput.TabIndex = 2;
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
            // 
            // buttonInput
            // 
            this.buttonInput.Location = new System.Drawing.Point(397, 376);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(123, 23);
            this.buttonInput.TabIndex = 3;
            this.buttonInput.Text = "Поиск";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.SystemColors.Window;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID2,
            this.parentID,
            this.FirstName,
            this.LastName,
            this.Text2});
            this.listView2.HideSelection = false;
            this.listView2.LargeImageList = this.imageList1;
            this.listView2.Location = new System.Drawing.Point(12, 168);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(776, 169);
            this.listView2.SmallImageList = this.imageList1;
            this.listView2.TabIndex = 6;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            // 
            // ID2
            // 
            this.ID2.Text = "ID";
            this.ID2.Width = 37;
            // 
            // parentID
            // 
            this.parentID.Text = "ID ответ";
            this.parentID.Width = 61;
            // 
            // FirstName
            // 
            this.FirstName.Text = "Имя";
            this.FirstName.Width = 91;
            // 
            // LastName
            // 
            this.LastName.Text = "Фамилия";
            this.LastName.Width = 167;
            // 
            // Text2
            // 
            this.Text2.Text = "Текст";
            this.Text2.Width = 332;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1.Location = new System.Drawing.Point(526, 376);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(262, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(638, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOutput.Location = new System.Drawing.Point(12, 346);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(379, 15);
            this.labelOutput.TabIndex = 9;
            this.labelOutput.Text = "Нажмите на кнопку для вывода всех записей с вашей страницы";
            // 
            // AnswerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 413);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.buttonInput);
            this.Controls.Add(this.textBoxInput);
            this.MaximumSize = new System.Drawing.Size(816, 452);
            this.MinimumSize = new System.Drawing.Size(816, 452);
            this.Name = "AnswerForm";
            this.Text = "Ответ на комментарии";
            this.Load += new System.EventHandler(this.AnswerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonInput;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader ID2;
        private System.Windows.Forms.ColumnHeader parentID;
        private System.Windows.Forms.ColumnHeader FirstName;
        private System.Windows.Forms.ColumnHeader LastName;
        private System.Windows.Forms.ColumnHeader Text2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.ColumnHeader ID1;
        private System.Windows.Forms.ColumnHeader Comments;
        private System.Windows.Forms.ColumnHeader Text1;
    }
}