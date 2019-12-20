using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace LBAAutoSplitter
{
    class Locations
    {
        /**
        * An object to provide a wrapper to the Locations XML file
        */
        
        public string[] islandName;
        public string[,]subArea;
        private XmlDocument doc;
        
        private string path;

        public Locations()
        {
            //Get file path
            path = AppDomain.CurrentDomain.BaseDirectory + "files\\areacode.xml";
            //path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath + "\\files\\areacode.xml";
            doc = new XmlDocument();            
            doc.Load(path);

            readIslandNames();
            getSubAreaFromIsland(islandName[0]);
        }
        private void readIslandNames()
        {
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/lba1/island");
            islandName = new string[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
                islandName[i] = nodes[i].Attributes["name"].Value;                
        }

        public void getSubAreaFromIsland(string islandName)
        {
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/lba1/island[@name='" + islandName +"']/area");
            subArea = new string[nodes.Count,2];
            for(int i=0; nodes.Count > i; i++)
            {
                subArea[i, 0] = nodes[i].Attributes["id"].Value;
                subArea[i, 1] = nodes[i].InnerText.Trim();
            }          
        }

        public string getIslandNameFromCode(string code)
        {            
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/lba1/island/area[@id='" + code + "']");
            XmlNode parent = nodes[0].ParentNode;
            return parent.Attributes["name"].Value;
        }
        public string getAreaNameFromCode(string code)
        {
           return doc.DocumentElement.SelectNodes("/lba1/island/area[@id='" + code + "']")[0].InnerText;
        }
    }
}
