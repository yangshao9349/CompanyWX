using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRX.WeiXinCommon
{
    public class AccessTokenAPI
    {
        public string access_token
        {
            get;
            set;
        }

        public string expires_in
        {
            get;
            set;
        }

        public AccessTokenAPI() { }
    }

    public class Oauth2API
    {
        public string access_token
        {
            get;
            set;
        }

        public string expires_in
        {
            get;
            set;
        }

        public string refresh_token
        {
            get;
            set;
        }

        public string openid
        {
            get;
            set;
        }

        public string scope
        {
            get;
            set;
        }

        public Oauth2API() { }
    }

    /// <summary>
    /// 根据code获取成员信息
    /// </summary>
    public class OAuth2UserInfoAPI
    {
        public OAuth2UserInfoAPI() { }

        public string UserId { get; set; }
        public string OpenId { get; set; }
        public string DeviceId { get; set; }

    }

    /// <summary>
    /// 成员信息
    /// </summary>
    public class UserInfo
    {
        public UserInfo() { }
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string userid { get; set; }
        public string name { get; set; }
        public string[] department { get; set; }
        public string position { get; set; }
        public string mobile { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string weixinid { get; set; }
        public string avatar { get; set; }
        public string avatar_mediaid { get; set; }
        public string status { get; set; }
        //public List<attrs> extattr { get; set; }

    }

    public class attrs
    {
        public string name { get; set; }
        public string value { get; set; }
    }

}
