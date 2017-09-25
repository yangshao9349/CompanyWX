using CompanyIService;
using CompanyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WeiXinCommon;

namespace CompanyController
{
    public class HomeController : Controller
    {

        private IUserInfoService userinfoservice = UserInfoService.getInstance();

        [HttpGet]
        public ActionResult Index()
        {


            return View();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckLogin(string username, string password)
        {

            if (userinfoservice.CheckAccount(username))
            {
                var list = userinfoservice.CheckLogin(username, password);
                if (list.Count > 0)
                {
                    SessionHelp.SetSession("UID", list[0].Id.ToString());
                    //修改用户相关信息
                    var model = list[0];
                    model.LastLoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.UserNums = model.UserNums != null ? model.UserNums + 1 : 1;
                    if (userinfoservice.Update(model) > 0)
                    {
                        return Json(1);
                    }
                    else
                    {
                        return Json(2);
                    }

                }
                else
                {

                    return Json(0);
                }
            }
            else
            {

                return Json(-1);
            }
        }
        [HttpGet]
        public ActionResult UpdatePassWord()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdatePassWord(string oldPass, string newPass)
        {
            var id = SessionHelp.GetSession("UID");
            if (id == "")
            {
                //当前账号信息已失效，提示用户重新登录
                return Json(-2);
            }
            else
            {
                //对比新旧密码
                var model = userinfoservice.GetModel(int.Parse(id));
                if (model.PassWord == oldPass)
                {
                    model.PassWord = newPass;
                    model.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //修改
                    if (userinfoservice.UpdatePassWord(model) > 0)
                    {
                        //修改成功
                        return Json(0);
                    }
                    else
                    {
                        //发生未知错误
                        return Json(1);
                    }
                }
                else
                {
                    //旧密码输入有误
                    return Json(-1);
                }
            }
           
        }
    }
}
