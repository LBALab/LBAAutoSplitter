using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Drawing;

namespace LBAAutoSplitter
{
    class Options
    {
        /**
         * An object to provide a wrapper to the Options XML file
         */

        public string LBADir;
        public string interval;
        public string routeFilePath;
        public string precisionDigits;
        public string startTimeDelay;
        public bool disableAutoZoom;
        public bool defaultInventorySquare;
        public bool deleteSaves;
        public bool autoSaveSplits;
        public bool showSubArea;
        public bool showMessageBoxOnRunEnd;
        public bool saveColumnWidths;
        public bool transparentBackground;
        public bool autoReset;
        public Color backgroundColour;
        public Color foreColour;
        WindowInf wiMain;
        WindowInf wiNotes;
        public bool alwaysOnTop;
        public MainLVColumnWidths mainLVColumnWidths = new MainLVColumnWidths();
        public XmlDocument doc;
        private string path;

        public Options()
        {
            readOptionsFile();
        }
        private void readOptionsFile()
        {
            if (null == doc)
                doc = new XmlDocument();
            doc = new XmlDocument();
            path = AppDomain.CurrentDomain.BaseDirectory + "files\\options.xml";
            doc.Load(path);

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/options/LBADir");
            if (null == nodes) System.Windows.Forms.MessageBox.Show("Nodes is null");
            LBADir = nodes[0].InnerText.Trim();
            interval = doc.DocumentElement.SelectNodes("/options/interval")[0].InnerText.Trim();
            precisionDigits = doc.DocumentElement.SelectNodes("/options/precisionDigits")[0].InnerText.Trim();
            disableAutoZoom = "true" == doc.DocumentElement.SelectNodes("/options/disableAutoZoom")[0].InnerText.Trim().ToLower();
            defaultInventorySquare = "true" == doc.DocumentElement.SelectNodes("/options/defaultInventorySquare")[0].InnerText.Trim().ToLower();
            deleteSaves = "true" ==  doc.DocumentElement.SelectNodes("/options/deleteSaves")[0].InnerText.Trim().ToLower();
            autoSaveSplits = "true" == doc.DocumentElement.SelectNodes("/options/autoSaveSplits")[0].InnerText.Trim().ToLower();
            showSubArea = "true" == doc.DocumentElement.SelectNodes("/options/showSubArea")[0].InnerText.Trim().ToLower();
            showMessageBoxOnRunEnd = "true" == doc.DocumentElement.SelectNodes("/options/showMessageBoxOnRunEnd")[0].InnerText.Trim().ToLower();
            alwaysOnTop = "true" == doc.DocumentElement.SelectNodes("/options/alwaysOnTop")[0].InnerText.Trim().ToLower();
            transparentBackground = "true" == doc.DocumentElement.SelectNodes("/options/transparentBackground")[0].InnerText.Trim().ToLower();
            autoReset = "true" == doc.DocumentElement.SelectNodes("/options/autoReset")[0].InnerText.Trim().ToLower();
            backgroundColour = getColour(doc.DocumentElement.SelectNodes("/options/backgroundColour")[0].InnerText.Trim());
            foreColour = getColour(doc.DocumentElement.SelectNodes("/options/foreColour")[0].InnerText.Trim());

            routeFilePath = doc.DocumentElement.SelectNodes("/options/currentRouteFile")[0].InnerText.Trim();
            startTimeDelay = doc.DocumentElement.SelectNodes("/options/startTimeDelay")[0].InnerText.Trim();
            //If route file doesn't exist then default to default route file
            if (!File.Exists(routeFilePath))
            {
                routeFilePath= AppDomain.CurrentDomain.BaseDirectory + "files\\routeFiles\\route.xml";
            }

            saveColumnWidths = "true" == doc.DocumentElement.SelectNodes("/options/saveColumnWidths")[0].InnerText.Trim().ToLower();

            if (saveColumnWidths)
            {
                mainLVColumnWidths.areaName = doc.DocumentElement.SelectNodes("/options/mainLVColumnWidths")[0].SelectNodes("areaName")[0].InnerText;
                mainLVColumnWidths.differenceBest = doc.DocumentElement.SelectNodes("/options/mainLVColumnWidths")[0].SelectNodes("differenceBest")[0].InnerText;
                mainLVColumnWidths.differencePB = doc.DocumentElement.SelectNodes("/options/mainLVColumnWidths")[0].SelectNodes("differencePB")[0].InnerText;
                mainLVColumnWidths.time = doc.DocumentElement.SelectNodes("/options/mainLVColumnWidths")[0].SelectNodes("time")[0].InnerText;
            }
            else
            {
                mainLVColumnWidths.areaName = "-1";
                mainLVColumnWidths.differenceBest = "-1";
                mainLVColumnWidths.differencePB = "-1";
                mainLVColumnWidths.time = "-1";
            }
        }

