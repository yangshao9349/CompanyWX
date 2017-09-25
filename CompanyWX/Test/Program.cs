using Bert.lin.Common;
using CompanyModel;
using SRX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText(@"E:\taobao\c#\bs\CompanyWX1.1\CompanyWX\Test\test.txt");
            string str = StringHelper.format(text);
            System.IO.File.WriteAllText("c://1.txt", str);
         

            Console.WriteLine(str);
            Console.Read();

        }
       
    }
}
