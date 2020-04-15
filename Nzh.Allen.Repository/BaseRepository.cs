using Nzh.Allen.Extension.SQLExts.MySQLExt;
using Nzh.Allen.IRepository;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        public MySqlHelper dbContext { set; get; }

        public T GetById(int Id)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.GetById<T>(Id);
            }
        }

        public int Insert(T model)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.Insert<T>(model);
            }
        }

        public int UpdateById(T model)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.UpdateById<T>(model);
            }
        }

        public int UpdateById(T model, string updateFields)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.UpdateById<T>(model, updateFields);
            }
        }

        public int DeleteById(int Id)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.DeleteById<T>(Id);
            }
        }

        public int DeleteByIds(object Ids)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.DeleteByIds<T>(Ids);
            }
        }

        public int DeleteByWhere(string where)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.DeleteByWhere<T>(where);
            }
        }

        public IEnumerable<T> GetByPage(SearchFilter filter, out long total)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.GetByPage<T>(filter.pageIndex, filter.pageSize, out total, filter.returnFields, filter.where, filter.param, filter.orderBy, filter.transaction, filter.commandTimeout);
            }
        }

        public IEnumerable<T> GetByPageUnite(SearchFilter filter, out long total)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.GetByPageUnite<T>(filter.pageIndex, filter.pageSize, out total, filter.returnFields, filter.where, filter.param, filter.orderBy, filter.transaction, filter.commandTimeout);
            }
        }

        public IEnumerable<T> GetAll(string returnFields = null, string orderby = null)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.GetAll<T>(returnFields, orderby);
            }
        }

        public IEnumerable<T> GetByWhere(string where = null, object param = null, string returnFields = null, string orderby = null)
        {
            using (var conn = dbContext.GetConnection())
            {
                return conn.GetByWhere<T>(where, param, returnFields, orderby);
            }
        }
    }
}
