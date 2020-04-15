using Dapper;
using Nzh.Allen.Common;
using Nzh.Allen.IRepository;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Repository
{
    public class ButtonRepository : BaseRepository<ButtonModel>, IButtonRepository
    {
        public IEnumerable<ButtonModel> GetButtonListByRoleIdMenuId(int roleId, int menuId, PositionEnum position)
        {
            using (var conn = dbContext.GetConnection())
            {
                string sql = @"SELECT b.* FROM roleauthorize a
                            INNER JOIN button b ON a.ButtonId=b.Id
                            WHERE a.RoleId=@RoleId
                            and a.MenuId=@MenuId
                            and b.Location=@Location
                            ORDER BY b.SortCode";
                return conn.Query<ButtonModel>(sql, new { RoleId = roleId, MenuId = menuId, Location = (int)position });
            }
        }

        public IEnumerable<ButtonModel> GetButtonListByRoleIdMenuId(int roleId, int menuId, out IEnumerable<ButtonModel> selectList)
        {
            using (var conn = dbContext.GetConnection())
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"SELECT Id,FullName FROM button a
                            INNER JOIN roleauthorize b ON a.Id = b.ButtonId
                            WHERE b.RoleId = @RoleId and b.MenuId = @MenuId;");
                sb.AppendLine(@"SELECT Id, FullName FROM button");
                using (var reader = conn.QueryMultiple(sb.ToString(), new { RoleId = roleId, MenuId = menuId }))
                {
                    selectList = reader.Read<ButtonModel>();
                    return reader.Read<ButtonModel>();
                }
            }
        }
    }
}
