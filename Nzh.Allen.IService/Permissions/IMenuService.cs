using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface IMenuService : IBaseService<MenuModel>
    {
        /// <summary>
        /// 获得菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        dynamic GetModuleList(int roleId);
        /// <summary>
        /// Module treeSelect数据列表
        /// </summary>
        IEnumerable<TreeSelect> GetModuleTreeSelect();
        /// <summary>
        /// 获取所有菜单列表及可用按钮权限列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        IEnumerable<MenuModel> GetModuleButtonList(int roleId);
    }
}
