using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nzh.Allen.Service
{
    public class ItemsService : BaseService<ItemsModel>, IItemsService
    {
        public dynamic GetListByFilter(ItemsModel filter, PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Items treeSelect数据列表
        /// </summary>
        public IEnumerable<TreeSelect> GetItemsTreeSelect()
        {
            IEnumerable<ItemsModel> menuList = BaseRepository.GetAll("Id,FullName,ParentId", "ORDER BY SortCode ASC");
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
                GetItemsChildren(treeSelectList, menuList, tree, item.Id);
                treeSelectList.Add(tree);
            }
            return treeSelectList;
        }

        /// <summary>
        /// 递归遍历treeSelectList
        /// </summary>
        private void GetItemsChildren(List<TreeSelect> treeSelectList, IEnumerable<ItemsModel> menuList, TreeSelect tree, int id)
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
                    GetItemsChildren(treeSelectList, menuList, _tree, item.Id);
                }
            }
        }
    }
}
