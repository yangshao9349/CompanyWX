using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using NuoDe.Code.Log; // 引用

/// <summary>
///DBHelper 的摘要说明
/// </summary>

    public class DBHelper
    {
        //连接数据库字符串。
        private static string cnnstr = ConfigurationManager.AppSettings["ConnectionString"];

        public DBHelper()
        {
            cnnstr = ConfigurationManager.AppSettings["ConnectionString"];
        }

        private static SqlConnection GetConnection()
        {
            SqlConnection cnn = new SqlConnection(cnnstr);
            cnn.Open();

            return cnn;
        }

        //-------------------------------
        /// <summary>
        /// 执行SQL 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteSql(string sql, object param)
        {
            try
            {
                using (var con = GetConnection())
                {
                    int i = con.Execute(sql, param);
                    return i;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return -1;
            }

        }

        /// <summary>
        /// 执行SQL，返回第一行第一个元素的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T ExecuteSql_First<T>(string sql, object param)
        {
            try
            {
                using (var con = GetConnection())
                {
                    T result = con.Query<T>(sql, param).FirstOrDefault<T>();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return default(T);
            }
        }

        /// <summary>
        /// 执行SQL，返回数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<T> ExecuteSql_ToList<T>(string sql, object param)
        {
            try
            {
                using (var con = GetConnection())
                {
                    IEnumerable<T> result = con.Query<T>(sql, param);
                    return result.ToList<T>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }
        }

        //------------------------
        /// <summary>
        /// 执行存储过程,无返回结果集  
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteSP(string proName, object param)
        {
          
            try
            {
                using (var con = GetConnection())
                {
                    int result = con.Execute(proName, param, CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 执行存储过程,返回数据实体结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<T> ExecuteSP_ToList<T>(string proName, object param)
        {
            try
            {
                using (var con = GetConnection())
                {
                    IEnumerable<T> result = con.Query<T>(proName, param, CommandType.StoredProcedure);
                    return result.ToList<T>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }
        }
        //执行事务
        public static int ExecuteSql(List<string> sqls, List<object> param)
        {
            int k = 0;
            try
            {
                using (var con = GetConnection())
                {
                    IDbTransaction transaction = con.BeginTransaction();
                    try
                    {
                      
                        for (int i = 0; i < sqls.Count; i++)
                        {
                           
                            k=con.Execute(sqls[i], param[i], transaction, null, null);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        
                        transaction.Rollback();
                        Logger.Error(ex.ToString());
                        throw new Exception(ex.Message);
                    }
                   
                   
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                k= -1;
            }
            return k;

        }

        //执行事务
        public static int ExecuteSql(List<string> sqls)
        {
            int k = 0;
            try
            {
                using (var con = GetConnection())
                {
                    IDbTransaction transaction = con.BeginTransaction();
                    try
                    {

                        for (int i = 0; i < sqls.Count; i++)
                        {

                           k= con.Execute(sqls[i], null, transaction, null, null);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        Logger.Error(ex.ToString());
                        throw new Exception(ex.Message);
                    }


                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                k = -1;
            }
            return k;

        }


    }
