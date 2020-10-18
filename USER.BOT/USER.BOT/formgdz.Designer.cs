namespace USER.BOT
{
    partial class formgdz
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
            this.labelgrope = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelgrope
            // 
            this.labelgrope.AutoSize = true;
            this.labelgrope.Location = new System.Drawing.Point(184, 533);
            this.labelgrope.Name = "labelgrope";
            this.labelgrope.Size = new System.Drawing.Size(51, 20);
            this.labelgrope.TabIndex = 0;
            this.labelgrope.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // formgdz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 807);
            this.Controls.Add(this.labelgrope);
            this.Name = "formgdz";
            this.Text = "formgdz";
            this.Load += new System.EventHandler(this.formgdz_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelgrope;
        private System.Windows.Forms.Timer timer1;
    }
}