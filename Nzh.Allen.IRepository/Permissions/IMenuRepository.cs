using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IRepository
{
    public interface IMenuRepository : IBaseRepository<MenuModel>
    {
        IEnumerable<MenuModel> GetMenuListByRoleId(string sql, int roleId);
    }
}
