using Dapper;
using Nzh.Allen.IRepository;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Repository
{
    public class ModuleRepository : BaseRepository<ModuleModel>, IModuleRepository
    {
        /// <summary>
        /// 根据角色ID获取菜单列表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IEnumerable<ModuleModel> GetModuleListByRoleId(string sql, int roleId)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.Query<ModuleModel>(sql, new { RoleId = roleId });
            }
        }
    }
}
