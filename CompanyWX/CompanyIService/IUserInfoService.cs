using CompanyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyIService
{
    public interface IUserInfoService : IDAL<UserInfo>
    {
        bool CheckAccount(string account);

        IList<CompanyModel.UserInfo> CheckLogin(string username, string password);

        int UpdatePassWord(UserInfo model);

    }
}
