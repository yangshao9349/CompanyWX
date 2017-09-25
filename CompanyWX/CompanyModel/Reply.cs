using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyModel
{
    public class Reply
    {
        public int id { get; set; }
        public string keys { get; set; }
        public string contents { get; set; }
        public DateTime createtime { get; set; }
        public int type { get; set; }
        public int resid { get; set; }
        public int searchtype { get; set; }

        public string title { get; set; }


    }
}
