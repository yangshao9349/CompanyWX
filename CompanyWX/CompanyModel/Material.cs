using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyModel
{
    public class Material
    {
        public int id { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public int type { get; set; }
        public string topimg { get; set; }


        public string author {
            get;
            set;
        }
     
        public string url { get; set; }
        public string summary { get; set; }
        public string annex { get; set; } //附件
        public DateTime  createtime { get; set; }

        public string list { get; set; }

        public List<Material> mylist { get; set; }
        public string filename { get; set; }

       
    }

  
}
