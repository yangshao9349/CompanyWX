using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinCommon
{
   public  class MessageTextResponse : MessageBaseResponse
    {
        private String Content;

        public String getContent()
        {
            return Content;
        }

        public void setContent(String content)
        {
            Content = content;
        }

    }
}
