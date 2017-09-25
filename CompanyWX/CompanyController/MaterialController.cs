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
using WeiXinCommon;

namespace CompanyController
{

    public class MaterialController : Controller
    {
        private IMaterialService materialservice = MaterialService.getInstance();
        public ActionResult Index(int page = 1, int pageSize = 10, string key = "")
        {

            int totalCount = 0;
            var list = materialservice.GetListByPage("type=" + Types.Text + " and contents like '%" + key + "%'", "", page, pageSize, out totalCount);


            ViewBag.recordcount = totalCount;
            ViewBag.pageindex = page;
            ViewBag.pagecount = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            ViewBag.list = list;
            return View();
        }
        //[HttpGet]
        //public ActionResult AddText()
        //{

        //    return View();
        //}
        [HttpGet]
        public ActionResult AddText(string id)
        {
            if (id == null)
            {
                //执行添加
                ViewBag.model = null;

            }
            else
            {
                //执行修改，获取对象
                ViewBag.model = materialservice.GetModel(int.Parse(id));

            }
            return View();

        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddText(Material material)
        {
            int num = 0;
            material.type = Types.Text;
            material.createtime = DateTime.Now;
            if (material.id == 0)
            {
                num = materialservice.Insert(material);
            }
            else
            {
                num = materialservice.Update(material);
            }
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                resultInfo.Result = true;
                resultInfo.Msg = "添加成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "添加失败";
            }
            return Json(resultInfo);
        }
        public ActionResult EditText()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DelText(string ids)
        {
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            int num = materialservice.DeleteList(ids);
            if (num > 0)
            {
                resultInfo.Result = true;
                resultInfo.Msg = "删除成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "删除失败";
            }
            return Json(resultInfo);
        }

