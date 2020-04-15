using Nzh.Allen.IRepository;
using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Service
{
    public class RoleAuthorizeService : BaseService<RoleAuthorizeModel>, IRoleAuthorizeService
    {
        public IRoleAuthorizeRepository RoleAuthorizeRepository { get; set; }

        public dynamic GetListByFilter(RoleAuthorizeModel filter, PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public int SavePremission(IEnumerable<RoleAuthorizeModel> entitys, int roleId)
        {
            return RoleAuthorizeRepository.SavePremission(entitys, roleId);
        }

        public IEnumerable<RoleAuthorizeModel> GetListByRoleIdMenuId(int roleId, int menuId)
        {
            string where = "where RoleId=@RoleId and MenuId=@MenuId";
            return GetByWhere(where, new { RoleId = roleId, MenuId = menuId });
        }
    }
}
