using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IRepository
{
    public interface IModuleRepository : IBaseRepository<ModuleModel>
    {
        /// <summary>
        /// 根据角色ID获取菜单列表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IEnumerable<ModuleModel> GetModuleListByRoleId(string sql, int roleId);

    }
}
