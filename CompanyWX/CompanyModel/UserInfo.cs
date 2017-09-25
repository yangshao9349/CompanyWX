using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyModel
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string username { get; set; }

        public string PassWord { get; set; }

        public string CreateTime { get; set; }

        public string UpdateTime { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int UserNums { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public string LastLoginTime { get; set; }
        /// <summary>
        ///账号当前状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 账号当前类型
        /// </summary>
        public int Types { get; set; }
    }
}
