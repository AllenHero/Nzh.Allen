using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IRepository
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserModel LoginOn(string username, string password);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int ModifyUserPwd(ModifyPwd model, int userId);
    }
}
