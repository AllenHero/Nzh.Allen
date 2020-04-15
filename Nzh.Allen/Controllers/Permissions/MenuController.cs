using Microsoft.AspNetCore.Mvc;
using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Allen.Controllers.Permissions
{
    public class MenuController : BaseController
    {
        public IMenuService MenuService { get; set; }
        // GET: Permissions/Module
        public override ActionResult Index(int? id)
        {
            base.Index(id);
            return View();
        }
        [HttpGet]
        public JsonResult List()
        {
            var list = MenuService.GetAll();
            var result = new { code = 0, count = list.Count(), data = list };
            return Json(result);
        }
        [HttpGet]
        public JsonResult GetModuleList()
        {
            object result = MenuService.GetModuleList(Operator.RoleId);
            return Json(result);
        }
        [HttpGet]
        public JsonResult GetModuleTreeSelect()
        {
            var result = MenuService.GetModuleTreeSelect();
            return Json(result);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Add(MenuModel model)
        {
            model.FontFamily = "ok-icon";
            model.CreateTime = DateTime.Now;
            model.CreateUserId = Operator.UserId;
            model.UpdateTime = DateTime.Now;
            model.UpdateUserId = Operator.UserId;
            var result = MenuService.Insert(model) ? SuccessTip("添加成功") : ErrorTip("添加失败");
            return Json(result);
        }
        public ActionResult Edit(int id)
        {
            var model = MenuService.GetById(id);
            return View(model);
        }
        [HttpPost]

        public ActionResult Edit(MenuModel model)
        {
            model.UpdateTime = DateTime.Now;
            model.UpdateUserId = Operator.UserId;
            var result = MenuService.UpdateById(model) ? SuccessTip("修改成功") : ErrorTip("修改失败");
            return Json(result);
        }
        [HttpGet]
        public JsonResult Delete(int id)
        {
            var result = MenuService.DeleteById(id) ? SuccessTip("删除成功") : ErrorTip("删除失败");
            return Json(result);
        }
        [HttpGet]
        public JsonResult ModuleButtonList(int roleId)
        {
            var list = MenuService.GetModuleButtonList(roleId);
            var result = new { code = 0, count = list.Count(), data = list };
            return Json(result);
        }
    }
}
