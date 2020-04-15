using Microsoft.AspNetCore.Mvc;
using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Allen.Controllers.Permissions
{
    public class LogController : BaseController
    {
        public ILogService LogService { get; set; }

        public override ActionResult Index(int? id)
        {
            base.Index(id);
            return View();
        }

        [HttpGet]
        public JsonResult List(LogModel model, PageInfo pageInfo)
        {
            var result = LogService.GetListByFilter(model, pageInfo);
            return Json(result);
        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            var result = LogService.DeleteById(id) ? SuccessTip("删除成功") : ErrorTip("删除失败");
            return Json(result);
        }

        [HttpGet]
        public JsonResult BatchDel(string idsStr)
        {
            var idsArray = idsStr.Substring(0, idsStr.Length - 1).Split(',');
            var result = LogService.DeleteByIds(idsArray) ? SuccessTip("批量删除成功") : ErrorTip("批量删除失败");
            return Json(result);
        }
    }
}
