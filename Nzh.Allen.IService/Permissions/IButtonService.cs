using Nzh.Allen.Common;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface IButtonService : IBaseService<ButtonModel>
    {
        IEnumerable<ButtonModel> GetButtonListByRoleIdMenuId(int roleId, int menuId, PositionEnum position);

        string GetButtonListHtmlByRoleIdMenuId(int roleId, int menuId);
    }
}
