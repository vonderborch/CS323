namespace BH_STG.BarrageEngine.Forms
{
    partial class EnhancedTextBox
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
            this.ok_btn = new System.Windows.Forms.Button();
            this.text_txt = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(824, 441);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 23);
            this.ok_btn.TabIndex = 0;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // text_txt
            // 
            this.text_txt.Location = new System.Drawing.Point(12, 12);
            this.text_txt.Name = "text_txt";
            this.text_txt.ReadOnly = true;
            this.text_txt.Size = new System.Drawing.Size(887, 423);
            this.text_txt.TabIndex = 1;
            this.text_txt.Text = "";
            // 
            // EnhancedTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 476);
            this.Controls.Add(this.text_txt);
            this.Controls.Add(this.ok_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnhancedTextBox";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Changelog";
            this.Load += new System.EventHandler(this.Changelog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.RichTextBox text_txt;
    }
}