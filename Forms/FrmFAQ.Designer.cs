namespace LBAAutoSplitter
{
    partial class FrmFAQ
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
            this.btnClose = new System.Windows.Forms.Button();
            this.gbFAQ = new System.Windows.Forms.GroupBox();
            this.tbFAQ = new System.Windows.Forms.TextBox();
            this.gbFAQ.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(91, 347);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // gbFAQ
            // 
            this.gbFAQ.Controls.Add(this.tbFAQ);
            this.gbFAQ.Location = new System.Drawing.Point(12, 12);
            this.gbFAQ.Name = "gbFAQ";
            this.gbFAQ.Size = new System.Drawing.Size(236, 329);
            this.gbFAQ.TabIndex = 1;
            this.gbFAQ.TabStop = false;
            this.gbFAQ.Text = "FAQ";
            // 
            // tbFAQ
            // 
            this.tbFAQ.Location = new System.Drawing.Point(6, 19);
            this.tbFAQ.Multiline = true;
            this.tbFAQ.Name = "tbFAQ";
            this.tbFAQ.Size = new System.Drawing.Size(224, 300);
            this.tbFAQ.TabIndex = 0;
            // 
            // FrmFAQ
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(260, 382);
            this.ControlBox = false;
            this.Controls.Add(this.gbFAQ);
            this.Controls.Add(this.btnClose);
            this.Name = "FrmFAQ";
            this.Text = "FAQs";
            this.gbFAQ.ResumeLayout(false);
            this.gbFAQ.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox gbFAQ;
        private System.Windows.Forms.TextBox tbFAQ;
    }
}