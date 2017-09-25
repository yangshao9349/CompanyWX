using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace CompanyWX.Ashx
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class upload : IHttpHandler
    {
        private HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //指定文件保存目录
            string getPath = context.Request.QueryString["p"] ;
            //文件保存目录路径
            string savePath = "/upload/" + getPath + "/";
            //文件保存目录URL
            string saveUrl = "http://" + context.Request.Url.Authority + "/upload/" + getPath + "/";
            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,amr,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2,css");
            //最大文件大小
            string dirName = context.Request.QueryString["p"];
            int maxSize = 1000000;

            if (dirName == "video")
            {
                maxSize = 1000000*20;
            }
            this.context = context;

            HttpPostedFile imgFile = context.Request.Files["imgFile"];

            if (imgFile == null)
            {
                showError("请选择文件。");
            }

            string dirPath = context.Server.MapPath(savePath);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }


            //if (string.IsNullOrEmpty(dirName))
            //{
            //    dirName = "image";
            //}

            //if (!extTable.ContainsKey(dirName))
            //{
            //    showError("目录名不正确。");
            //}

            string fileName = imgFile.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (string.IsNullOrEmpty(fileExt))
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((string)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            saveUrl += dirName + "/";

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string ymdStr = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymdStr + "/";
            saveUrl += ymdStr + "/";

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            string filePath = dirPath + newFileName;

            imgFile.SaveAs(filePath);

            string fileUrl = saveUrl + newFileName;

            Hashtable hashMsg = new Hashtable();
            hashMsg["error"] = 0;
            hashMsg["url"] = fileUrl;
            hashMsg["filename"] = fileName;
            

            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");

            //转相对路径
            //2013-11-29 11:17:46 谢俊
            //if (!string.IsNullOrEmpty(context.Request["relative"]))
            //{
            //    var relativeUrl = hashMsg["url"].ToString().Substring(0, hashMsg["url"].ToString().IndexOf("upload"));
            //    hashMsg["url"] = hashMsg["url"].ToString().Replace(relativeUrl, "../../../");
            //}

            context.Response.Write(JsonMapper.ToJson(hashMsg));
            context.Response.End();
        }

        private void showError(string messageStr)
        {
            Hashtable hashMsg = new Hashtable();
            hashMsg["error"] = 1;
            hashMsg["message"] = messageStr;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hashMsg));
            context.Response.End();
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