        private int getInt(string value)
        {
            int val;
            if (!int.TryParse(value, out val)) return 0;
            return val;
        }

        private Color getColour(string s)
        {
            if (0 == s.Length) return Color.Empty;
            return Color.FromArgb(getInt(s));
        }
        public void save()
        {
            if (null == doc) throw new Exception();

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/options/LBADir");
            nodes[0].InnerText = LBADir;

            doc.DocumentElement.SelectNodes("/options/interval")[0].InnerText = interval;
            doc.DocumentElement.SelectNodes("/options/precisionDigits")[0].InnerText = precisionDigits;
            doc.DocumentElement.SelectNodes("/options/disableAutoZoom")[0].InnerText = disableAutoZoom.ToString();
            doc.DocumentElement.SelectNodes("/options/defaultInventorySquare")[0].InnerText = defaultInventorySquare.ToString();
            doc.DocumentElement.SelectNodes("/options/deleteSaves")[0].InnerText = deleteSaves.ToString();
            doc.DocumentElement.SelectNodes("/options/autoSaveSplits")[0].InnerText = autoSaveSplits.ToString();
            doc.DocumentElement.SelectNodes("/options/showSubArea")[0].InnerText = showSubArea.ToString();
            doc.DocumentElement.SelectNodes("/options/showMessageBoxOnRunEnd")[0].InnerText = showMessageBoxOnRunEnd.ToString();
            doc.DocumentElement.SelectNodes("/options/alwaysOnTop")[0].InnerText = alwaysOnTop.ToString();
            doc.DocumentElement.SelectNodes("/options/transparentBackground")[0].InnerText = transparentBackground.ToString();
            doc.DocumentElement.SelectNodes("/options/autoReset")[0].InnerText = autoReset.ToString();
            doc.DocumentElement.SelectNodes("/options/backgroundColour")[0].InnerText = backgroundColour.ToArgb().ToString();
            doc.DocumentElement.SelectNodes("/options/foreColour")[0].InnerText = foreColour.ToArgb().ToString();

            doc.DocumentElement.SelectNodes("/options/currentRouteFile")[0].InnerText = routeFilePath;
            doc.DocumentElement.SelectNodes("/options/startTimeDelay")[0].InnerText = startTimeDelay;

            doc.DocumentElement.SelectNodes("/options/saveColumnWidths")[0].InnerText = saveColumnWidths.ToString();
            doc.DocumentElement.SelectNodes("/options/mainLVColumnWidths")[0].SelectNodes("areaName")[0].InnerText = mainLVColumnWidths.areaName;
            doc.DocumentElement.SelectNodes("/options/mainLVColumnWidths")[0].SelectNodes("differenceBest")[0].InnerText = mainLVColumnWidths.differenceBest;
            doc.DocumentElement.SelectNodes("/options/mainLVColumnWidths")[0].SelectNodes("differencePB")[0].InnerText = mainLVColumnWidths.differencePB;
            doc.DocumentElement.SelectNodes("/options/mainLVColumnWidths")[0].SelectNodes("time")[0].InnerText = mainLVColumnWidths.time;
            doc.Save(path);
        }

        private XmlNode addWindowInf(XmlNode xn, WindowInf wiInf)
        {
            xn.SelectNodes("positionX")[0].InnerText = wiInf.x.ToString();
            xn.SelectNodes("positionY")[0].InnerText = wiInf.y.ToString();
            xn.SelectNodes("width")[0].InnerText = wiInf.width.ToString();
            xn.SelectNodes("height")[0].InnerText = wiInf.height.ToString();
            xn.SelectNodes("alwaysOnTop")[0].InnerText = wiInf.alwaysOnTop.ToString();
            return xn;
        }
    }

    public class MainLVColumnWidths
    {
        public string areaName;
        public string differenceBest;
        public string differencePB;
        public string time;
    }

    public class WindowInf
    {
        public int x;
        public int y;
        public int height;
        public int width;
        public bool alwaysOnTop;
    }
}
