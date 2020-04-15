using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IRepository
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        UserModel LoginOn(string username, string password);

        int ModifyUserPwd(ModifyPwd model, int userId);
    }
}
