
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PDF_TML
{

    class Ini
    {

        
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(string ApplicationName, string KeyName, string StrValue, string FileName);
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string ApplicationName, string KeyName, string DefaultValue, StringBuilder ReturnString, int nSize, string FileName);


        public static void WriteValue(string SectionName, string KeyName, string KeyValue, string FileName)
        {
            WritePrivateProfileString(SectionName, KeyName, KeyValue, FileName);
        }

        public static string ReadValue(string SectionName, string KeyName, string FileName)
        {
            StringBuilder szStr = new StringBuilder(100000);
            GetPrivateProfileString(SectionName, KeyName, "", szStr, 100000, FileName);
            return szStr.ToString().Trim();
        }
    }
}

    
