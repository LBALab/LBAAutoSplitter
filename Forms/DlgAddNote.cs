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
    public partial class DlgAddNote : Form
    {
        int splitIndex;
        Route ru = new Route();
        public DlgAddNote(int splitIndex)
        {
            InitializeComponent();
            this.splitIndex = splitIndex;
            txtNote.Text = ru.splits[splitIndex].note;
        }

        private void BtnOkay_Click(object sender, EventArgs e)
        {
            Route ru = new Route();
            ru.splits[splitIndex].note = txtNote.Text;
            ru.Save(false);
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
