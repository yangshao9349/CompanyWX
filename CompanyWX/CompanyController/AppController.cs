using Bert.lin.Common;
using CompanyIService;
using CompanyModel;
using CompanyService;
using SRX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CompanyController
{
     public  class AppController : Controller
    {
         private IMaterialService materialservice = MaterialService.getInstance();
         public ActionResult Index(int resid,int index) {

             var obj =materialservice.GetModel(resid);
             string text = StringHelper.format(obj.list);
             obj.mylist = JsonHelper.DeserializeJsonToList<Material>(text);
             var model = obj.mylist[index];
             ViewBag.obj = obj;
             ViewBag.model = model;
             return View();
         }
    }
}
