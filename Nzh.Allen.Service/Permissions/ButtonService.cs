using Nzh.Allen.Common;
using Nzh.Allen.IRepository;
using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nzh.Allen.Service
{
    public class ButtonService : BaseService<ButtonModel>, IButtonService
    {
        public IButtonRepository ButtonRepository { get; set; }
        /// <summary>
        /// 根据角色菜单按钮位置获得按钮列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public IEnumerable<ButtonModel> GetButtonListByRoleIdMenuId(int roleId, int menuId, PositionEnum position)
        {
            return ButtonRepository.GetButtonListByRoleIdMenuId(roleId, menuId, position);
        }

        /// <summary>
        /// 根据角色菜单获得按钮列表Html
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public string GetButtonListHtmlByRoleIdMenuId(int roleId, int menuId)
        {
            IEnumerable<ButtonModel> selectList = null;
            var allList = ButtonRepository.GetButtonListByRoleIdMenuId(roleId, menuId, out selectList);
            StringBuilder sb = new StringBuilder();
            if (allList != null && allList.Count() > 0)
            {
                foreach (var item in allList)
                {
                    var checkedStr = selectList.FirstOrDefault(x => x.Id == item.Id) == null ? "" : "checked";
                    sb.AppendFormat("<input name='cbx_{0}' class='layui-btn layui-btn-sm' lay-skin='primary' value='{1}' title='{2}' type='checkbox' {3}>", menuId, item.Id, item.FullName, checkedStr);
                }
            }
            return sb.ToString();
        }

        public dynamic GetListByFilter(ButtonModel filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.EnCode))
            {
                _where += " and EnCode=@EnCode";
            }
            if (!string.IsNullOrEmpty(filter.FullName))
            {
                _where += " and FullName=@FullName";
            }
            return GetListByFilter(filter, pageInfo, _where);
        }

    }
}
