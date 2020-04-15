using Microsoft.AspNetCore.Mvc;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Allen.Controllers.SysSet
{
    public class EmailSetController : BaseController
    {
        // GET: SysSet/EmailSet
        public override ActionResult Index(int? id)
        {
            return View(new EmailModel().GetEmailInfo());
        }
        [HttpPost]
        public ActionResult Index(EmailModel model)
        {
            try
            {
                new EmailModel().SetEmailInfo(model);
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Error:" + ex.Message;
                return View(new EmailModel().GetEmailInfo());
            }
            ViewBag.Msg = "修改成功！";
            return View(new EmailModel().GetEmailInfo());
        }
    }
}
