﻿using Dapper;
using Nzh.Allen.IRepository;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nzh.Allen.Repository
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public UserModel LoginOn(string username, string password)
        {
            using (var conn = dbContext.GetConnection())
            {
                var sql = "Select * from user where 1=1";
                if (!string.IsNullOrEmpty(username))
                {
                    sql += " and Account=@Account";
                }
                if (!string.IsNullOrEmpty(password))
                {
                    sql += " and UserPassWord=@UserPassWord";
                }
                return conn.Query<UserModel>(sql, new { Account = username, UserPassWord = password }).FirstOrDefault();
            }
        }

        public int ModifyUserPwd(ModifyPwd model, int userId)
        {
            using (var conn = dbContext.GetConnection())
            {
                var sql = "UPDATE user SET UserPassword=@UserPassword WHERE Id=@Id AND Account=@Account AND UserPassword=@OldPassword";
                return conn.Execute(sql, new { UserPassword = model.Password, Id = userId, Account = model.UserName, OldPassword = model.OldPassword });
            }
        }
    }
}
