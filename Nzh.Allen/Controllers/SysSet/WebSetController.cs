using Microsoft.AspNetCore.Mvc;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Allen.Controllers.SysSet
{
    public class WebSetController : BaseController
    {
        // GET: SysSet/WebSet
        public override ActionResult Index(int? id)
        {
            return View(new WebModel().GetWebInfo());
        }
        [HttpPost]
        public ActionResult Index(WebModel model)
        {
            try
            {
                new WebModel().SetWebInfo(model);
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Error:" + ex.Message;
                return View(new WebModel().GetWebInfo());
            }
            ViewBag.Msg = "修改成功！";
            return View(new WebModel().GetWebInfo());
        }
    }
}
