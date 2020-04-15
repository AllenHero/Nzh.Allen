using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface IOrganizeService : IBaseService<OrganizeModel>
    {
        IEnumerable<OrganizeModel> GetOrganizeList();
        IEnumerable<TreeSelect> GetOrganizeTreeSelect();
    }
}
