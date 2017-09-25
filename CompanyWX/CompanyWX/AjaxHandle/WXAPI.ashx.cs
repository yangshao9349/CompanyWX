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
using WeiXinCommon;

namespace SRX.OAWeb.AjaxHandle
{
    /// <summary>
    /// WXAPI 的摘要说明
    /// </summary>
    public class WXAPI : IHttpHandler
    {
        public string token = ConfigurationManager.AppSettings["WeixinToken"];//从配置文件获取Token
        public string encodingAESKey = ConfigurationManager.AppSettings["EncodingAESKey"];//从配置文件获取EncodingAESKey
        public string corpId = ConfigurationManager.AppSettings["CropID"];//从配置文件获取corpId
        public void ProcessRequest(HttpContext context)
        {
            string postString = string.Empty;
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);

                }

                if (!string.IsNullOrEmpty(postString))
                {

                    try
                    {
                        String sMsg = "";
                        WXBizMsgCrypt2 wxcpt = new WXBizMsgCrypt2(token, encodingAESKey, corpId);
                     //   WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(token, encodingAESKey, corpId);
                        String sVerifyMsgSig = context.Request.QueryString["msg_signature"];
                        String sVerifyTimeStamp = context.Request.QueryString["timestamp"];
                        String sVerifyNonce = context.Request.QueryString["nonce"];


                        
                        int ret = wxcpt.DecryptMsg(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, postString, ref sMsg);
                        LogTextHelper.Error("ret:" + ret + ",sMsg:" + sMsg);
                        if (ret == 0)
                        {

                            messageHelp help = new messageHelp();
                          
                            string responseContent = help.ReturnMessage(sMsg);
                            int k = wxcpt.EncryptMsg(responseContent, sVerifyTimeStamp, sVerifyNonce, ref sMsg);
                            LogTextHelper.Error("k:" + k);
                            if (k == 0)
                            {
                                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                                HttpContext.Current.Response.Write(sMsg);
                            }


                        }

                    }
                    catch (Exception ex)
                    {

                        LogTextHelper.Error(ex.ToString());
                    }

                }
            }
            else
            {
                Auth();
            }
        }


        private void Auth()
        {
            #region 获取关键参数

            if (string.IsNullOrEmpty(token))
            {
                LogTextHelper.Error(string.Format("CorpToken 配置项没有配置！"));
            }

            if (string.IsNullOrEmpty(encodingAESKey))
            {
                LogTextHelper.Error(string.Format("EncodingAESKey 配置项没有配置！"));
            }

            if (string.IsNullOrEmpty(corpId))
            {
                LogTextHelper.Error(string.Format("CorpId 配置项没有配置！"));
            }
            #endregion

            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["msg_signature"];//企业号的 msg_signature
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];

            string decryptEchoString = "";
            if (new CorpBasicApi().CheckSignature(token, signature, timestamp, nonce, corpId, encodingAESKey, echoString, ref decryptEchoString))
            {
                if (!string.IsNullOrEmpty(decryptEchoString))
                {
                    HttpContext.Current.Response.Write(decryptEchoString);
                    HttpContext.Current.Response.End();
                }
            }
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