        public ActionResult Pic(int page = 1, int pageSize = 10, string key = "")
        {

            int totalCount = 0;
            var list = materialservice.GetListByPage("type=" + Types.Pic + " and contents like '%" + key + "%'", "", page, pageSize, out totalCount);
            ViewBag.recordcount = totalCount;
            ViewBag.pageindex = page;
            ViewBag.pagecount = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            ViewBag.list = list;
            return View();
        }
        [HttpPost]
        public ActionResult AddPic(Material material)
        {

            material.type = Types.Pic;

            material.createtime = DateTime.Now;
            int num = materialservice.Insert(material);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                resultInfo.Result = true;
                resultInfo.Msg = "添加成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "添加失败";
            }
            return Json(resultInfo);

        }
        [HttpPost]
        public ActionResult DelPic(int id)
        {
            var model = materialservice.GetModel(id);
            int num = materialservice.Delete(id);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                System.IO.File.Delete(Server.MapPath(model.topimg));
                resultInfo.Result = true;
                resultInfo.Msg = "删除成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "删除失败";
            }
            return Json(resultInfo);

        }
        public ActionResult Voice(int page = 1, int pageSize = 10, string key = "")
        {
            int totalCount = 0;
            var list = materialservice.GetListByPage("type=" + Types.Voice + " and contents like '%" + key + "%'", "", page, pageSize, out totalCount);
            ViewBag.recordcount = totalCount;
            ViewBag.pageindex = page;
            ViewBag.pagecount = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            ViewBag.list = list;
            return View();
        }
        [HttpPost]
        public ActionResult AddVoice(Material material)
        {
            material.type = Types.Voice;
            material.createtime = DateTime.Now;
            int num = materialservice.Insert(material);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                resultInfo.Result = true;
                resultInfo.Msg = "添加成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "添加失败";
            }
            return Json(resultInfo);

        }
        [HttpPost]
        public ActionResult DelVoice(string ids)
        {

            ResultJsonInfo resultInfo = new ResultJsonInfo();
            int num = materialservice.DeleteList(ids);
            if (num > 0)
            {
                resultInfo.Result = true;
                resultInfo.Msg = "删除成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "删除失败";
            }
            return Json(resultInfo);
        }
        public ActionResult Video(int page = 1, int pageSize = 10, string key = "")
        {
            int totalCount = 0;
            var list = materialservice.GetListByPage("type=" + Types.Video + " and contents like '%" + key + "%'", "", page, pageSize, out totalCount);
            ViewBag.recordcount = totalCount;
            ViewBag.pageindex = page;
            ViewBag.pagecount = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            ViewBag.list = list;
            return View();
        }

        [HttpGet]
        public ActionResult AddVideo()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddVideo(Material material)
        {

            material.type = Types.Video;
            material.createtime = DateTime.Now;
            int num = materialservice.Insert(material);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                resultInfo.Result = true;
                resultInfo.Msg = "添加成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "添加失败";
            }
            return Json(resultInfo);
        }
        public ActionResult ImageText(int page = 1, int pageSize = 10, string key = "")
        {
            int totalCount = 0;
            var list = materialservice.GetListByPage("type=" + Types.ImageText + " and contents like '%" + key + "%'", "", page, pageSize, out totalCount);
          
            foreach (var item in list)
            {
                if (item.list != null)
                {
                    item.mylist = JsonHelper.DeserializeJsonToList<Material>(StringHelper.format(item.list));
                }

            }
            ViewBag.recordcount = totalCount;
            ViewBag.pageindex = page;
            ViewBag.pagecount = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            ViewBag.list = list;
            return View();
        }
        public ActionResult AddImageText()
        {
         
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddImageText(Material material)
        {

            
            material.type = Types.ImageText;
            material.createtime = DateTime.Now;
         
   
          
            int num = materialservice.Insert(material);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                resultInfo.Result = true;
                resultInfo.Msg = "添加成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "添加失败";
            }
            return Json(resultInfo);
        }
        public ActionResult EditImageText(int id) {

            var obj = materialservice.GetModel(id);
       
            if (obj.list != null)
            {
                obj.mylist = JsonHelper.DeserializeJsonToList<Material>(StringHelper.format(obj.list));
            }
            ViewBag.obj = obj;
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditImageText(Material material) {
          

            material.createtime = DateTime.Now;
            material.type = Types.ImageText;
            int num = materialservice.Update(material);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                resultInfo.Result = true;
                resultInfo.Msg = "修改成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "修改失败";
            }
            return Json(resultInfo);
        }
        [HttpPost]
        public ActionResult DelImageText(int id)
        {
            var model = materialservice.GetModel(id);
            int num = materialservice.Delete(id);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
               // System.IO.File.Delete(Server.MapPath(model.topimg));
                resultInfo.Result = true;
                resultInfo.Msg = "删除成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "删除失败";
            }
            return Json(resultInfo);

        }

        public ActionResult File(int page = 1, int pageSize = 10, string key = "")
        {
            int totalCount = 0;
            var list = materialservice.GetListByPage("type=" + Types.File + " and contents like '%" + key + "%'", "", page, pageSize, out totalCount);
            ViewBag.recordcount = totalCount;
            ViewBag.pageindex = page;
            ViewBag.pagecount = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            ViewBag.list = list;
            return View();
        }
        [HttpPost]
        public ActionResult DelFile(int id)
        {
            var model = materialservice.GetModel(id);
            int num = materialservice.Delete(id);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                System.IO.File.Delete(Server.MapPath(model.topimg));
                resultInfo.Result = true;
                resultInfo.Msg = "删除成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "删除失败";
            }
            return Json(resultInfo);
        }
        [HttpPost]
        public ActionResult AddFile(Material material)
        {
            material.type = Types.File;
            material.createtime = DateTime.Now;
            int num = materialservice.Insert(material);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                resultInfo.Result = true;
                resultInfo.Msg = "添加成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "添加失败";
            }
            return Json(resultInfo);

        }
        public ActionResult Url(int pageindex = 1, int pagesize = 20)
        {
            int totalCount = 0;
            var list = materialservice.GetListByPage("type=" + Types.Url, "", pageindex, pagesize, out totalCount);
            foreach (var item in list)
            {
                if (item.list != null)
                {
                    item.mylist = JsonHelper.DeserializeJsonToList<Material>(item.list);
                }

            }
            ViewBag.list = list;
            return View();
        }

        [HttpGet]
        public ActionResult AddUrl()
        {


            return View();
        }
        [HttpPost]
        public ActionResult AddUrl(Material material)
        {

            material.type = Types.Url;
            material.createtime = DateTime.Now;
            int num = materialservice.Insert(material);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                resultInfo.Result = true;
                resultInfo.Msg = "添加成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "添加失败";
            }
            return Json(resultInfo);
        }
        [HttpPost]
        public ActionResult DelUrl(int id)
        {
            var model = materialservice.GetModel(id);
            int num = materialservice.Delete(id);
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (num > 0)
            {
                System.IO.File.Delete(Server.MapPath(model.topimg));
                resultInfo.Result = true;
                resultInfo.Msg = "删除成功";
            }
            else
            {
                resultInfo.Result = false;
                resultInfo.Msg = "删除失败";
            }
            return Json(resultInfo);
        }

    }
}
