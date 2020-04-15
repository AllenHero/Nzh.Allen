using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nzh.Allen.IService;
using Nzh.Allen.Model;
using Nzh.Allen.Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Allen.Controllers.Security
{
    public class QuartzController : BaseController
    {
        public ITaskScheduleService taskScheduleService { set; get; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        public JobCenter jobCenter { set; get; }

        public override ActionResult Index(int? id)
        {
            base.Index(id);
            return View();
        }

        [HttpGet]
        public JsonResult List(TaskScheduleModel model, PageInfo pageInfo)
        {
            var result = taskScheduleService.GetListByFilter(model, pageInfo);
            return Json(result);
        }

        #region 创建新的任务
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(TaskScheduleModel model)
        {
            model.CreateTime = DateTime.Now;
            model.State = 0;
            var result = taskScheduleService.Insert(model) ? SuccessTip("添加成功") : ErrorTip("添加失败");
            return Json(result);
        }
        #endregion

        #region 启动/恢复,停止任务
        [HttpGet]
        public ActionResult ScheduleJob(int id, bool isState)
        {
            try
            {
                var model = taskScheduleService.GetById(id);
                if (!isState)
                {
                    model.State = 0;
                    jobCenter.StopScheduleJobAsync(model);
                    var result = taskScheduleService.StopScheduleJob(model) ? SuccessTip("成功") : ErrorTip("失败");
                    return Json(result);
                }
                else
                {
                    model.State = 1;
                    jobCenter.ResumeScheduleJobAsync(model);
                    var result = taskScheduleService.ResumeScheduleJob(model) ? SuccessTip("成功") : ErrorTip("失败");
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                var result = ErrorTip(ex.Message);
                return Json(result);
            }

        }
        #endregion

        #region 删除新的任务
        [HttpGet]
        public JsonResult Delete(int id)
        {
            var model = taskScheduleService.GetById(id);
            //改为假删除
            model.State = 2;
            var result = taskScheduleService.UpdateById(model) ? SuccessTip("删除成功") : ErrorTip("删除失败");
            return Json(result);
        }
        [HttpGet]
        public JsonResult BatchDel(string idsStr)
        {
            try
            {
                var idsArray = idsStr.Substring(0, idsStr.Length - 1).Split(',');
                var models = taskScheduleService.GetByWhere(" where Id in @Ids", new { Ids = idsArray });
                foreach (var model in models)
                {
                    //改为假删除
                    model.State = 2;
                    taskScheduleService.UpdateById(model);
                }
                return Json(SuccessTip("删除成功"));
            }
            catch (Exception ex)
            {
                return Json(ErrorTip(ex.Message));
            }
        }
        #endregion

        public JsonResult ExportFile()
        {
            UploadFile uploadFile = new UploadFile();
            try
            {
                var file = Request.Form.Files[0];    //获取选中文件
                var filecombin = file.FileName.Split('.');
                if (file == null || string.IsNullOrEmpty(file.FileName) || file.Length == 0 || filecombin.Length < 2)
                {
                    uploadFile.code = -1;
                    uploadFile.src = "";
                    uploadFile.msg = "上传出错!请检查文件名或文件内容";
                    return Json(uploadFile);
                }
                //定义本地路径位置
                string localPath = WebHostEnvironment.WebRootPath + @"/JobPlugins";
                string filePathName = file.FileName; //最终文件名
                //Upload不存在则创建文件夹
                if (!System.IO.Directory.Exists(localPath))
                {
                    System.IO.Directory.CreateDirectory(localPath);
                }
                if (System.IO.File.Exists(Path.Combine(localPath, filePathName)))
                {
                    uploadFile.code = -1;
                    uploadFile.src = "";
                    uploadFile.msg = "不要重复上传同一定时任务!";
                    return Json(uploadFile);
                }
                using (FileStream fs = System.IO.File.Create(Path.Combine(localPath, filePathName)))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                uploadFile.code = 0;
                uploadFile.src = filePathName;
                uploadFile.msg = "上传成功";
                return Json(uploadFile);
            }
            catch (Exception)
            {
                uploadFile.code = -1;
                uploadFile.src = "";
                uploadFile.msg = "上传出错!程序异常";
                return Json(uploadFile);
            }
        }
    }
}
