using Microsoft.AspNetCore.Mvc;
using Nzh.Allen.Common;
using Nzh.Allen.Handler;
using Nzh.Allen.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Allen.Controllers
{
    [HandlerLogin]
    public class BaseController : Controller
    {
        protected const string SuccessText = "操作成功！";

        protected const string ErrorText = "操作失败！";

        public IButtonService ButtonService { get; set; }

        public OperatorModel Operator
        {
            get { return new OperatorProvider(HttpContext).GetCurrent(); }
        }

        public virtual ActionResult Index(int? id)
        {
            var _menuId = id == null ? 0 : id.Value;
            var _roleId = Operator.RoleId;
            if (id != null)
            {
                ViewData["RightButtonList"] = ButtonService.GetButtonListByRoleIdMenuId(_roleId, _menuId, PositionEnum.FormInside);
                ViewData["TopButtonList"] = ButtonService.GetButtonListByRoleIdMenuId(_roleId, _menuId, PositionEnum.FormRightTop);
            }
            return View();
        }

        protected virtual AjaxResult SuccessTip(string message = SuccessText)
        {
            return new AjaxResult { state = ResultType.success.ToString(), message = message };
        }

        protected virtual AjaxResult ErrorTip(string message = ErrorText)
        {
            return new AjaxResult { state = ResultType.error.ToString(), message = message };
        }
    }
}
