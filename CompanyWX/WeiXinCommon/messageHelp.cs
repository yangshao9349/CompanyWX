using Bert.lin.Common;
using CompanyModel;
using SRX.Common;
using SRX.WeiXinCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Tencent;

namespace WeiXinCommon
{
    public class messageHelp
    {
        /// <summary>
        /// 接受/发送消息帮助类
        /// </summary>

        //返回消息
        public string ReturnMessage(string postStr)
        {

            string responseContent = "";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(new System.IO.MemoryStream(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(postStr)));

            XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
            XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
            XmlNode MsgType = xmldoc.SelectSingleNode("/xml/MsgType");



            if (MsgType != null)
            {
                switch (MsgType.InnerText)
                {
                    case "event":
                        responseContent = EventHandle(xmldoc);//事件处理
                        break;
                    case "text":
                        responseContent = TextHandle(xmldoc);//接受文本消息处理

                        break;
                    default:
                        break;
                }
            }
            return responseContent;
        }
        //事件
        public string EventHandle(XmlDocument xmldoc)
        {
            string responseContent = "";
            XmlNode Event = xmldoc.SelectSingleNode("/xml/Event");
            XmlNode EventKey = xmldoc.SelectSingleNode("/xml/EventKey");
            XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
            XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
            if (Event != null)
            {

                switch (Event.InnerText.ToUpper())
                {
                    case "CLICK":   //菜单单击事件
                        if (EventKey.InnerText.Equals("btn_YYZL"))//预约诊疗
                        {
                            responseContent = string.Format(ReplyType.Message_Text,
                                FromUserName.InnerText,
                                ToUserName.InnerText,
                                DateTime.Now.Ticks,
                                "请选择您要了解的医院。\n<a href='http://www.gui120.com/WeiXinApp/SelectHospital.aspx?FromUserName=" + FromUserName.InnerText + "'>选择医院</a>");
                        }
                        else if (EventKey.InnerText.Equals("btn_WWXW"))//问问小微
                        {
                            string text = "Hi，我是导诊人员小薇，请尝试以下操作：\n1、输入医院名称，直接查看医院主页\n2、输入医生姓名，快速查找医生";
                            responseContent = string.Format(ReplyType.Message_Text,
                                FromUserName.InnerText,
                                ToUserName.InnerText,
                                DateTime.Now.Ticks,
                                text);
                        }
                        else if (EventKey.InnerText.Equals("btn_WDXX"))//我的信息
                        {
                            responseContent = string.Format(ReplyType.Message_View,
                                FromUserName.InnerText,
                                ToUserName.InnerText,
                                DateTime.Now.Ticks,
                                "http://www.gui120.com/WeiXinApp/UserManage/Default.aspx?FromUserName=" + FromUserName.InnerText);
                        }
                        break;
                    case "SUBSCRIBE"://关注
                        responseContent = string.Format(ReplyType.Message_Text,
                               FromUserName.InnerText,
                               ToUserName.InnerText,
                               DateTime.Now.Ticks,
                               "欢迎您关注桂康在线，请选择您需要了解的医院。<a href='http://www.gui120.com/WeiXinApp/SelectHospital.aspx?FromUserName=" + FromUserName.InnerText + "'>选择医院</a>");
                        break;
                    case "UNSUBSCRIBE"://取消关注
                        break;

                }

            }
            return responseContent;
        }

        //接受文本消息
        public string TextHandle(XmlDocument xmldoc)
        {
            string responseContent = "";
            XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
            XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
            XmlNode Content = xmldoc.SelectSingleNode("/xml/Content");
            if (Content != null)
            {
                responseContent = new DataQuery().GetDatas(Content.InnerText, FromUserName.InnerText, ToUserName.InnerText);


            }

            return responseContent;
        }

