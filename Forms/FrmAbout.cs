using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBAAutoSplitter
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
            lblAbout.Text = "Shoutouts to El Muerte whose documentation on the LBA SaveGame file saved me a lot of work.\n\n";
            lblAbout.Text += "Thanks to Kash for always being supportive and the hours of entertainment and dedication to LBA.\n\n";
            lblAbout.Text += "Thanks to everybody who's helped in the LBA Discord.\n\n";
            lblAbout.Text += "Thanks to everybody who's using this especially Mike and Roxer for being first.\n\n";
            lblAbout.Text += "Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
