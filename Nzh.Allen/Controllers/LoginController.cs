using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nzh.Allen.Common;
using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Allen.Controllers
{
    public class LoginController : Controller
    {
        public IHttpContextAccessor httpContextAccessor { get; set; }
        public IUserService UserService { get; set; }
        public ILogService LogService { get; set; }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAuthCode()
        {

            return File(new VerifyCode(HttpContext).GetVerifyCode(), @"image/Gif");
        }
        [HttpPost]
        public ActionResult LoginOn(string username, string password, string captcha)
        {
            LogModel logEntity = new LogModel();
            var OperatorProvider = new OperatorProvider(HttpContext);
            logEntity.LogType = DbLogType.Login.ToString();
            try
            {
                if (OperatorProvider.WebHelper.GetSession("session_verifycode").IsEmpty() || Md5.md5(captcha.ToLower(), 16) != OperatorProvider.WebHelper.GetSession("session_verifycode"))
                {
                    throw new Exception("验证码错误");
                }
                UserModel userEntity = UserService.LoginOn(username, Md5.md5(password, 32));
                if (userEntity != null)
                {
                    if (userEntity.EnabledMark == 1)
                    {
                        throw new Exception("账号被锁定，禁止登录");
                    }
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = userEntity.Id;
                    operatorModel.Account = userEntity.Account;
                    operatorModel.RealName = userEntity.RealName;
                    operatorModel.HeadIcon = userEntity.HeadIcon;
                    operatorModel.RoleId = userEntity.RoleId;
                    operatorModel.LoginIPAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    operatorModel.LoginIPAddressName = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    OperatorProvider.AddCurrent(operatorModel);
                    logEntity.Account = userEntity.Account;
                    logEntity.RealName = userEntity.RealName;
                    logEntity.Description = "登陆成功";
                    LogService.WriteDbLog(logEntity, operatorModel.LoginIPAddress, operatorModel.LoginIPAddressName);
                    return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功" }.ToJson());
                }
                else
                {
                    throw new Exception("用户名或密码错误");
                }
            }
            catch (Exception ex)
            {
                logEntity.Account = username;
                logEntity.RealName = username;
                logEntity.Description = "登录失败，" + ex.Message;
                LogService.WriteDbLog(logEntity, HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Connection.RemoteIpAddress.ToString());
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }
        [HttpGet]
        public ActionResult LoginOut()
        {
            var OperatorProvider = new OperatorProvider(HttpContext);
            LogService.WriteDbLog(new LogModel
            {
                LogType = DbLogType.Exit.ToString(),
                Account = OperatorProvider.GetCurrent().Account,
                RealName = OperatorProvider.GetCurrent().RealName,
                Description = "安全退出系统",
            }, HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Connection.RemoteIpAddress.ToString());
            OperatorProvider.WebHelper.ClearSession();
            OperatorProvider.RemoveCurrent();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Error()
        {

            ViewData["StatusCode"] = HttpContext.Response.StatusCode;
            return View();
        }
    }
}
