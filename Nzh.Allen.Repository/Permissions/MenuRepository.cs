using Dapper;
using Nzh.Allen.IRepository;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Repository
{
    public class MenuRepository : BaseRepository<MenuModel>, IMenuRepository
    {
        public IEnumerable<MenuModel> GetMenuListByRoleId(string sql, int roleId)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.Query<MenuModel>(sql, new { RoleId = roleId });
            }
        }
    }
}
