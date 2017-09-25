using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinCommon
{
   public  class MessageBaseResponse
    {
        // 开发者微信号    
        private String ToUserName;
        // 发送方账号（一个OpenID）    
        private String FromUserName;
        // 消息创建时间 （整型）    
        private long CreateTime;
        // 消息类型（text/image/location/link/voice）   
        private String MsgType;

        public String getFromUserName()
        {
            return FromUserName;
        }

        public void setFromUserName(String fromUserName)
        {
            FromUserName = fromUserName;
        }

        public long getCreateTime()
        {
            return CreateTime;
        }

        public void setCreateTime(long createTime)
        {
            CreateTime = createTime;
        }

        public String getMsgType()
        {
            return MsgType;
        }

        public void setMsgType(String msgType)
        {
            MsgType = msgType;
        }

        public String getToUserName()
        {
            return ToUserName;
        }

        public void setToUserName(String toUserName)
        {
            ToUserName = toUserName;
        }

    }
}
