using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WeiXinCommon
{
    public class SessionHelp
    {
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="sessionName">名称</param>
        /// <param name="value">值</param>
        /// <param name="minute">时间</param>
        public static void SetSession(string sessionName, string value, int minute = 20)
        {
            HttpContext.Current.Session[sessionName] = value;
            HttpContext.Current.Session.Timeout = minute;
        }
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="sessionName">名称</param>
        /// <returns></returns>
        public static string GetSession(string sessionName)
        {
            return HttpContext.Current.Session[sessionName].ToString();
        }
        public static void RemoveSession(string sessionName)
        {
            HttpContext.Current.Session.Remove(sessionName);
        }
    }
}
