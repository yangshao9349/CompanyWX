using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WeiXinCommon
{
    public class LogTextHelper
    {
        const string directory = "C:\\LocalLog";
        public static void Error(string msg)
        {
            StreamWriter file;
            string errorpath = directory + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "_error.log";
            if (!File.Exists(errorpath))
            {
                Directory.CreateDirectory(directory);
                file = File.CreateText(errorpath);
            }
            else
                file = new StreamWriter(errorpath, true);
            file.WriteLine(string.Format("{0}:" + Environment.NewLine + "{1}", DateTime.Now.ToString(), msg));
            file.Close();
        }
    }
}
