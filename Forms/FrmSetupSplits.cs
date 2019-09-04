using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LBAAutoSplitter
{

    public partial class FrmSetupSplits : Form
    {
        Locations loc;
        public FrmSetupSplits()
        {
            InitializeComponent();
            PopulateComboBox();
            LoadRoute();
        }

        //Load current Route into lvSplits
        private void LoadRoute()
        {
            lvSplits.Items.Clear();
            ListViewItem lvi;
            Route ru = new Route();
            Locations loc = new Locations();

            //Bail out if no splits configured. Could have been a manually deleted route file
            if (null == ru.splits || 0 == ru.splits.Length) return;
            for (int i = 0; i < ru.splits.Length; i++)
            {
                lvi = new ListViewItem();
                lvi.Text = loc.getIslandNameFromCode(ru.splits[i].id);
                lvi.SubItems.Add(loc.getAreaNameFromCode(ru.splits[i].id));
                lvi.Tag = ru.splits[i].id;
                AddLVIToLVSplits(lvi);
            }
        }

        private void AddLVIToLVSplits(ListViewItem lvi)
        {
            lvSplits.Items.Add(lvi);
            lvSplits.Columns[0].Width = -1;
            lvSplits.Columns[1].Width = -1; ;
        }

        private void PopulateComboBox()
        {
            loc = new Locations();
            for (int i = 0; i < loc.islandName.Length; i++)
                cbIsland.Items.Add(loc.islandName[i]);
            cbIsland.Text = loc.islandName[0];
        }

        private void PopulateLV()
        {
            lvMain.Items.Clear();
            loc.getSubAreaFromIsland(cbIsland.Text);
            ListViewItem lvi;
            for (int i = 0; loc.subArea.GetLength(0) > i; i++)
            {
                lvi = new ListViewItem();
                lvi.Text = loc.subArea[i, 1];
                lvi.Tag = loc.subArea[i, 0];
                lvMain.Items.Add(lvi);
            }
            lvMain.Columns[0].Width = -1;
        }
        private void CbIsland_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateLV();
        }

        private void BtnAddToSplits_Click(object sender, EventArgs e)
        {
            ListViewItem lviNew = new ListViewItem();
            //If none are selected, or multiple, bail out
            if (1 != lvMain.SelectedItems.Count) return;
            lviNew.Text = cbIsland.Text;
            lviNew.SubItems.Add(lvMain.SelectedItems[0].Text);
            lviNew.Tag = lvMain.SelectedItems[0].Tag;
            AddLVIToLVSplits(lviNew);
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            int pos;
            ListViewItem temp;
            if (1 != lvSplits.SelectedItems.Count) return;
            pos = lvSplits.SelectedItems[0].Index;

            if (0!= pos)
            {
                temp = lvSplits.SelectedItems[0];

                lvSplits.Items.RemoveAt(pos);
                lvSplits.Items.Insert(pos - 1, temp);
            }
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            int pos;
            ListViewItem temp;
            if (1 != lvSplits.SelectedItems.Count) return;
            pos = lvSplits.SelectedItems[0].Index;

            temp = lvSplits.SelectedItems[0];

            lvSplits.Items.RemoveAt(pos);
            if (lvSplits.Items.Count == pos)
                pos--;
            if (pos != lvSplits.Items.Count)
                lvSplits.Items.Insert(pos + 1, temp);            
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (1 == lvSplits.SelectedItems.Count)
                lvSplits.Items.RemoveAt(lvSplits.SelectedItems[0].Index);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != MessageBox.Show("Warning, this will clear your current splits", "Clear splits", MessageBoxButtons.OKCancel)) return;
            SaveRoute(new Options().routeFilePath);
            this.Close();
        }

        private void SaveRoute(string path)
        {
            if (1 > lvSplits.Items.Count)
            {
                MessageBox.Show("Please configuring at least a start and end split");
                return;
            }
            Route ru = new Route();
            Split[] splits = new Split[lvSplits.Items.Count];

            for (int i = 0; i < lvSplits.Items.Count; i++)
            {
                Split t = new Split();                
                t.pos = (i + 1).ToString();
                t.id = (string)lvSplits.Items[i].Tag;
                splits[i] = t;
            }

            ru.CreateNew(path, splits);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            lvSplits.Items.Clear();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.CheckFileExists = false;
            sfd.DefaultExt = "xml";
            sfd.FileName = "route.xml";
            sfd.SupportMultiDottedExtensions = true;
            sfd.Title = "Save route file as";
            sfd.Filter = "XML Files | *.xml";
            sfd.ShowDialog();
            Options opt = new Options();
            opt.routeFilePath = sfd.FileName;
            opt.save();
            SaveRoute(sfd.FileName);
            sfd.Dispose();
        }
    }
}
