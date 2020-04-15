using Nzh.Allen.IRepository;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Service
{
    public abstract class BaseService<T> where T : class, new()
    {
        public IBaseRepository<T> BaseRepository { get; set; }

        public T GetById(int Id)
        {
            return BaseRepository.GetById(Id);
        }

        public bool Insert(T model)
        {
            return BaseRepository.Insert(model) > 0 ? true : false;
        }

        public bool UpdateById(T model)
        {
            return BaseRepository.UpdateById(model) > 0 ? true : false;
        }

        public bool UpdateById(T model, string updateFields)
        {
            return BaseRepository.UpdateById(model, updateFields) > 0 ? true : false;
        }

        public bool DeleteById(int Id)
        {
            return BaseRepository.DeleteById(Id) > 0 ? true : false;
        }

        public bool DeleteByIds(object Ids)
        {
            return BaseRepository.DeleteByIds(Ids) > 0 ? true : false;
        }

        public bool DeleteByWhere(string where)
        {
            return BaseRepository.DeleteByWhere(where) > 0 ? true : false;
        }

        public dynamic GetListByFilter(T filter, PageInfo pageInfo, string where = null)
        {
            string _orderBy = string.Empty;
            if (!string.IsNullOrEmpty(pageInfo.field))
            {
                _orderBy = string.Format(" ORDER BY {0} {1}", pageInfo.field, pageInfo.order);
            }
            else
            {
                _orderBy = " ORDER BY CreateTime desc";
            }
            long total = 0;
            var list = BaseRepository.GetByPage(new SearchFilter { pageIndex = pageInfo.page, pageSize = pageInfo.limit, returnFields = pageInfo.returnFields, param = filter, where = where, orderBy = _orderBy }, out total);
            return Pager.Paging(list, total);
        }

        public dynamic GetPageUnite(T filter, PageInfo pageInfo, string where = null)
        {
            string _orderBy = string.Empty;
            if (!string.IsNullOrEmpty(pageInfo.field))
            {
                _orderBy = string.Format(" ORDER BY {0} {1}", pageInfo.field, pageInfo.order);
            }
            else
            {
                _orderBy = " ORDER BY CreateTime desc";
            }
            long total = 0;
            var list = BaseRepository.GetByPageUnite(new SearchFilter { pageIndex = pageInfo.page, pageSize = pageInfo.limit, returnFields = pageInfo.returnFields, param = filter, where = where, orderBy = _orderBy }, out total);
            return Pager.Paging(list, total);
        }

        public IEnumerable<T> GetAll(string returnFields = null, string orderby = null)
        {
            return BaseRepository.GetAll(returnFields, orderby);
        }

        protected string CreateTimeWhereStr(string StartEndDate, string _where, string prefix = null)
        {
            if (!string.IsNullOrEmpty(StartEndDate) && StartEndDate != " ~ ")
            {
                if (StartEndDate.Contains("~"))
                {
                    if (StartEndDate.Contains("+"))
                    {
                        StartEndDate = StartEndDate.Replace("+", "");
                    }
                    var dts = StartEndDate.Split('~');
                    var start = dts[0].Trim();
                    var end = dts[1].Trim();
                    if (!string.IsNullOrEmpty(start))
                    {
                        if (!string.IsNullOrEmpty(prefix))
                        {
                            _where += string.Format(" and {1}CreateTime>='{0} 00:00'", start, prefix);
                        }
                        else
                        {
                            _where += string.Format(" and CreateTime>='{0} 00:00'", start);
                        }
                    }
                    if (!string.IsNullOrEmpty(end))
                    {
                        if (!string.IsNullOrEmpty(prefix))
                        {
                            _where += string.Format(" and {1}CreateTime<='{0} 59:59'", end, prefix);
                        }
                        else
                        {
                            _where += string.Format(" and CreateTime<='{0} 59:59'", end);
                        }
                    }
                }
            }
            return _where;
        }

        public IEnumerable<T> GetByWhere(string where = null, object param = null, string returnFields = null, string orderby = null)
        {
            return BaseRepository.GetByWhere(where, param, returnFields, orderby);
        }
    }
}
