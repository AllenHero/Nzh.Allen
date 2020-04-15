using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface IRoleAuthorizeService : IBaseService<RoleAuthorizeModel>
    {
        int SavePremission(IEnumerable<RoleAuthorizeModel> entitys, int roleId);

        IEnumerable<RoleAuthorizeModel> GetListByRoleIdMenuId(int roleId, int menuId);
    }
}