        //写入日志
        public void WriteLog(string text)
        {
            StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(".") + "\\log.txt", true);
            sw.WriteLine(text);
            sw.Close();//写入
        }
    }

    //回复类型
    public class ReplyType
    {

        /// <summary>
        /// 普通文本消息
        /// </summary>
        public static string Message_Text
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[text]]></MsgType>
                            <Content><![CDATA[{3}]]></Content>
                            </xml>";
            }
        }
        /// <summary>
        /// 图文消息主体
        /// </summary>
        public static string Message_News_Main
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[news]]></MsgType>
                            <ArticleCount>{3}</ArticleCount>
                            <Articles>
                            {4}
                            </Articles>
                            </xml> ";
            }
        }
        /// <summary>
        /// 图文消息项
        /// </summary>
        public static string Message_News_Item
        {
            get
            {
                return @"<item>
                            <Title><![CDATA[{0}]]></Title> 
                            <Description><![CDATA[{1}]]></Description>
                            <PicUrl><![CDATA[{2}]]></PicUrl>
                            <Url><![CDATA[{3}]]></Url>
                            </item>";
            }
        }

        public static string Message_View
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[event]]></MsgType>
                            <Event><![CDATA[VIEW]]></Event>
                            <EventKey><![CDATA[{3}]]></EventKey>
                            </xml>";
            }
        }
    }

    public class DataQuery
    {
        public DataQuery() { }

        /// <summary>
        /// 获取检索数据：医院、医生
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public string GetDatas(string keyword, string FromUserName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1  *  from Reply ");
            strSql.Append(" where keys like '%" + keyword + "%'");
            LogTextHelper.Error(strSql.ToString());

            Reply reply = DBHelper.ExecuteSql_First<Reply>(strSql.ToString(), null);
            string content = "没有数据";
            if (reply != null)
            {
                content = reply.contents;
            }
            return content;
        }

        public string GetDatas(string keyword, string fromusername, string tousername)
        {

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("select top 1  *  from Reply ");
            strSql1.Append(" where keys='" + keyword + "'");


            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select top 1  *  from Reply ");
            strSql2.Append(" where keys like '%" + keyword + "%'");

            string content = "";
            Reply reply = DBHelper.ExecuteSql_First<Reply>(strSql1.ToString(), null);
       
            if (reply != null)
            {

               content= getcontent(reply, fromusername, tousername);
            }
            else
            {
                reply = DBHelper.ExecuteSql_First<Reply>(strSql2.ToString(), null);
           
                if (reply != null)
                {
                    content = getcontent(reply, fromusername, tousername);
                }
                else
                {

                    content = string.Format(ReplyType.Message_Text, tousername, fromusername, DateTime.Now.Ticks, "没有匹配的数据");
                }

            }
            return content;

        }

        private string getcontent(Reply reply, string fromusername, string tousername)
        {
            string content = "";
            if (reply.type == Types.Text)
            {
                content = string.Format(ReplyType.Message_Text,
                fromusername,
                tousername,
                DateTime.Now.Ticks,
                reply.contents);
            }
            else if (reply.type == Types.ImageText)
            {
                string sql = "select top 1 * from material where id =" + reply.resid;
                Material mat = DBHelper.ExecuteSql_First<Material>(sql, null);
                if (mat != null)
                {
                    if (mat.list != "")
                    {
                        mat.mylist = JsonHelper.DeserializeJsonToList<Material>(StringHelper.format(mat.list));
                        int ArticleCount = mat.mylist.Count;
                        string list = "";
                        int k = 0;
                        foreach (var item in mat.mylist)
                        {
                            list += string.Format(ReplyType.Message_News_Item,
                                item.title,
                                item.summary,
                                item.topimg,
                                ConfigurationManager.AppSettings["Url"].ToString() + "App/Index?resid=" + reply.resid + "&index=" + k);
                            k++;
                        }


                        content = string.Format(ReplyType.Message_News_Main,
                            tousername,
                            fromusername,
                            DateTime.Now.Ticks,
                            ArticleCount,
                            list);
                    }
                }


            }

            return content;

        }

    }
}
