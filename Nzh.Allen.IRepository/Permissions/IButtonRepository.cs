using Nzh.Allen.Common;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IRepository
{
    public interface IButtonRepository : IBaseRepository<ButtonModel>
    {
        /// <summary>
        /// 根据角色菜单按钮位置获得按钮列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        IEnumerable<ButtonModel> GetButtonListByRoleIdModuleId(int roleId, int moduleId, PositionEnum position);

        /// <summary>
        /// 根据角色菜单获得按钮列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <param name="selectList"></param>
        /// <returns></returns>
        IEnumerable<ButtonModel> GetButtonListByRoleIdModuleId(int roleId, int moduleId, out IEnumerable<ButtonModel> selectList);
    }
}
