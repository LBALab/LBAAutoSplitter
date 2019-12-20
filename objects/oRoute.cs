using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LBAAutoSplitter
{
    class Route
    {
        public Split[] splits;
        XmlDocument doc;
        private string path;
        public Route()
        {
            ReadSplitInfo();
        }

        /**
         * opens route.xml and reads it in to an array of splits
         */
        private void ReadSplitInfo()
        {
            doc = new XmlDocument();
            Options opt = new Options();
            path = opt.routeFilePath;
            if (!File.Exists(path)) return;
            doc.Load(path);

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/route/area");

            splits = new Split[nodes.Count];

            for (int i = 0; i < splits.Length; i++)
            {
                splits[i] = new Split();
                splits[i].pos = nodes[i].Attributes["pos"].Value;
                splits[i].id = nodes[i].Attributes["id"].Value;
                splits[i].bestEver = StringToTicks(doc.DocumentElement.SelectNodes("/route/area/bestEver")[i].InnerText.Trim());
                splits[i].PBTime = StringToTicks(doc.DocumentElement.SelectNodes("/route/area/PBTime")[i].InnerText.Trim());
                //splits[i].splitName = doc.DocumentElement.SelectNodes("/route/area/splitName")[i].InnerText.Trim();
                //splits[i].note = doc.DocumentElement.SelectNodes("/route/area/splitNote")[i].InnerText.Trim();
            }
        }

        private long CalculateSumOfBest()
        {
            int i;
            long sumBest =0;
            for (i = 0; i < splits.Length - 1; i++)
                sumBest += splits[i].bestEver;
            return sumBest;

        }

        public TimeSpan GetSumOfBest()
        {
            return new TimeSpan(splits[splits.Length-1].bestEver);
        }

        public TimeSpan GetPBTime()
        {
            return new TimeSpan(splits[splits.Length - 1].PBTime);
        }
        private long StringToTicks(string ticks)
        {
            return (StringToLong(ticks));
        }

        private long StringToLong(string str)
        {
            if (long.TryParse(str, out long res))
                return res;
            return 0;
        }

        public void RecalculateTimes()
        {
            if (null == doc) throw new Exception();

            long runSplitTimeTotal;
            long pbTime = splits[splits.Length - 1].PBTime;
            
            runSplitTimeTotal = splits[splits.Length-1].runSplitTime;

            for (int i = 0; i < splits.Length-1; i++)
            {
                long bestEver = StringToLong(doc.DocumentElement.SelectNodes("/route/area/bestEver")[i].InnerText);
                //If bestEver is zero, runSplitTime isn't zero, i.e. there's been a run, and splitTime is less than bestEver
                if (0 == bestEver || (splits[i].runSplitTime < bestEver && 0 != splits[i].runSplitTime))
                {
                    doc.DocumentElement.SelectNodes("/route/area/bestEver")[i].InnerText = splits[i].runSplitTime.ToString();
                    splits[i].bestEver = splits[i].runSplitTime;
                }

                //If sum of runSplitTime < PBTime save PBs

                if (0 == pbTime  || (runSplitTimeTotal < pbTime && 0 != splits[i].runSplitTime))
                    doc.DocumentElement.SelectNodes("/route/area/PBTime")[i].InnerText = splits[i].runSplitTime.ToString();
            }
            splits[splits.Length - 1].bestEver = CalculateSumOfBest();
            doc.DocumentElement.SelectNodes("/route/area/bestEver")[splits.Length-1].InnerText = splits[splits.Length - 1].bestEver.ToString();

            if (0 == pbTime || runSplitTimeTotal < pbTime)
            {
                splits[splits.Length - 1].PBTime = runSplitTimeTotal;
                doc.DocumentElement.SelectNodes("/route/area/PBTime")[splits.Length - 1].InnerText = splits[splits.Length - 1].PBTime.ToString();

            }
        }
        /** ToDo: Currently this causes split times to zero
         */
        public void Save(bool completedRun)
        {
            try
            {
                if (completedRun)
                    RecalculateTimes();
                for (int i = 0; i < splits.Length - 1; i++)
                {
                    doc.DocumentElement.SelectNodes("/route/area/splitNote")[i].InnerText = splits[i].note;
                }
                doc.Save(path);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace + "\n" + e.InnerException + "\n");
                MessageBox.Show("Error saving splits");
                MessageBox.Show("Doc null? " + (null == doc).ToString());            }
        }

        public void CreateNew(string path, Split[] splits )
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.IndentChars = ("\t");
            xmlWriterSettings.NewLineOnAttributes = false;
            xmlWriterSettings.Encoding = Encoding.GetEncoding("ISO-8859-1");

            //string path = new oOptions().routeFilePath;
            if (File.Exists(path))
                File.Delete(path);
            using (XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings))
            {

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("route");

                for (int i = 0; i < splits.Length; i++)
                {
                    xmlWriter.WriteStartElement("area");
                    xmlWriter.WriteAttributeString("pos", (i + 1).ToString());
                    xmlWriter.WriteAttributeString("id", splits[i].id);
                    xmlWriter.WriteStartElement("bestEver");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("PBTime");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("splitNote");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("splitName");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
            }
            ReadSplitInfo();
        }
    }
}
