using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface IMenuService : IBaseService<MenuModel>
    {
        dynamic GetMenuList(int roleId);

        IEnumerable<TreeSelect> GetMenuTreeSelect();

        IEnumerable<MenuModel> GetMenuButtonList(int roleId);
    }
}
