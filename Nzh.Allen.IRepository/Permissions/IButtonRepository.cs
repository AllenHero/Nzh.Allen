using Nzh.Allen.Common;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IRepository
{
    public interface IButtonRepository : IBaseRepository<ButtonModel>
    {
        IEnumerable<ButtonModel> GetButtonListByRoleIdMenuId(int roleId, int menuId, PositionEnum position);

        IEnumerable<ButtonModel> GetButtonListByRoleIdMenuId(int roleId, int menuId, out IEnumerable<ButtonModel> selectList);
    }
}
