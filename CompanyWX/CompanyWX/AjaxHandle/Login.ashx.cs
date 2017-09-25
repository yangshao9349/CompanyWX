using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace SRX.OAWeb.AjaxHandle
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler, IRequiresSessionState
    {

        HttpContext RequestStr = null;
        public void ProcessRequest(HttpContext context)
        {
            RequestStr = context;
            RequestStr.Response.ContentType = "text/plain";
            RequestStr.Response.Charset = "utf-8";
            String methodName = RequestStr.Request["method"];
            String name = context.Request["name"];
            Type type = this.GetType();
            MethodInfo method = type.GetMethod(methodName);

            if (method == null)
            {
                throw new Exception("method is null");
            }

            try
            {
                method.Invoke(this, null);
            }
            catch { }
        }
        public void UserLogin()
        {
            try
            {
                string username = HttpUtility.UrlDecode(RequestStr.Request.Form["UserName"]);
                string pwd = HttpUtility.UrlDecode((RequestStr.Request.Form["UserPwd"]));

               

            }
            catch
            {
                RequestStr.Response.Write(failure("登录异常！"));
            }

        }


        public string success(string str)
        {
            return "{\"statusCode\":\"200\", \"message\":\"" + str + "\"}";
        }
        public string failure(string str)
        {
            return "{\"statusCode\":\"300\", \"message\":\"" + str + "\"}";
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}