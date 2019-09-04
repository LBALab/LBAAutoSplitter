using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBAAutoSplitter
{
    public partial class FrmFAQ : Form
    {
        public FrmFAQ()
        {
            InitializeComponent();
            loadFAQs();
        }

        private void loadFAQs()
        {
            tbFAQ.Text = "Q: Why don't my splits always equal my overall time?\n\r\n\r";
            tbFAQ.Text += "A: All times are only displayed by thousandths of a second, but stored more precisely.\n\r\n\r";
        }
    }
}
