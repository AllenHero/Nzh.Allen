using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface IRoleAuthorizeService : IBaseService<RoleAuthorizeModel>
    {
        /// <summary>
        /// 保存菜单角色权限配置
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        int SavePremission(IEnumerable<RoleAuthorizeModel> entitys, int roleId);

        /// <summary>
        /// 根据角色菜单获得列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        IEnumerable<RoleAuthorizeModel> GetListByRoleIdMenuId(int roleId, int menuId);
    }
}
