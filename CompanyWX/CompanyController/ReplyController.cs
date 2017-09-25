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
    public class ReplyController : Controller
    {
        private IReplyService replyservice = ReplyService.getInstance();
        private IMaterialService materialservice = MaterialService.getInstance();
        public ActionResult Index(int type, int page = 1, int pageSize = 10, string title="")
        {

            int totalCount = 0;
            string sql ="type=" + type;
            if (title != "") {
                sql += " and title like '%" + title + "%'";
            }

            var list = replyservice.GetListByPage(sql, "", page, pageSize, out totalCount);
            ViewBag.recordcount = totalCount;
            ViewBag.pageindex = page;
            ViewBag.pagecount = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            if (type == Types.Text)
            {
                ViewBag.url = "/Reply/AddText";
            }
            else if (type == Types.ImageText)
            {
                ViewBag.url = "/Reply/AddImageText";
            }

            ViewBag.type = type;
            ViewBag.list = list;
            return View();
        }

        public ActionResult AddText(string id)
        {
            if (id == null)
            {
                ViewBag.model = null;
            }
            else
            {
                ViewBag.model = replyservice.GetModel(int.Parse(id));
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddReply(Reply reply)
        {

            var list = replyservice.GetList("keys='" + reply.keys + "'");
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            if (reply.id == 0)
            {
                if (list.Count > 0)
                {
                    resultInfo.Result = false;
                    resultInfo.Msg = "关键词已存在";
                }
                else
                {
                    reply.createtime = DateTime.Now;
                    int num = replyservice.Insert(reply);
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
                }
            }
            else
            {
                if (list.Count > 1)
                {
                    resultInfo.Result = false;
                    resultInfo.Msg = "关键词已存在";
                }
                else
                {
                    reply.createtime = DateTime.Now;
                    int num = replyservice.Update(reply);
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
                }
            }

            return Json(resultInfo);

        }
        public ActionResult ImageText()
        {

            return View();
        }
        public ActionResult AddImageText(int page = 1, int pageSize = 10,string title="")
        {
            int totalCount = 0;
            string where = "type=" + Types.ImageText;
            if (title != "") {
                where += " and title like '%" + title + "%'";
                 page = 1;
                 pageSize = 10;
            }
            var imgtextlist = materialservice.GetListByPage(where, "", page, pageSize, out totalCount);
            foreach (var item in imgtextlist)
            {
                if (item.list != null)
                {

                    item.mylist = JsonHelper.DeserializeJsonToList<Material>(StringHelper.format(item.list));
                }

            }
            ViewBag.recordcount = totalCount;
            ViewBag.pageindex = page;
            ViewBag.pagecount = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            ViewBag.imgtextlist = imgtextlist;
            return View();
        }

        [HttpGet]
        public ActionResult EditImageText(int gid, int page = 1, int pageSize = 10, string title = "")
        {
            var obj = replyservice.GetModel(gid);
            int totalCount = 0;
            string where = "type=" + Types.ImageText;
            if (title != "")
            {
                where += " and title like '%" + title + "%'";
                page = 1;
                pageSize = 10;
            }
            var imgtextlist = materialservice.GetListByPage(where, "", page, pageSize, out totalCount);
            foreach (var item in imgtextlist)
            {
                if (item.list != null)
                {
                    item.mylist = JsonHelper.DeserializeJsonToList<Material>(StringHelper.format(item.list));
                }

            }
            ViewBag.recordcount = totalCount;
            ViewBag.pageindex = page;
            ViewBag.pagecount = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            ViewBag.imgtextlist = imgtextlist;
            ViewBag.obj = obj;
            ViewBag.gid = gid;
            return View();
        }
        [HttpPost]
        public ActionResult EditImageText(Reply reply)
        {
            reply.createtime = DateTime.Now;
           int num = replyservice.Update(reply);
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
        public ActionResult DelReply(string ids)
        {
            ResultJsonInfo resultInfo = new ResultJsonInfo();
            int num = replyservice.DeleteList(ids);
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
    }
}
