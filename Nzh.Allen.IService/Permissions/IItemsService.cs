using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface IItemsService : IBaseService<ItemsModel>
    {
        IEnumerable<TreeSelect> GetItemsTreeSelect();
    }
}
