using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyIService
{
    public interface IDAL<T> where T : class
    {
        int Insert(T model);
        int Update(T model);
        int Delete(int id);
        T GetModel(int id);
        IList<T> GetListByPage(string where, string orderby, int pageindex, int pagesize, out int totalCount);

        IList<T> GetList(string where);

        int GetTotalCount(string strWhere);

        int DeleteList(string Idlist);

        int GetMaxId();

        bool Exists(int Id);

    }
}
