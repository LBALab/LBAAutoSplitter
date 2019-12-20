using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBAAutoSplitter
{
    //Is it still worth having this as a standalone object?
    class AreaCode
    {
        Mem mem = new Mem();
        string fullFilePath;
        const string TEMPPOSTFIX = ".temp";
        //const string TEMPPOSTFIX = ".temp";
        const int MAXFILESIZE = 500;    //Sanity check used when reading internal filename of LBA save file. If it's more than 500 it's either a REALLY
                                        //Long name, or it's not a LBA save file.
        private int fileTrueStart;      //The "true" file starts at length(filename) + 3 i.e. 03 filename null FF firstByte

        private const int OFFSET_AREACODE = 0xE10;

        public AreaCode(string fullFilePath)
        {
            this.fullFilePath = fullFilePath;
            string tempFilePath = fullFilePath + TEMPPOSTFIX;
            //Creating a copy should help prevent errors if the file is currently being written to
            if (File.Exists(fullFilePath))
                File.Copy(fullFilePath, tempFilePath, true);
            else
                return;
            FileStream fsStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

            if (MAXFILESIZE < new System.IO.FileInfo(tempFilePath).Length)
            {
                File.Delete(tempFilePath);
                fsStream.Dispose();
                return; //If the file is more than 500 bytes fail out                          
            }

            char b = (char)fsStream.ReadByte();//Read and discard the opening byte (03)
            int i;
            for (i = 0; (0 != b) && (i != MAXFILESIZE); i++)
                b = (char)fsStream.ReadByte();
            fileTrueStart = i + 2;  //We use this offset to read the file from as everything depends on the length of the internal filename

            fsStream.Close();
            fsStream.Dispose();
            File.Delete(tempFilePath);
        }

        public string GetAreaCodeMemory()
        {
            return mem.readAddress(1, OFFSET_AREACODE, 1).ToString();
        }

        public string GetAreaCodeFile()
        {
            string areaCode;
            string tempFilePath = fullFilePath + TEMPPOSTFIX;
            //Creating a copy should help prevent errors if the file is currently being written to
            //if the save file has been deleted do nothing
            if (!File.Exists(fullFilePath)) return null;
            File.Copy(fullFilePath, tempFilePath, true);
            FileStream fsStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
            //Read area
            fsStream.Position = 0x100 + (fileTrueStart - 1);
            areaCode = fsStream.ReadByte().ToString();
            fsStream.Close();
            fsStream.Dispose();
            File.Delete(tempFilePath);
            return areaCode;
        }
    }
}
