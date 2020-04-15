using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IRepository
{
    public interface IRoleAuthorizeRepository : IBaseRepository<RoleAuthorizeModel>
    {
        int SavePremission(IEnumerable<RoleAuthorizeModel> entitys, int roleId);
    }
}
