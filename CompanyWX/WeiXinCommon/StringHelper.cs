using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRX.Common
{
   public  class StringHelper
    {
       public static string todate(DateTime time)
       {
           return Convert.ToDateTime(time).ToString("yyyy-MM-dd hh:mm");
       }

       public static string format(string text) {
          
           text = text.Replace(@"\5b8b\4f53", "宋体");
           text = text.Replace(@"\534e\6587\65b0\9b4f", "华文新魏");
           text = text.Replace(@"\5e7c\5706", "幼圆");
           text = text.Replace(@"\9ed1\4f53", "黑体");
           text = text.Replace(@"\65b0\5b8b\4f53", "新宋体");
           text = text.Replace(@"\4eff\5B8b", "仿宋");
           text = text.Replace(@"\6977\4f53", "楷体");
           text = text.Replace(@"\5fae\x8f6f\6b63\9ed1\4f53", "微软正黑体");
           text = text.Replace(@"\5fae\8f6f\96c5\9ed1", "微软雅黑");
           return text;
       }
    }
}
