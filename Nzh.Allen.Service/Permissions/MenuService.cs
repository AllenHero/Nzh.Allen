using Nzh.Allen.IRepository;
using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nzh.Allen.Service
{
    public class MenuService : BaseService<MenuModel>, IMenuService
    {
        public IMenuRepository MenuRepository { get; set; }

        public IButtonService ButtonService { get; set; }

        public IRoleAuthorizeService RoleAuthorizeService { get; set; }

        public dynamic GetListByFilter(MenuModel filter, PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public dynamic GetMenuList(int roleId)
        {
            IEnumerable<MenuModel> allMenus = GetMenuListByRoleId(roleId);
            List<Tree> treeList = new List<Tree>();
            var rootMenus = allMenus.Where(x => x.ParentId == 0).OrderBy(x => x.SortCode);
            foreach (var item in rootMenus)
            {
                var _tree = new Tree { id = item.Id, title = item.FullName, href = item.UrlAddress, fontFamily = item.FontFamily, icon = item.Icon };
                GetMenuListByMenuId(treeList, allMenus, _tree, item.Id);
                treeList.Add(_tree);
            }
            var result = treeList;
            return result;
        }

        private void GetMenuListByMenuId(List<Tree> treeList, IEnumerable<MenuModel> allMenus, Tree tree, int menuId)
        {
            var childMenus = allMenus.Where(x => x.ParentId == menuId).OrderBy(x => x.SortCode);
            if (childMenus != null && childMenus.Count() > 0)
            {
                List<Tree> _children = new List<Tree>();
                foreach (var item in childMenus)
                {
                    var _tree = new Tree { id = item.Id, title = item.FullName, href = item.UrlAddress, fontFamily = item.FontFamily, icon = item.Icon };
                    _children.Add(_tree);
                    tree.children = _children;
                    GetMenuListByMenuId(treeList, allMenus, _tree, item.Id);
                }
            }
        }

        private IEnumerable<MenuModel> GetMenuListByRoleId(int roleId)
        {
            string sql = @"SELECT DISTINCT b.* FROM roleauthorize a INNER JOIN menu b ON a.MenuId = b.Id WHERE a.RoleId = @RoleId ORDER BY b.SortCode ASC";
            var list = MenuRepository.GetMenuListByRoleId(sql, roleId);
            return list;
        }

        public IEnumerable<TreeSelect> GetMenuTreeSelect()
        {
            IEnumerable<MenuModel> menuList = MenuRepository.GetAll("Id,FullName,ParentId", "ORDER BY SortCode ASC");
            var rootMenuList = menuList.Where(x => x.ParentId == 0).OrderBy(x => x.SortCode);
            List<TreeSelect> treeSelectList = new List<TreeSelect>();
            foreach (var item in rootMenuList)
            {
                TreeSelect tree = new TreeSelect
                {
                    id = item.Id,
                    name = item.FullName,
                    open = false
                };
                GetMenuChildren(treeSelectList, menuList, tree, item.Id);
                treeSelectList.Add(tree);
            }
            return treeSelectList;
        }

        private void GetMenuChildren(List<TreeSelect> treeSelectList, IEnumerable<MenuModel> menuList, TreeSelect tree, int id)
        {
            var childMenuList = menuList.Where(x => x.ParentId == id).OrderBy(x => x.SortCode);
            if (childMenuList != null && childMenuList.Count() > 0)
            {
                List<TreeSelect> _children = new List<TreeSelect>();
                foreach (var item in childMenuList)
                {
                    TreeSelect _tree = new TreeSelect
                    {
                        id = item.Id,
                        name = item.FullName,
                        open = false
                    };
                    _children.Add(_tree);
                    tree.children = _children;
                    GetMenuChildren(treeSelectList, menuList, _tree, item.Id);
                }
            }
        }

        public IEnumerable<MenuModel> GetMenuButtonList(int roleId)
        {
            string returnFields = "Id,ParentId,FullName,Icon,SortCode";
            string orderby = "ORDER BY SortCode ASC";
            IEnumerable<MenuModel> list = GetAll(returnFields, orderby);
            foreach (var item in list)
            {
                item.MenuButtonHtml = ButtonService.GetButtonListHtmlByRoleIdMenuId(roleId, item.Id);
                item.IsChecked = RoleAuthorizeService.GetListByRoleIdMenuId(roleId, item.Id).Count() > 0 ? true : false;
            }
            return list;
        }
    }
}
