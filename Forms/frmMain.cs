using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBAAutoSplitter
{

    public partial class FrmMain : Form
    {
        #region variables
        /**
         * Maybe should write a "run" object
         * to segment all of these out
         */
        //TmrInterval_Tick static objects
        AreaCode oAreacode;    //This would be a static variable in TmrInterval_Tick in C, but C# doesn't allow it    
        TimeSpan tsPrevSegments;
        bool startedRun = false;
        bool reset = false;
        DateTime dtRunStart;
        string saveFilePath;
        int splitIndex;        
        Route route;
        #endregion
        #region frmMain
        /**Sets double buffered on lvSplits to prevent flickering on update
         * Updates the title bar with app version.
         * loads the splits for visual purposes
         */
        public FrmMain()
        {
            InitializeComponent();
            fixColours();
            SetDoubleBuffered(lvSplits);
            this.Text = "Auto Splitter v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            LoadSplits();
        }
        #endregion

        private void fixColours()
        {
            Options opt = new Options();
            if (opt.transparentBackground)
            {
                this.TransparencyKey = this.BackColor;
            }
            else
            {
                this.TransparencyKey = Color.Empty;
                if (Color.Empty != opt.backgroundColour)
                {
                    try { this.BackColor = opt.backgroundColour; } catch (Exception e) { }
                    
                    foreach (Control C in this.Controls)
                    {
                        try
                        {
                            C.BackColor = opt.backgroundColour;
                        }
                        catch (Exception e) { }                        
                    }
                }
            }
            if(Color.Empty != opt.foreColour)
            {
                foreach (Control C in this.Controls)
                {
                    try
                    {
                        C.ForeColor = opt.foreColour;
                    }
                    catch (Exception e) { }
                }
            }
        }
        #region setDoubleBuffered
        /**
         * Used to stop flickering on interface update
         * caused by constantly updating with times
         */
        public static void SetDoubleBuffered(Control control)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }
        #endregion
        #region LoadSplits
        /**
         * Loads splits from route file, and updates the ListView
         */
        //Possibly should only reference this from "Start" and  wait until then to 
        //Configure splits, but I like it being visual on app start
        //Maybe rename this to refresh
        private void LoadSplits()
        {
            Options opt = new Options();
            ListViewItem lvi;
            route = new Route();
            Locations loc = new Locations();
            lvSplits.Items.Clear();
            for (int i = 0; i < route.splits.Length-1; i++)
            {
                lvi = new ListViewItem();
                lvi.Text = loc.getIslandNameFromCode(route.splits[i].id);
                if(new Options().showSubArea) lvi.Text += ": " + loc.getAreaNameFromCode(route.splits[i].id);
                lvi.SubItems.Add(TimeSpanToString(new TimeSpan(route.splits[i].bestEver), false));
                lvi.SubItems.Add(TimeSpanToString(new TimeSpan(route.splits[i].PBTime), false));
                lvi.SubItems.Add("0");
                lvi.Tag = route.splits[i].id;

                lvSplits.Items.Add(lvi);
            }
            if (opt.saveColumnWidths)
            {
                lvSplits.Columns[0].Width = (int)StringToLong(opt.mainLVColumnWidths.areaName);
                lvSplits.Columns[1].Width = (int)StringToLong(opt.mainLVColumnWidths.differenceBest);
                lvSplits.Columns[2].Width = (int)StringToLong(opt.mainLVColumnWidths.differencePB);
                lvSplits.Columns[3].Width = (int)StringToLong(opt.mainLVColumnWidths.time);
            }
            else
            {
                lvSplits.Columns[0].Width = -1;
                lvSplits.Columns[1].Width = -1;
                lvSplits.Columns[2].Width = -1;
                lvSplits.Columns[3].Width = 60;
            }
            lblSumOfBestText.Text = TimeSpanToString(route.GetSumOfBest(), false);
            lblPBText.Text = TimeSpanToString(route.GetPBTime(), false);
            this.TopMost = opt.alwaysOnTop;
        }
        private long StringToLong(string str)
        {
            if (long.TryParse(str, out long res))
                return res;
            return 0;
        }
        #endregion
        #region menuItems
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuspendAlwaysOnTop();
            FrmAbout fa = new FrmAbout();
            fa.ShowDialog();
            fa.Dispose();
            ResumeAlwaysOnTop();
        }
        private void SetLBADirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuspendAlwaysOnTop();
            FrmOptions fo = new FrmOptions();
            fo.ShowDialog();
            fo.Dispose();
            RefreshRoute();
            ResumeAlwaysOnTop();
            fixColours();
        }
        private void ConfigureSplitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuspendAlwaysOnTop();
            FrmSetupSplits fss = new FrmSetupSplits();
            fss.ShowDialog();
            fss.Dispose();
            RefreshRoute();
            ResumeAlwaysOnTop();
        }
        private void FAQsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuspendAlwaysOnTop();
            FrmFAQ fFAQ = new FrmFAQ();
            fFAQ.ShowDialog();
            fFAQ.Dispose();
            ResumeAlwaysOnTop();
        }

        #endregion
        #region TimerStateToggle
        private void BtnStart_Click(object sender, EventArgs e)
        {
            ToggleTimer();
        }

        /** 
         * Self explanatory - if off, turn on, if on, turn off
         */ 
        private void ToggleTimer()
        {
            //If the timer isn't running
            if (!tmrInterval.Enabled)
            {
                RefreshRoute();
                //int x;
                if (!Int32.TryParse(new Options().interval, out int x))
                    return;

                tmrInterval.Interval = x;
                tmrInterval.Start();
                btnStart.Text = "Stop";
            }
            else
            {
                startedRun = false;
                tmrInterval.Stop();
                oAreacode = null;
                btnStart.Text = "Start";
                tsPrevSegments = TimeSpan.Zero;
            }
        }
        #endregion
        #region runNotStarted
        /**
         * Keeps polling for a save file, when it finds the save file it looks for the areacode
         * If it's the correct areacode, i.e. if it's the first split it triggers the timer
         * This is the only time we update due to areacode in file.
         * We were updating all splits based on file, however this caused issues when
         * LBA was attempting to save the save file and we were attempting to copy it to query
         * which led to the save file not being actually saved and thus the split never changing
         * 
         * On finding first split we log DateTime.Now, set the started run flag to true, and set the current run time to 0
         */
        private void RunNotStarted()
        {
            string filePath = null;
            
            if (!tmrInterval.Enabled) return;
            //Keep polling until we have a file
            if (null == oAreacode)
            {

                filePath = CheckNewLBAFileInDir(new Options().LBADir);
                if (null == filePath) return;
                saveFilePath = filePath;
            }
            splitIndex = 0;
            //Check for no splits configured
            if (0 == route.splits.Length)
            {
                ToggleTimer();
                return;
            }
            oAreacode = new AreaCode(saveFilePath);
            //We should have a file
            if (oAreacode.GetAreaCodeMemory() == route.splits[splitIndex].id)
            {
                startedRun = true;
                System.Threading.Thread.Sleep(getInt(new Options().startTimeDelay));
                dtRunStart = DateTime.Now;
                if(new Options().disableAutoZoom) new Mem().WriteVal(0xE0A , 0, 1);
                if(new Options().defaultInventorySquare) new Mem().WriteVal(0x12F4, 27, 1);
                lblTime.Text = TimeSpanToString(dtRunStart - DateTime.Now, true);
            }
        }
        private int getInt(string value)
        {
            int val;
            if (!int.TryParse(value, out val)) return 0;
            return val;
        }

        /*private string getBasePath()
        {
            //
            string path;
            try
            {
                path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            }
            return "";
        }*/
        #endregion
        private void RunEnd()
        {
            long overallTime = 0;
            for (int i = 0; i < route.splits.Length; i++)
                overallTime += route.splits[i].runSplitTime;
            //DateTime dtEnd = DateTime.Now;
            TimeSpan runTime = new TimeSpan(overallTime);
            ToggleTimer();
            lblTime.Text = TimeSpanToString(runTime, false);

            route.splits[route.splits.Count() - 1].runSplitTime = runTime.Ticks;
            if (new Options().autoSaveSplits)
                SaveSplits(true);
            else
                route.RecalculateTimes();
            lblSumOfBestText.Text = TimeSpanToString(route.GetSumOfBest(), false);
            lblPBText.Text = TimeSpanToString(route.GetPBTime(), false);
            if (new Options().showMessageBoxOnRunEnd) MessageBox.Show("Congratulations");
        }

        #region saveSplits
        private void SaveSplits(bool completedRun)
        {
            route.Save(completedRun);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveSplits(false);
        }
        #endregion
        #region runStarted
        /** 
         * The main function called by the timer when it ticks every options.interval ms
         * If it's the last split this function updates the interface and passes control to
         * runEnd()
         */
        bool possibleReset;
        bool newGameStarted;
        private void RunStarted()
        {
            if (route.splits.Length - 1 > splitIndex)
            {
                TimeSpan tsOverall = DateTime.Now - dtRunStart;
                lblTime.Text = TimeSpanToString(tsOverall, false);
                TimeSpan ts = tsOverall - tsPrevSegments;
                route.splits[splitIndex].runSplitTime = ts.Ticks;
                UpdateLVI(splitIndex, ts);

                /*if (255 == getInt(oAreacode.GetAreaCodeMemory())) 
                    possibleReset = true;
                if(possibleReset && oAreacode.GetAreaCodeMemory() == route.splits[0].id)
                {
                    //reset
                    ToggleTimer();
                    ToggleTimer();
                }
                else
                {
                    possibleReset = false;
                }*/
                //Check if we're at a split area
                if (oAreacode.GetAreaCodeMemory() == route.splits[splitIndex + 1].id)
                {
                    splitIndex++;
                    tsPrevSegments += ts;
                    //If final split then gameover stuff
                    if (splitIndex + 1 == route.splits.Length)
                    {
                        RunEnd();
                    }
                }
            }
        }
        #endregion
        #region mainTimer
        /**
         * Calls one of runNotStarted or runStarted based upon the value of startedRun
         */
        private void TmrInterval_Tick(object sender, EventArgs e)
        {
            if (!startedRun)
            {
                RunNotStarted();
            }
            else
            {
                RunStarted();
            }
        }
        #endregion
        #region update GUI with split
        /**
         * Handles updating the interface with split times 
         */
        private void UpdateLVI(int index, TimeSpan ts)
        {
            //If we're not at last split
            if (index < lvSplits.Items.Count)
            {
                lvSplits.Items[index].SubItems[1].Text = TimeSpanToString(ts - new TimeSpan(route.splits[index].bestEver), true);
                lvSplits.Items[index].SubItems[2].Text = TimeSpanToString(ts - new TimeSpan(route.splits[index].PBTime), true);
                lvSplits.Items[index].SubItems[3].Text = TimeSpanToString(ts, false);
            }

            lblSumOfBestText.Text = TimeSpanToString(route.GetSumOfBest(), false);
            lblPBText.Text = TimeSpanToString(route.GetPBTime(), false);
        }
        #endregion
        #region saveFileExists
        /** Returns filepath on success
         *  else returns null
         */
        private string CheckNewLBAFileInDir(string dir)
        {
            string[] saveFiles = Directory.GetFiles(dir, "*.LBA");
            if (0 == saveFiles.Length) return null; //No save file exists
            if (1 != saveFiles.Length)
            {
                return null;
            }
            return saveFiles[0];
        }
        #endregion
        #region deleteSaves
        /**
         * Deletes all save files in the selected
         * LBA directory - triggered from flag in oOptions
         */
        private void DeleteSaves()
        {
            //ToDo: Not crash if remove fails.
            try
            {
                Options opt = new Options();
                //Loop through and delete all saves in the LBA directory
                while (0 != Directory.GetFiles(opt.LBADir, "*.LBA").Count())
                    File.Delete(Directory.GetFiles(opt.LBADir, "*.LBA")[0]);
            }
            catch(Exception e)
            { }

        }
        #endregion
        #region refresh
        /**
         * Updates LVI with current route and deletes saves if needed
         */
        private void RefreshRoute()
        {
            route = new Route(); //Update the route with current information i.e. if they've update splits
            if (new Options().deleteSaves) DeleteSaves(); //Check and delete saves if the option flag is set
            LoadSplits(); //Update lvi with current splits, needed for if they've been changed.
        }
        #endregion

        #region timeSpanToString
        /**
         * Converts a timeSpan object to a user friendly equiavalent string
         */
        private string TimeSpanToString(TimeSpan ts, bool addPlusMinus)
        {
            Options opt = new Options();
            if (!int.TryParse(opt.precisionDigits, out int precisionDigits))
                precisionDigits = 3; //Default to 3 if TryParse fails

            string posNeg;
            string time ="";
            if (addPlusMinus)
            {
                posNeg = "+";
                if (0 > ts.Ticks)
                {
                    ts = new TimeSpan(ts.Ticks * -1);
                    posNeg = "-";
                }
            }
            else
                posNeg = "";
            if (0 < ts.Hours)
                time += ts.Hours.ToString("0") + ":";
            if (0 < ts.Minutes || 0 != time.Length)
            {
                string s = ts.Minutes.ToString("00");
                //If no hours and less than 10 minutes drop first 0
                if (0 == time.Length && '0' == s.Substring(0, 1)[0])
                    time += s.Substring(1);
                else
                    time += ts.Minutes.ToString("00");
                time += ":";
            }
            if (0 < ts.Seconds || 0 != time.Length)
                time += ts.Seconds.ToString("00");
            if (0 != precisionDigits)
            {
                if (0 != time.Length)
                    time += ".";
                time += ts.Milliseconds.ToString("000").Substring(0, precisionDigits);//ToDo: Need to retrieve all 3 milliseconds then trim as per options
            }
            return posNeg + time;
        }
        #endregion

        #region formEvents
        /**
         * Used to save column width
         */
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Options opt = new Options();
            if(opt.saveColumnWidths)
            {
                opt.mainLVColumnWidths.areaName = lvSplits.Columns[0].Width.ToString();
                opt.mainLVColumnWidths.differenceBest = lvSplits.Columns[1].Width.ToString();
                opt.mainLVColumnWidths.differencePB = lvSplits.Columns[2].Width.ToString();
                opt.mainLVColumnWidths.time = lvSplits.Columns[3].Width.ToString();
            }
            opt.save();
        }

        private void LvSplits_DoubleClick(object sender, EventArgs e)
        {
            SuspendAlwaysOnTop();
            int index = lvSplits.SelectedIndices[0];
            DlgAddNote dAN = new DlgAddNote(index);
            dAN.ShowDialog();
            dAN.Dispose();
            ResumeAlwaysOnTop();
        }

        private void LvSplits_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            Options opt = new Options();
            opt.mainLVColumnWidths.areaName = lvSplits.Columns[0].Width.ToString();
            opt.mainLVColumnWidths.differenceBest = lvSplits.Columns[1].Width.ToString();
            opt.mainLVColumnWidths.differencePB = lvSplits.Columns[2].Width.ToString();
            opt.mainLVColumnWidths.time = lvSplits.Columns[3].Width.ToString();
            opt.save();
        }

        private void LoadRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofb = new OpenFileDialog();
            ofb.ShowDialog();
            string res = ofb.FileName;
            ofb.Dispose();
            if (0 == res.Length) return; //Do nothing if fileName is blank
            Options opt = new Options();
            opt.routeFilePath = res;
            opt.save();
            RefreshRoute();
        }

        #endregion

        private void SuspendAlwaysOnTop()
        {
            this.TopMost = false;
        }
        private void ResumeAlwaysOnTop()
        {
            this.TopMost = new Options().alwaysOnTop;
        }

        /**
         * Run against PB
         * Run against best split
         */

    }
}


