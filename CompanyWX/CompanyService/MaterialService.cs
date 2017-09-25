using CompanyIService;
using CompanyModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CompanyService
{
    public class MaterialService : IMaterialService
    {
        private static MaterialService _instance = null;
        public static MaterialService getInstance()
        {
            if (_instance == null)
            {
                _instance = new MaterialService();
            }
            return _instance;
        }

        public int Insert(CompanyModel.Material model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into material(");
            strSql.Append("title,contents,type,topimg,author,url,summary,annex,createtime,list,filename)");
            strSql.Append(" values (");
            strSql.Append("@title,@contents,@type,@topimg,@author,@url,@summary,@annex,@createtime,@list,@filename)");
            strSql.Append(";select @@IDENTITY");
        

            return DBHelper.ExecuteSql(strSql.ToString(), model); 
        }

        public int Update(CompanyModel.Material model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update material set ");
            strSql.Append("title=@title,");
            strSql.Append("contents=@contents,");
            strSql.Append("type=@type,");
            strSql.Append("topimg=@topimg,");
            strSql.Append("author=@author,");
            strSql.Append("url=@url,");
            strSql.Append("summary=@summary,");
            strSql.Append("annex=@annex,");
            strSql.Append("createtime=@createtime,");
            strSql.Append("list=@list,");
            strSql.Append("filename=@filename");
            strSql.Append(" where id=@id");
            return DBHelper.ExecuteSql(strSql.ToString(), model); 
        }

        public int Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from material ");
            strSql.Append(" where id=@id");


            return DBHelper.ExecuteSql(strSql.ToString(), new { id=id});
        }

        public CompanyModel.Material GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from material ");
            strSql.Append(" where id=@id");
            return DBHelper.ExecuteSql_First<Material>(strSql.ToString(), new { id = id });
        }

        public IList<CompanyModel.Material> GetListByPage(string where, string orderby, int pageindex, int pagesize, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from(select RN=ROW_NUMBER() OVER(ORDER BY Id desc),* from material ");
            if (where.Trim() != "")
            {
                strSql.Append("  where " + where);
            }
            strSql.Append(" ) as a  where a.RN between (@pageIndex-1)*@pageSize+1 and @pageIndex*@pageSize ");

            if (orderby.Trim() != "")
            {
                strSql.Append(" order by  " + orderby);
            }




            var lt = DBHelper.ExecuteSql_ToList<Material>(strSql.ToString(), new { @pageIndex = pageindex, @pageSize = pagesize });
            totalCount = GetTotalCount(where);

            //通过ToPagedList扩展方法进行分页

            return lt;
        }

        public IList<CompanyModel.Material> GetList(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from material");
            if (where.Trim() != "")
            {
                strSql.Append("  where " + where);
            }
            var list = DBHelper.ExecuteSql_ToList<Material>(strSql.ToString(), null);

            return list;
        }

        public int GetTotalCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as c from material");
            if (strWhere.Trim() != "")
            {
                strSql.Append("  where " + strWhere);
            }
            return DBHelper.ExecuteSql_First<int>(strSql.ToString(), null);
        }

        public int DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete  from material where id in("+Idlist+")");
            return DBHelper.ExecuteSql(strSql.ToString(),null);
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
