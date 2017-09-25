using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRX.WeiXinCommon
{
    public class BasicApi
    {
        public static string GetAccessToken(string corpid, string corpsecret)
        {
            //正常情况下access_token有效期为7200秒,这里使用缓存设置短于这个时间即可
            string access_token = SRX.WeiXinCommon.MemoryCacheHelper.AddOrGetExisting<string>("access_token",
                new TimeSpan(0, 0, 7000), delegate()
                {
                    var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", corpid, corpsecret);

                    string jsonText = Common.HttpUtility.GetPageForGet(url);

                    AccessTokenAPI accesstoken = JsonConvert.DeserializeObject<AccessTokenAPI>(jsonText);

                    string token = accesstoken.access_token;
                    return token;
                }
            );

            return access_token;
        }

    }
}
