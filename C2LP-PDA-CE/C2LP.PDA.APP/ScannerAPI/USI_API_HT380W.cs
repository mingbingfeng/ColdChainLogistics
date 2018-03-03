using System;
using System.Runtime.InteropServices;
using System.Text;

namespace C2LP.PDA.APP.ScannerAPI
{
    public class USI_API_HT380W
    {
        [DllImport("Coredll.dll", EntryPoint = "GetTickCount")]
        public static extern int GetTickCount();

        [DllImport("CE380.dll", CharSet = CharSet.Auto, EntryPoint = "Barcode1D_init")]
        public static extern bool Barcode1D_init();

        [DllImport("CE380.dll", CharSet = CharSet.Auto, EntryPoint = "Barcode1D_scan")]
        public static extern int Barcode1D_scan(byte[] pszData);

        [DllImport("CE380.dll", CharSet = CharSet.Auto, EntryPoint = "Barcode1D_free")]
        public static extern void Barcode1D_free();

        public static string GetScanData()
        {
            int i_startTime, i_endTime;
            byte[] pszData = new byte[500];

            i_startTime = GetTickCount();
            int iRes = Barcode1D_scan(pszData);
            i_endTime = GetTickCount();
            return HandleShowData(true, pszData, iRes, i_endTime - i_startTime);
        }

        private static string HandleShowData(bool isSingle, byte[] bData, int len, int runtime)
        {
            string barData = "";

            try
            {
                if (len > 0)
                {
                    //barData = System.Text.Encoding.Default.GetString(bData, 0, len).Trim();
                    //barData = System.Text.Encoding.GetEncoding("GB2312").GetString(bData, 0, len).Trim();
                    barData = GetText(bData, len);
                    SystemDll.MessageBeep(-1);
                }
                
            }
            catch
            {

            }
            return barData.Trim();
        }

        private static string GetText(byte[] buff, int len)
        {
            string strReslut = string.Empty;
            if (buff.Length > 3)
            {
                if (buff[0] == 239 && buff[1] == 187 && buff[2] == 191)
                {// utf-8  
                    strReslut = Encoding.UTF8.GetString(buff, 0, len);
                }
                else if (buff[0] == 254 && buff[1] == 255)
                {// big endian unicode  
                    strReslut = Encoding.BigEndianUnicode.GetString(buff, 0, len);
                }
                else if (buff[0] == 255 && buff[1] == 254)
                {// unicode  
                    strReslut = Encoding.Unicode.GetString(buff, 0, len);
                }
                else if (isUtf8(buff))
                {// utf-8  
                    strReslut = Encoding.UTF8.GetString(buff, 0, len);
                }
                else
                {// ansi  
                    strReslut = Encoding.Default.GetString(buff, 0, len);
                }
            }

            return strReslut;
        }

        private static bool isUtf8(byte[] buff)
        {
            for (int i = 0; i < buff.Length; i++)
            {
                if ((buff[i] & 0xE0) == 0xC0)    // 110x xxxx 10xx xxxx  
                {
                    if ((buff[i + 1] & 0x80) != 0x80)
                    {
                        return false;
                    }
                }
                else if ((buff[i] & 0xF0) == 0xE0)  // 1110 xxxx 10xx xxxx 10xx xxxx  
                {
                    if ((buff[i + 1] & 0x80) != 0x80 || (buff[i + 2] & 0x80) != 0x80)
                    {
                        return false;
                    }
                }
                else if ((buff[i] & 0xF8) == 0xF0)  // 1111 0xxx 10xx xxxx 10xx xxxx 10xx xxxx  
                {
                    if ((buff[i + 1] & 0x80) != 0x80 || (buff[i + 2] & 0x80) != 0x80 || (buff[i + 3] & 0x80) != 0x80)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
