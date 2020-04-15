using Microsoft.AspNetCore.Mvc;
using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Allen.Controllers.Donation
{
    public class DonationController : BaseController
    {
        public IDonationService DonationService { get; set; }

        // GET: Donation/Donation
        public override ActionResult Index(int? id)
        {
            base.Index(id);
            return View();
        }
        [HttpGet]
        public JsonResult List(DonationModel model, PageInfo pageInfo)
        {
            var result = DonationService.GetListByFilter(model, pageInfo);
            return Json(result);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(DonationModel model)
        {
            model.CreateTime = DateTime.Now;
            var result = DonationService.Insert(model) ? SuccessTip("添加成功") : ErrorTip("添加失败");
            return Json(result);
        }
        public ActionResult Edit(int id)
        {
            var model = DonationService.GetById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(DonationModel model)
        {
            var result = DonationService.UpdateById(model) ? SuccessTip("修改成功") : ErrorTip("修改失败");
            return Json(result);
        }
        [HttpGet]
        public JsonResult Delete(int id)
        {
            var result = DonationService.DeleteById(id) ? SuccessTip("删除成功") : ErrorTip("删除失败");
            return Json(result);
        }
    }
}
