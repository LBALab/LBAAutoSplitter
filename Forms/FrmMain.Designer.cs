namespace LBAAutoSplitter
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRouteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setLBADirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureSplitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fAQsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvSplits = new System.Windows.Forms.ListView();
            this.chAreaName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDiffBest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTimeDiffPB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTime = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tmrInterval = new System.Windows.Forms.Timer(this.components);
            this.lbltimeText = new System.Windows.Forms.Label();
            this.lblSumOfBest = new System.Windows.Forms.Label();
            this.lblSumOfBestText = new System.Windows.Forms.Label();
            this.lblPB = new System.Windows.Forms.Label();
            this.lblPBText = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(288, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRouteToolStripMenuItem,
            this.setLBADirectoryToolStripMenuItem,
            this.configureSplitsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // loadRouteToolStripMenuItem
            // 
            this.loadRouteToolStripMenuItem.Name = "loadRouteToolStripMenuItem";
            this.loadRouteToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.loadRouteToolStripMenuItem.Text = "&Load Route";
            this.loadRouteToolStripMenuItem.Click += new System.EventHandler(this.LoadRouteToolStripMenuItem_Click);
            // 
            // setLBADirectoryToolStripMenuItem
            // 
            this.setLBADirectoryToolStripMenuItem.Name = "setLBADirectoryToolStripMenuItem";
            this.setLBADirectoryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.setLBADirectoryToolStripMenuItem.Text = "&Options";
            this.setLBADirectoryToolStripMenuItem.Click += new System.EventHandler(this.SetLBADirectoryToolStripMenuItem_Click);
            // 
            // configureSplitsToolStripMenuItem
            // 
            this.configureSplitsToolStripMenuItem.Name = "configureSplitsToolStripMenuItem";
            this.configureSplitsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.configureSplitsToolStripMenuItem.Text = "&Configure Splits";
            this.configureSplitsToolStripMenuItem.Click += new System.EventHandler(this.ConfigureSplitsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.fAQsToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // fAQsToolStripMenuItem
            // 
            this.fAQsToolStripMenuItem.Name = "fAQsToolStripMenuItem";
            this.fAQsToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.fAQsToolStripMenuItem.Text = "FA&Qs";
            this.fAQsToolStripMenuItem.Click += new System.EventHandler(this.FAQsToolStripMenuItem_Click);
            // 
            // lvSplits
            // 
            this.lvSplits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSplits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAreaName,
            this.chDiffBest,
            this.chTimeDiffPB,
            this.chTime});
            this.lvSplits.FullRowSelect = true;
            this.lvSplits.HideSelection = false;
            this.lvSplits.Location = new System.Drawing.Point(12, 27);
            this.lvSplits.MultiSelect = false;
            this.lvSplits.Name = "lvSplits";
            this.lvSplits.Size = new System.Drawing.Size(264, 205);
            this.lvSplits.TabIndex = 1;
            this.lvSplits.UseCompatibleStateImageBehavior = false;
            this.lvSplits.View = System.Windows.Forms.View.Details;
            this.lvSplits.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.LvSplits_ColumnWidthChanged);
            this.lvSplits.DoubleClick += new System.EventHandler(this.LvSplits_DoubleClick);
            // 
            // chAreaName
            // 
            this.chAreaName.Text = "Area";
            this.chAreaName.Width = 80;
            // 
            // chDiffBest
            // 
            this.chDiffBest.Text = "Diff Best";
            this.chDiffBest.Width = 52;
            // 
            // chTimeDiffPB
            // 
            this.chTimeDiffPB.Text = "DiffPB";
            this.chTimeDiffPB.Width = 46;
            // 
            // chTime
            // 
            this.chTime.Text = "Time";
            this.chTime.Width = 81;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(162, 245);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(114, 26);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "0:00:00.00";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 388);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(93, 388);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // tmrInterval
            // 
            this.tmrInterval.Tick += new System.EventHandler(this.TmrInterval_Tick);
            // 
            // lbltimeText
            // 
            this.lbltimeText.AutoSize = true;
            this.lbltimeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltimeText.Location = new System.Drawing.Point(12, 245);
            this.lbltimeText.Name = "lbltimeText";
            this.lbltimeText.Size = new System.Drawing.Size(66, 26);
            this.lbltimeText.TabIndex = 8;
            this.lbltimeText.Text = "Time:";
            // 
            // lblSumOfBest
            // 
            this.lblSumOfBest.AutoSize = true;
            this.lblSumOfBest.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumOfBest.Location = new System.Drawing.Point(12, 286);
            this.lblSumOfBest.Name = "lblSumOfBest";
            this.lblSumOfBest.Size = new System.Drawing.Size(138, 26);
            this.lblSumOfBest.TabIndex = 10;
            this.lblSumOfBest.Text = "Sum of Best:";
            // 
            // lblSumOfBestText
            // 
            this.lblSumOfBestText.AutoSize = true;
            this.lblSumOfBestText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumOfBestText.Location = new System.Drawing.Point(162, 286);
            this.lblSumOfBestText.Name = "lblSumOfBestText";
            this.lblSumOfBestText.Size = new System.Drawing.Size(114, 26);
            this.lblSumOfBestText.TabIndex = 11;
            this.lblSumOfBestText.Text = "0:00:00.00";
            // 
            // lblPB
            // 
            this.lblPB.AutoSize = true;
            this.lblPB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPB.Location = new System.Drawing.Point(12, 329);
            this.lblPB.Name = "lblPB";
            this.lblPB.Size = new System.Drawing.Size(48, 26);
            this.lblPB.TabIndex = 12;
            this.lblPB.Text = "PB:";
            // 
            // lblPBText
            // 
            this.lblPBText.AutoSize = true;
            this.lblPBText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPBText.Location = new System.Drawing.Point(162, 329);
            this.lblPBText.Name = "lblPBText";
            this.lblPBText.Size = new System.Drawing.Size(114, 26);
            this.lblPBText.TabIndex = 13;
            this.lblPBText.Text = "0:00:00.00";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 423);
            this.Controls.Add(this.lblPBText);
            this.Controls.Add(this.lblPB);
            this.Controls.Add(this.lblSumOfBestText);
            this.Controls.Add(this.lblSumOfBest);
            this.Controls.Add(this.lbltimeText);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lvSplits);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Live Splitter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setLBADirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureSplitsToolStripMenuItem;
        private System.Windows.Forms.ListView lvSplits;
        private System.Windows.Forms.ColumnHeader chAreaName;
        private System.Windows.Forms.ColumnHeader chTimeDiffPB;
        private System.Windows.Forms.ColumnHeader chDiffBest;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Timer tmrInterval;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fAQsToolStripMenuItem;
        private System.Windows.Forms.Label lbltimeText;
        private System.Windows.Forms.Label lblSumOfBest;
        private System.Windows.Forms.Label lblSumOfBestText;
        private System.Windows.Forms.Label lblPB;
        private System.Windows.Forms.Label lblPBText;
        private System.Windows.Forms.ToolStripMenuItem loadRouteToolStripMenuItem;
    }
}

