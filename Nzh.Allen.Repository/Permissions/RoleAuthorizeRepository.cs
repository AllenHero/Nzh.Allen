using Nzh.Allen.Extension.SQLExts.MySQLExt;
using Nzh.Allen.IRepository;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nzh.Allen.Repository
{
    public class RoleAuthorizeRepository : BaseRepository<RoleAuthorizeModel>, IRoleAuthorizeRepository
    {
        /// <summary>
        /// 保存菜单角色权限配置
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int SavePremission(IEnumerable<RoleAuthorizeModel> entitys, int roleId)
        {
            int result = 0;
            using (var conn = dbContext.GetConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    //先删除当前角色所有权限
                    conn.DeleteByWhere<RoleAuthorizeModel>("where RoleId=@RoleId", new { RoleId = roleId }, transaction);
                    if (entitys != null)
                    {
                        //批量插入权限
                        conn.InsertBatch<RoleAuthorizeModel>(entitys, transaction);
                    }
                    result = 1;
                    transaction.Commit();
                }
                catch (Exception)
                {
                    result = -1;
                    transaction.Rollback();
                }
            }
            return result;
        }
    }
}
