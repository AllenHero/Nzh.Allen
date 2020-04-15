using Nzh.Allen.IRepository;
using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nzh.Allen.Service
{
    public class OrganizeService : BaseService<OrganizeModel>, IOrganizeService
    {
        public IOrganizeRepository OrganizeRepository { get; set; }

        public dynamic GetListByFilter(OrganizeModel filter, PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrganizeModel> GetOrganizeList()
        {
            return OrganizeRepository.GetOrganizeList();
        }

        public IEnumerable<TreeSelect> GetOrganizeTreeSelect()
        {
            IEnumerable<OrganizeModel> organizeList = BaseRepository.GetAll("Id,FullName,ParentId", "ORDER BY SortCode ASC");
            var rootOrganizeList = organizeList.Where(x => x.ParentId == 0).OrderBy(x => x.SortCode);
            List<TreeSelect> treeSelectList = new List<TreeSelect>();
            foreach (var item in rootOrganizeList)
            {
                TreeSelect tree = new TreeSelect
                {
                    id = item.Id,
                    name = item.FullName,
                    open = false
                };
                GetOrganizeChildren(treeSelectList, organizeList, tree, item.Id);
                treeSelectList.Add(tree);
            }
            return treeSelectList;
        }

        private void GetOrganizeChildren(List<TreeSelect> treeSelectList, IEnumerable<OrganizeModel> organizeList, TreeSelect tree, int id)
        {
            var childOrganizeList = organizeList.Where(x => x.ParentId == id).OrderBy(x => x.SortCode);
            if (childOrganizeList != null && childOrganizeList.Count() > 0)
            {
                List<TreeSelect> _children = new List<TreeSelect>();
                foreach (var item in childOrganizeList)
                {
                    TreeSelect _tree = new TreeSelect
                    {
                        id = item.Id,
                        name = item.FullName,
                        open = false
                    };
                    _children.Add(_tree);
                    tree.children = _children;
                    GetOrganizeChildren(treeSelectList, organizeList, _tree, item.Id);
                }
            }
        }
    }
}
