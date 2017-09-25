using CompanyIService;
using CompanyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyService
{
    public class UserInfoService : IUserInfoService
    {
        public static UserInfoService _instance = null;

        public static UserInfoService getInstance()
        {

            if (_instance == null)
            {
                _instance = new UserInfoService();
            }
            return _instance;
        }

        public int Insert(CompanyModel.UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserInfo(");
            strSql.Append("username,PassWord,CreateTime,UpdateTime,UsedNums,LastLoginTime,Status,Types)");
            strSql.Append(" values (");
            strSql.Append("@username,@PassWord,@CreateTime,@UpdateTime,@UsedNums,@LastLoginTime,@Status,@Types)");
            strSql.Append(";select @@IDENTITY");
            return DBHelper.ExecuteSql(strSql.ToString(), model);
        }
        /// <summary>
        /// 检测账号是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool CheckAccount(string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as c from userinfo");
            strSql.Append("  where username='" + username.Trim() + "'");
            var num = DBHelper.ExecuteSql_First<int>(strSql.ToString(), null);
            if (num != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IList<CompanyModel.UserInfo> CheckLogin(string username, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from userinfo where username='" + username + "' and password='" + password + "'");
            var list = DBHelper.ExecuteSql_ToList<UserInfo>(strSql.ToString(), null);

            return list;
        }
        public int UpdatePassWord(UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");
            strSql.Append("password=@password,");
            strSql.Append("updatetime=@updatetime");
            strSql.Append(" where id=@id");
            return DBHelper.ExecuteSql(strSql.ToString(), model);
        }
        public int Update(CompanyModel.UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");
            strSql.Append("username=@username,");
            strSql.Append("UserNums=@UserNums,");
            strSql.Append("PassWord=@PassWord,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("LastLoginTime=@LastLoginTime,");
            strSql.Append("Status=@Status,");
            strSql.Append("Types=@Types");
            strSql.Append(" where id=@id");
            return DBHelper.ExecuteSql(strSql.ToString(), model);
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CompanyModel.UserInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from UserInfo ");
            strSql.Append(" where id=@id");
            return DBHelper.ExecuteSql_First<UserInfo>(strSql.ToString(), new { id = id });
        }

        public IList<CompanyModel.UserInfo> GetListByPage(string where, string orderby, int pageindex, int pagesize, out int totalCount)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyModel.UserInfo> GetList(string where)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as c from userinfo");
            if (strWhere.Trim() != "")
            {
                strSql.Append("  where " + strWhere);
            }
            return DBHelper.ExecuteSql_First<int>(strSql.ToString(), null);
        }

        public int DeleteList(string Idlist)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public bool Exists(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
