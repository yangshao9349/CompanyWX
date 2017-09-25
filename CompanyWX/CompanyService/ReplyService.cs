using CompanyIService;
using CompanyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyService
{
    public  class ReplyService : IReplyService
    {
        private static ReplyService _instance = null;
        public static ReplyService getInstance()
        {
            if (_instance == null)
            {
                _instance = new ReplyService();
            }
            return _instance;
        }

        public int Insert(CompanyModel.Reply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Reply(");
            strSql.Append("keys,contents,createtime,type,resid,searchtype,title)");
            strSql.Append(" values (");
            strSql.Append("@keys,@contents,@createtime,@type,@resid,@searchtype,@title)");
            strSql.Append(";select @@IDENTITY");
            return DBHelper.ExecuteSql(strSql.ToString(), model); 

        }

        public int Update(CompanyModel.Reply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Reply set ");
            strSql.Append("keys=@keys,");
            strSql.Append("contents=@contents,");
            strSql.Append("createtime=@createtime,");
            strSql.Append("type=@type,");
            strSql.Append("searchtype=@searchtype,");
            strSql.Append("title=@title,");
            strSql.Append("resid=@resid");
            strSql.Append(" where id=@id");

            return DBHelper.ExecuteSql(strSql.ToString(), model); 
        }

        public int Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Reply ");
            strSql.Append(" where id=@id");

            return DBHelper.ExecuteSql(strSql.ToString(), new { id = id });
        }

        public CompanyModel.Reply GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *  from Reply ");
            strSql.Append(" where id=@id");

            return DBHelper.ExecuteSql_First<Reply>(strSql.ToString(), new { id = id });
        }

        public IList<CompanyModel.Reply> GetListByPage(string where, string orderby, int pageindex, int pagesize, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from(select RN=ROW_NUMBER() OVER(ORDER BY Id desc),* from Reply ");
            if (where.Trim() != "")
            {
                strSql.Append("  where " + where);
            }
            strSql.Append(" ) as a  where a.RN between (@pageIndex-1)*@pageSize+1 and @pageIndex*@pageSize ");

            if (orderby.Trim() != "")
            {
                strSql.Append(" order by  " + orderby);
            }




            var lt = DBHelper.ExecuteSql_ToList<Reply>(strSql.ToString(), new { @pageIndex = pageindex, @pageSize = pagesize });
            totalCount = GetTotalCount(where);

            //通过ToPagedList扩展方法进行分页

            return lt;
        }

        public IList<CompanyModel.Reply> GetList(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Reply");
            if (where.Trim() != "")
            {
                strSql.Append("  where " + where);
            }
            var list = DBHelper.ExecuteSql_ToList<Reply>(strSql.ToString(), null);

            return list;
        }

        public int GetTotalCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as c from Reply");
            if (strWhere.Trim() != "")
            {
                strSql.Append("  where " + strWhere);
            }
            return DBHelper.ExecuteSql_First<int>(strSql.ToString(), null);
        }

        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete  from Reply where id in(" + Idlist + ")");
            return DBHelper.ExecuteSql(strSql.ToString(), null);
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
