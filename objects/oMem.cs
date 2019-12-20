using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace LBAAutoSplitter
{
    class Mem
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, uint lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(int hProcess, uint lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        const int PROCESS_WM_READ = 0x0010;
        Process proc;
        IntPtr processHandle;
        private uint baseAddress = 0;
        byte LBAVersion = 0;

        public Mem()
        {
            OpenProcess(PROCESS_ALL_ACCESS);
        }
        public uint getBaseAddress()
        {
            if (0 != baseAddress) return baseAddress;
            uint readAddr;
            string baseString;
            byte lbaVer = DetectLBAVersion();
            if (0 == lbaVer) return 0;
            if (1 == lbaVer)
            {
                //baseString = "Relent";
                //readAddr = 0x0A000FC8;//Base address to start scanning from
                baseString = "tempa.tmp";
                readAddr = 0x0A000C81;
            }
            else
            {
                baseString = "Run-Time system.";
                readAddr = 0x0A00003D;
            }

            byte[] b = new byte[baseString.Length];

            for (int i = 0; i <= 0xFFFFF; readAddr += 0x1000, i++)
            {
                int bytesRead = 0;

                ReadProcessMemory((int)processHandle, readAddr, b, b.Length, ref bytesRead);
                if (baseString == System.Text.Encoding.UTF7.GetString(b).Trim())
                {
                    if (1 == lbaVer) readAddr -= 0xCB9;
                    baseAddress = readAddr;
                    return readAddr;
                }
            }
            return 0;
        }
        #region writeMemory
        private int writeProcess(uint addressToWrite, byte[] buffer, ushort size)
        {
            int bytesWritten = 0;
            WriteProcessMemory((int)processHandle, addressToWrite, buffer, size, ref bytesWritten);
            return bytesWritten;
        }
        /*public bool writeAddress(byte LBAVer, Item item, byte[] bytes)
        {
            if (DetectLBAVersion() != LBAVer) return false;
            uint addressToWrite = (uint)item.memoryOffset;
            uint baseAddr = getBaseAddress();
            if (0 == baseAddr)
                return false;
            else
                addressToWrite += baseAddr;
            return (!(0 >= writeProcess(addressToWrite, bytes, item.size)));
        }*/
        /*public bool WriteVal(byte LBAVer, Item itm, string value)
        {
            ushort val;
            if (!ushort.TryParse(value, out val)) return false;
            if (val > itm.maxVal) val = itm.maxVal;
            if (val < itm.minVal) val = itm.minVal;
            WriteVal(LBAVer, itm, val);
            return true;
        }*/
        /*public void WriteVal(byte LBAVer, Item itm, ushort val)
        {
            writeAddress(LBAVer, itm, BitConverter.GetBytes(val));
        }*/
        public void WriteVal(byte LBAVer, int offset, ushort val, ushort size)
        {
            if (DetectLBAVersion() != LBAVer) return;
            writeProcess((uint)(getBaseAddress() + offset), BitConverter.GetBytes(val), size);
        }

        public void WriteVal(int offset, ushort data, byte size)
        {
            WriteVal(DetectLBAVersion(), offset, data, size);
        }
        #endregion
        #region readMemory
        public bool readProcess(uint addressToRead, ref byte[] data)
        {
            try
            {
                int bytesRead = 0;
                return ReadProcessMemory((int)processHandle, addressToRead, data, data.Length, ref bytesRead);
            }
            catch { }
            return false;
        }


        public int readAddress(byte LBAVer, uint offsetToRead, uint size)
        {
            if (DetectLBAVersion() != LBAVer) return -1;
            uint addressToRead = 0;
            byte[] bytes = new byte[size];
            uint baseAddr = getBaseAddress();
            if (0 == baseAddr)
                return -1;
            else
                addressToRead = (uint)(offsetToRead + baseAddr);
            if (readProcess(addressToRead, ref bytes))
            {
                if (1 == size)
                    return bytes[0];
                return BitConverter.ToInt16(bytes, 0);
            }
            return -1;
        }
        /*public int getVal(byte LBAVer, Item itm)
        {
            if (null == itm) return -1;
            return readAddress(LBAVer, itm.memoryOffset, itm.size);
        }*/
        /*public int readVal(uint offsetToRead, uint size)
        {
            return readAddress(DetectLBAVersion(), offsetToRead, size);
        }*/
        /*public string getString(byte LBAVer, uint startOffset)
        {
            string sVal = "";
            int iVal;
            for (int i = 0; 0 != (iVal = readAddress(LBAVer, startOffset++, 1)); i++)
                if (-1 == iVal)
                    return null;
                else
                    sVal += Char.ConvertFromUtf32(iVal);
            return sVal;
        }*/

        //This reads bytes until a null character is encountered
        /*public byte[] getByteArrayNull(byte LBAVer, uint startOffset)
        {
            string s;
            if (null == (s = getString(LBAVer, startOffset))) return null;
            byte[] t = Encoding.UTF8.GetBytes(s);
            byte[] b = new byte[t.Length + 1];
            for (byte i = 0; i < t.Length; i++) b[i] = t[i];
            b[b.Length - 1] = 0;
            return b;
        }*/
        /*public byte[] getByteArray(ushort LBAVer, uint startOffset, ushort size)
        {
            if (DetectLBAVersion() != LBAVer) return null;
            byte[] b = new byte[size];
            readProcess(getBaseAddress() + startOffset, ref b);
            return b;
        }*/
        #endregion
        //Assigns to proc and ProcessHandle
        private void OpenProcess(int access)
        {
            Process[] p;
            p = Process.GetProcessesByName("DOSBox");
            if (1 != p.Length) return;
            proc = p[0];
            processHandle = OpenProcess(access, false, proc.Id); ;
        }

        //Returns 1 for LBA1, 2 for LBA2, or 0 if not found.
        public byte DetectLBAVersion()
        {
            if (0 == LBAVersion)
            {
                Process[] p = Process.GetProcessesByName("DOSBox");
                if (1 != p.Length) return 0;
                string winTitle = Process.GetProcessesByName("DOSBox")[0].MainWindowTitle;
                if (winTitle.Contains("RELENT")) LBAVersion = 1;
                if (winTitle.Contains("LBA2")) LBAVersion = 2;
            }
            return LBAVersion;
        }
    }

}

#region ItemObject
/*public class Item
{
    public const ushort TYPE_BITFLAG = 0;
    public const ushort TYPE_VALUE = 1;
    public string name;
    public uint memoryOffset;
    public ushort maxVal;
    public ushort minVal;
    public ushort size; //Number of bytes needed to store value
    public ushort type;
    public byte lbaVersion; //1 for LBA1, or 2 for LBA2

    public override string ToString()
    {
        return name;
    }
}*/
#endregion
