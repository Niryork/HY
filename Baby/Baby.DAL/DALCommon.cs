using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Baby.DAL
{
    public class DALCommon
    {
        #region 使用单例模式,创建实例

        static DALCommon dal;
        static object obj = new object();

        /// <summary>
        /// 使用单例模式,创建实例，避免重复新建该公共类
        /// </summary>
        /// <param name="strconn"></param>
        /// <returns></returns>
        public static DALCommon CreateIntance(string strconn = "")
        {
            if (dal == null)
            {
                lock (obj)
                {
                    if (dal == null)
                    {
                        if (strconn == "")
                        {
                            strconn = GetStrConn.GetConn();
                        }
                        dal = new DALCommon(strconn);
                    }
                }
            }
            return dal;

        }

        private DALCommon(string sc)
        {
            strConn = sc;
        }
        #endregion

        #region 数据库操作

        private string strConn;
        private SqlConnection conn;
        private SqlCommand cmd;

        #region 增删改
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonquery(string sql, out string msg, SqlParameter[] splst = null)
        {
            using (conn = new SqlConnection(strConn))
            {
                msg = "";
                try
                {
                    conn.Open();
                    cmd = new SqlCommand(sql, conn);
                    if (splst != null)
                    {
                        cmd.Parameters.AddRange(splst);
                    }
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return 0;
                }

            }
        }
        #endregion

        #region 获取数据表格
        /// <summary>
        /// 获取数据表格
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable ExecuteAdapter(string sql, out string msg, SqlParameter[] splst = null)
        {
            using (conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand(sql, conn);
                    if (splst != null)
                    {
                        cmd.Parameters.AddRange(splst);
                    }
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    msg = "";
                    return dt;

                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return null;
                }

            }

        }
        #endregion

        #region 获取首行首列
        public object ExecuteScalar(string sql, out string msg, SqlParameter[] splst = null)
        {
            using (conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand(sql, conn);
                    if (splst != null)
                    {
                        cmd.Parameters.AddRange(splst);
                    }
                    object obj = cmd.ExecuteScalar();
                    msg = "";
                    return obj;

                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return null;
                }

            }
        }
        #endregion

        #region 获取分页数据
        public DataTable ExecutePageProcedure(int pageIndex, int pageSize, string tableName, string orderBy, out int recordCount, string where = "", string fields = "*")
        {
            using (SqlConnection con = new SqlConnection(strConn))
            using (SqlDataAdapter ada = new SqlDataAdapter("usp_Pager", con))
            {
                //@PageIndex int=1,--页码
                //@PageSize int=100,--每页记录数
                //@Table nvarchar(20),--表名
                //@Where nvarchar(500)='',--查询条件
                //@Fields nvarchar(500)='*',--待查询的字段
                //@OrderBy nvarchar(500),--排序
                //@Total int output--总记录数

                SqlParameter[] ps = {
                                     new SqlParameter("@PageIndex",pageIndex),
                                     new SqlParameter("@PageSize",pageSize),
                                     new SqlParameter("@Table",tableName),
                                     new SqlParameter("@Where",where),
                                     new SqlParameter("@Fields",fields),
                                     new SqlParameter("@OrderBy",orderBy),
                                     new SqlParameter("@Total",SqlDbType.Int)
                                     };
                ps[6].Direction = ParameterDirection.Output;//总记录数 输出参数

                ada.SelectCommand.CommandType = CommandType.StoredProcedure;//将命令类型设置为存储过程
                ada.SelectCommand.Parameters.AddRange(ps);//批量添加参数

                DataTable dt = new DataTable();
                ada.Fill(dt);//调用存储过程，将数据填充到DataTable中

                recordCount = Convert.ToInt32(ps[6].Value);//总记录数
                return dt;
            }
        }


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageindex">页索引</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="tablename">表名</param>
        /// <param name="pkey">主键名</param>
        /// <param name="strWh">带and条件sql语句</param>
        /// <param name="orderby">排序</param>
        /// <param name="count">返回总条数</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public DataTable ExecutePageProc(int pageindex, int pagesize, string tablename, string pkey, string strWh, string orderby, out int count, out string msg, string select = "*")
        {
            DataTable dt = new DataTable();
            SqlParameter[] splist = {
                                     new SqlParameter("@pageindex",pageindex),
                                     new SqlParameter("@pagesize",pagesize),
                                     new SqlParameter("@table",tablename),
                                     new SqlParameter("@pkey",pkey),
                                     new SqlParameter("@strWh",strWh),
                                     new SqlParameter("@select",select),
                                     new SqlParameter("@orderby",orderby)
                                     };
            dt = ExecutePageProc("proc_Page", splist, "count", out count, out msg);

            return dt;

        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="procName">分页的存储过程名</param>
        /// <param name="splist">存储过程中的参数列表</param>
        /// <param name="strCount">总条数的参数名称</param>
        /// <param name="count">返回总条数</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public DataTable ExecutePageProc(string procName, SqlParameter[] splist, string strCount, out int count, out string msg)
        {
            count = 0;
            msg = "";
            using (conn = new SqlConnection(strConn))
            {
                try
                {

                    conn.Open();
                    cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(splist);

                    SqlParameter sp = new SqlParameter();
                    sp.ParameterName = "@" + strCount;
                    sp.Direction = ParameterDirection.Output;
                    sp.DbType = DbType.Int32;
                    cmd.Parameters.Add(sp);

                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);

                    count = Convert.ToInt32(sp.Value);

                    return dt;

                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return null;
                }

            }

        }


        #endregion

        #endregion

        #region 拼写SQL语句

        #region 拼写添加语句
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="UnInsertColumns"></param>
        /// <param name="isUnColumns">指示【UnInsertColumns】是不插入的列,默认True</param>
        /// <returns></returns>
        public string MakeInsertSql<T>(T model, String[] UnInsertColumns, bool isUnColumns = true, string tablename = null)
        {
            if (model == null)
            {
                return "";
            }
            //将不要插入的列转换成小写
            UnInsertColumns = ToLower(UnInsertColumns);

            StringBuilder sbInsert = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();

            Type t = typeof(T);
            if (tablename == null)
            {
                tablename = t.Name;
            }
            sbInsert
                .Append("INSERT INTO ")
                .Append(tablename)
                .Append("(");

            sbValue.Append(" VALUES (");
            PropertyInfo[] pis = t.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (isUnColumns)
                {
                    if (UnInsertColumns != null && UnInsertColumns.Length > 0 && UnInsertColumns.Contains(pi.Name.ToLower()))
                    {
                        continue;
                    }
                }
                else
                {
                    if (UnInsertColumns != null && UnInsertColumns.Length > 0 && !UnInsertColumns.Contains(pi.Name.ToLower()))
                    {
                        continue;
                    }
                }


                sbInsert
                    .Append(pi.Name)
                    .Append(",");

                sbValue.AppendFormat("'{0}',", pi.GetValue(model, null));

            }

            sbInsert.Remove(sbInsert.Length - 1, 1);
            sbValue.Remove(sbValue.Length - 1, 1);

            sbInsert.Append(")");
            sbValue.Append(")");

            sbInsert.Append(sbValue);
            return sbInsert.ToString();
        }
        #endregion

        #region 拼写删除语句
        /// <summary>
        /// 拼写删除语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="whColumns"></param>
        /// <returns></returns>
        public string MakeDeleteSql<T>(T model1, String[] whColumns, string tablename = null)
            where T : new()
        {
            T model = model1;
            if (model1 == null)
            {
                model = new T();
            }


            //将输入的条件语句的列全部转换成小写
            whColumns = ToLower(whColumns);

            StringBuilder sbDelete = new StringBuilder();

            Type t = model.GetType();
            if (tablename == null)
            {
                tablename = t.Name;
            }
            sbDelete
                .Append("DELETE FROM ")
                .Append(tablename)
                .Append(" WHERE 1=1");
            PropertyInfo[] pis = t.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (whColumns.Contains(pi.Name.ToLower()))
                {
                    sbDelete.AppendFormat(" AND {0} = '{1}'", pi.Name, pi.GetValue(model, null));
                }
            }

            return sbDelete.ToString();
        }
        #endregion

        #region 拼写修改语句
        /// <summary>
        /// 拼写修改语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uptModel">要修改的对象</param>
        /// <param name="whModel">在条件中的对象</param>
        /// <param name="uptColumns">要修改的列</param>
        /// <param name="whColumns">条件的列</param>
        /// <returns></returns>
        public string MakeUpdateSql<T, U>(T uptModel, U whModel, string[] uptColumns, string[] whColumns, string strWh = null, string tablename = null)
        where U : new()
        {
            //将输入的条件语句的列全部转换成小写
            whColumns = ToLower(whColumns);
            uptColumns = ToLower(uptColumns);

            StringBuilder sbUpdate = new StringBuilder();
            StringBuilder sbWH = new StringBuilder();
            Type t = typeof(T);
            if (tablename == null)
            {
                tablename = t.Name;
            }

            if (whModel == null)
            {
                whModel = new U();
            }

            Type u = typeof(U);


            sbUpdate
                .Append("UPDATE ")
                .Append(tablename)
                .Append(" SET ");


            //拼写要修改的字段语句
            PropertyInfo[] pis = t.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object obj = pi.GetValue(uptModel, null);
                if (obj == null)
                {
                    continue;
                }
                if (uptColumns.Contains(pi.Name.ToLower()))
                {
                    sbUpdate
                   .Append(pi.Name)
                   .Append("=")
                   .AppendFormat("'{0}'", obj)
                   .Append(",");
                }

            }

            //拼写条件中的字段语句
            pis = u.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object obj = pi.GetValue(whModel, null);
                if (obj == null)
                {
                    continue;
                }
                if (whColumns.Contains(pi.Name.ToLower()))
                {
                    sbWH.AppendFormat(" AND {0}='{1}'", pi.Name, obj);
                    continue;
                }

            }

            sbUpdate.Remove(sbUpdate.Length - 1, 1);


            sbUpdate
                .AppendFormat(" Where 1=1 {0} ", sbWH);

            if (strWh != null)
            {
                sbUpdate.Append(strWh);
            }
            return sbUpdate.ToString();
            ;

        }
        #endregion

        #region 拼写查询语句
        /// <summary>
        /// 拼写查询语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="tablename"></param>
        /// <param name="whereColumns">条件语句的列</param>
        /// <param name="slColumns">查询的列</param>
        /// <returns></returns>
        public string MakeSelectSql<T>(T model1, string[] whColumns, string orderby = "", string[] likeColumns = null, string slColumns = "*", string tablename = null)
            where T : new()
        {
            //将输入的条件语句的列全部转换成小写
            whColumns = ToLower(whColumns);
            likeColumns = ToLower(likeColumns);
            T model = model1;
            if (model1 == null)
            {
                model = new T();
            }


            string sql = "";
            StringBuilder sb = new StringBuilder();
            Type t = typeof(T);
            if (tablename == null)
            {
                tablename = t.Name;
            }
            sb.AppendFormat("SELECT {0} FROM {1} WHERE 1=1 ", slColumns, tablename);

            PropertyInfo[] pis = t.GetProperties();

            foreach (PropertyInfo pi in pis)
            {
                object obj = pi.GetValue(model, null);

                if (whColumns.Contains(pi.Name.ToLower()))
                {
                    if (obj != null && obj.ToString() != "")
                    {
                        sb.AppendFormat(" AND {0}='{1}'", pi.Name, obj);
                    }

                }
                if (likeColumns.Contains(pi.Name.ToLower()))
                {
                    sb.AppendFormat(" AND {0} like '%{1}%'", pi.Name, pi.GetValue(model, null));
                }
            }
            if (orderby != "")
            {
                sb.AppendFormat(" ORDER BY {0} ", orderby);
            }
            sql = sb.ToString();


            return sql;
        }
        #endregion

        #region 拼写批量添加
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="UnInsertColumns"></param>
        /// <param name="isUnColumns">指示【UnInsertColumns】是不插入的列,默认True</param>
        /// <returns></returns>
        public string BatchInsert<T>(List<T> list, String[] UnInsertColumns, bool isUnColumns = true, string tablename = null)
        {
            //将不要插入的列转换成小写
            UnInsertColumns = ToLower(UnInsertColumns);

            StringBuilder sbInsert = new StringBuilder();

            Type t = typeof(T);
            if (tablename == null)
            {
                tablename = t.Name;
            }
            sbInsert
                .Append("INSERT INTO ")
                .Append(tablename)
                .Append("(");

            //获取所有属性
            PropertyInfo[] pis = t.GetProperties();

            //拼写 要插入的列
            foreach (PropertyInfo pi in pis)
            {
                if (isUnColumns)
                {
                    if (UnInsertColumns != null && UnInsertColumns.Length > 0 && UnInsertColumns.Contains(pi.Name.ToLower()))
                    {
                        continue;
                    }
                }
                else
                {
                    if (UnInsertColumns != null && UnInsertColumns.Length > 0 && !UnInsertColumns.Contains(pi.Name.ToLower()))
                    {
                        continue;
                    }
                }


                sbInsert
                    .Append(pi.Name)
                    .Append(",");

            }
            //去除最后一个逗号
            sbInsert.Remove(sbInsert.Length - 1, 1);
            sbInsert.Append(") ");

            int count = 0;
            //拼写要插入的数据
            foreach (T model in list)
            {
                count++;
                sbInsert.Append(" SELECT ");
                foreach (PropertyInfo pi in pis)
                {
                    if (UnInsertColumns != null && UnInsertColumns.Length > 0)
                    {
                        if (isUnColumns)
                        {
                            if (UnInsertColumns.Contains(pi.Name.ToLower()))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (!UnInsertColumns.Contains(pi.Name.ToLower()))
                            {
                                continue;
                            }
                        }
                    }

                    sbInsert.AppendFormat("'{0}',", pi.GetValue(model, null));
                }
                //去除最后一个逗号
                sbInsert.Remove(sbInsert.Length - 1, 1);

                if (count < list.Count)
                {
                    sbInsert.Append(" UNION ");
                }
            }


            return sbInsert.ToString();
        }
        #endregion

        #region 将数组转换成小写
        /// <summary>
        /// 将数组转换成小写
        /// </summary>
        /// <param name="colums"></param>
        /// <returns></returns>
        private string[] ToLower(string[] colums)
        {
            if (colums == null)
            {
                colums = new string[] { };
            }
            for (int i = 0; i < colums.Length; i++)
            {
                colums[i] = colums[i].ToLower();
            }
            return colums;
        }
        #endregion
        #endregion

        #region 通过Parameter参数赋值，拼写SQL语句

        #region 拼写添加语句
        /// <summary>
        /// 拼写添加语句通过Parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="UnInsertColumns"></param>
        /// <param name="isUnColumns">指示【UnInsertColumns】是不插入的列,默认True</param>
        /// <returns></returns>
        public string MakeInsertSql<T>(T model, out SqlParameter[] splist, String[] UnInsertColumns, bool isUnColumns = true, string tablename = null)
        {
            //将不要插入的列转换成小写
            UnInsertColumns = ToLower(UnInsertColumns);

            StringBuilder sbInsert = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();
            // StringBuilder sbValue = new StringBuilder();

            Type t = typeof(T);
            if (tablename == null)
            {
                tablename = t.Name;
            }
            sbInsert
                .Append("INSERT INTO ")
                .Append(tablename)
                .Append("(");

            sbValue.Append(" VALUES (");
            PropertyInfo[] pis = t.GetProperties();
            //   SqlParameter[] splist = new SqlParameter[pis.Length];

            List<SqlParameter> splst = new List<SqlParameter>();
            foreach (PropertyInfo pi in pis)
            {
                if (isUnColumns)
                {
                    if (UnInsertColumns != null && UnInsertColumns.Length > 0 && UnInsertColumns.Contains(pi.Name.ToLower()))
                    {
                        continue;
                    }
                }
                else
                {
                    if (UnInsertColumns != null && UnInsertColumns.Length > 0 && !UnInsertColumns.Contains(pi.Name.ToLower()))
                    {
                        continue;
                    }
                }


                sbInsert
                    .Append(pi.Name)
                    .Append(",");
                sbValue.AppendFormat("@{0},", pi.Name);

                SqlParameter sp = new SqlParameter("@" + pi.Name, pi.GetValue(model, null));
                splst.Add(sp);

            }

            sbInsert.Remove(sbInsert.Length - 1, 1);
            sbValue.Remove(sbValue.Length - 1, 1);

            sbInsert.Append(")");
            sbValue.Append(")");

            sbInsert.Append(sbValue);

            splist = splst.ToArray();

            return sbInsert.ToString();
        }
        #endregion

        #region 拼写删除语句
        /// <summary>
        /// 拼写删除语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="whColumns"></param>
        /// <returns></returns>
        public string MakeDeleteSql<T>(T model1, String[] whColumns, out SqlParameter[] splist, string tablename = null)
            where T : new()
        {
            T model = model1;
            if (model1 == null)
            {
                model = new T();
            }

            List<SqlParameter> splst = new List<SqlParameter>();
            //将输入的条件语句的列全部转换成小写
            whColumns = ToLower(whColumns);

            StringBuilder sbDelete = new StringBuilder();

            Type t = model.GetType();
            if (tablename == null)
            {
                tablename = t.Name;
            }
            sbDelete
                .Append("DELETE FROM ")
                .Append(tablename)
                .Append(" WHERE 1=1");
            PropertyInfo[] pis = t.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (whColumns.Contains(pi.Name.ToLower()))
                {
                    sbDelete.AppendFormat(" AND {0} = @{0}", pi.Name);
                    SqlParameter sp = new SqlParameter("@" + pi.Name, pi.GetValue(model, null));
                    splst.Add(sp);
                }
            }

            splist = splst.ToArray();
            return sbDelete.ToString();
        }
        #endregion

        #region 拼写修改语句
        /// <summary>
        /// 拼写修改语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uptModel">要修改的对象</param>
        /// <param name="whModel">在条件中的对象</param>
        /// <param name="uptColumns">要修改的列</param>
        /// <param name="whColumns">条件的列</param>
        /// <returns></returns>
        public string MakeUpdateSql<T, U>(T uptModel, U whModel, string[] uptColumns, string[] whColumns, out SqlParameter[] splist, string strWh = null, string tablename = null)
        where U : new()
        {
            //将输入的条件语句的列全部转换成小写
            whColumns = ToLower(whColumns);
            uptColumns = ToLower(uptColumns);

            StringBuilder sbUpdate = new StringBuilder();
            StringBuilder sbWH = new StringBuilder();
            Type t = typeof(T);
            if (tablename == null)
            {
                tablename = t.Name;
            }
            if (whModel == null)
            {
                whModel = new U();
            }
            Type u = typeof(U);

            List<SqlParameter> splst = new List<SqlParameter>();

            sbUpdate
                .Append("UPDATE ")
                .Append(tablename)
                .Append(" SET ");


            //拼写要修改的字段语句
            PropertyInfo[] pis = t.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object obj = pi.GetValue(uptModel, null);
                if (obj == null)
                {
                    continue;
                }
                if (uptColumns.Contains(pi.Name.ToLower()))
                {
                    sbUpdate
                   .Append(pi.Name)
                   .Append("=")
                   .AppendFormat("@{0}", pi.Name)
                   .Append(",");


                    SqlParameter sp = new SqlParameter("@" + pi.Name, obj);
                    splst.Add(sp);
                }

            }

            //拼写条件中的字段语句
            pis = u.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object obj = pi.GetValue(whModel, null);
                if (obj == null)
                {
                    continue;
                }
                if (whColumns.Contains(pi.Name.ToLower()))
                {
                    sbWH.AppendFormat(" AND {0}=@{0}1", pi.Name);
                    SqlParameter sp = new SqlParameter("@" + pi.Name + "1", obj);
                    splst.Add(sp);
                    continue;
                }

            }

            sbUpdate.Remove(sbUpdate.Length - 1, 1);


            sbUpdate
                .AppendFormat(" Where 1=1 {0} ", sbWH);

            sbUpdate.Append(strWh);

            splist = splst.ToArray();
            return sbUpdate.ToString();
            ;

        }
        #endregion

        #region 拼写查询语句
        /// <summary>
        /// 拼写查询语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="tablename"></param>
        /// <param name="whereColumns">条件语句的列</param>
        /// <param name="slColumns">查询的列</param>
        /// <returns></returns>
        public string MakeSelectSql<T>(T model1, string[] whColumns, out SqlParameter[] splist, string[] likeColumns = null, string orderby = "", string slColumns = "*", string tablename = null)
            where T : new()
        {
            //将输入的条件语句的列全部转换成小写
            whColumns = ToLower(whColumns);
            likeColumns = ToLower(likeColumns);

            T model = model1;
            if (model1 == null)
            {
                model = new T();
            }


            string sql = "";
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> splst = new List<SqlParameter>();
            Type t = typeof(T);
            if (tablename == null)
            {
                tablename = t.Name;
            }
            sb.AppendFormat("SELECT {0} FROM {1} WHERE 1=1 ", slColumns, tablename);

            PropertyInfo[] pis = t.GetProperties();

            foreach (PropertyInfo pi in pis)
            {
                if (whColumns.Contains(pi.Name.ToLower()))
                {
                    sb.AppendFormat(" AND {0}=@{0} ", pi.Name);
                    SqlParameter sp = new SqlParameter("@" + pi.Name, pi.GetValue(model, null));
                    splst.Add(sp);
                }

                if (likeColumns.Contains(pi.Name.ToLower()))
                {
                    sb.AppendFormat(" AND {0} like '%'+@{0}+'%' ", pi.Name);
                    SqlParameter sp = new SqlParameter("@" + pi.Name, pi.GetValue(model, null));
                    splst.Add(sp);
                }
            }
            if (orderby != "")
            {
                sb.AppendFormat(" ORDER BY {0} ", orderby);
            }

            sql = sb.ToString();

            splist = splst.ToArray();
            return sql;
        }
        #endregion

        #region 拼写批量添加
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="UnInsertColumns"></param>
        /// <param name="isUnColumns">指示【UnInsertColumns】是不插入的列,默认True</param>
        /// <returns></returns>
        public string BatchInsert<T>(List<T> list, out SqlParameter[] splist, String[] UnInsertColumns, bool isUnColumns = true, string tablename = null)
        {
            //将不要插入的列转换成小写
            UnInsertColumns = ToLower(UnInsertColumns);

            StringBuilder sbInsert = new StringBuilder();

            Type t = typeof(T);
            if (tablename == null)
            {
                tablename = t.Name;
            }
            List<SqlParameter> splst = new List<SqlParameter>();
            sbInsert
                .Append("INSERT INTO ")
                .Append(tablename)
                .Append("(");

            //获取所有属性
            PropertyInfo[] pis = t.GetProperties();

            //拼写 要插入的列
            foreach (PropertyInfo pi in pis)
            {
                if (isUnColumns)
                {
                    if (UnInsertColumns != null && UnInsertColumns.Length > 0 && UnInsertColumns.Contains(pi.Name.ToLower()))
                    {
                        continue;
                    }
                }
                else
                {
                    if (UnInsertColumns != null && UnInsertColumns.Length > 0 && !UnInsertColumns.Contains(pi.Name.ToLower()))
                    {
                        continue;
                    }
                }


                sbInsert
                    .Append(pi.Name)
                    .Append(",");

            }
            //去除最后一个逗号
            sbInsert.Remove(sbInsert.Length - 1, 1);
            sbInsert.Append(") ");

            int count = 0;
            //拼写要插入的数据
            int i = 0;
            foreach (T model in list)
            {
                count++;
                sbInsert.Append(" SELECT ");
                foreach (PropertyInfo pi in pis)
                {
                    if (UnInsertColumns != null && UnInsertColumns.Length > 0)
                    {
                        if (isUnColumns)
                        {
                            if (UnInsertColumns.Contains(pi.Name.ToLower()))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (!UnInsertColumns.Contains(pi.Name.ToLower()))
                            {
                                continue;
                            }
                        }
                    }

                    sbInsert.AppendFormat("@{0}{1},", pi.Name, i);
                    SqlParameter sp = new SqlParameter("@" + pi.Name + i, pi.GetValue(model, null));
                    splst.Add(sp);
                }
                i++;
                //去除最后一个逗号
                sbInsert.Remove(sbInsert.Length - 1, 1);

                if (count < list.Count)
                {
                    sbInsert.Append(" UNION ");
                }
            }

            splist = splst.ToArray();
            return sbInsert.ToString();
        }
        #endregion

        #endregion

        #region 数据转换


        #region DataTable转List.v2.0
        public List<T> DataTable2List<T>(DataTable dt, out string msg)
        where T : new()
        {
            try
            {

                msg = "";

                // 定义集合  
                List<T> ts = new List<T>();

                // 获得此模型的类型  
                Type type = typeof(T);
                //定义一个临时变量  
                string tempName = string.Empty;
                //遍历DataTable中所有的数据行  
                foreach (DataRow dr in dt.Rows)
                {
                    T t = new T();
                    // 获得此模型的公共属性  
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    //遍历该对象的所有属性  
                    foreach (PropertyInfo pi in propertys)
                    {
                        tempName = pi.Name;//将属性名称赋值给临时变量  
                        //检查DataTable是否包含此列（列名==对象的属性名）    
                        if (dt.Columns.Contains(tempName))
                        {
                            // 判断此属性是否有Setter  
                            if (!pi.CanWrite) continue;//该属性不可写，直接跳出  
                            //取值  
                            object value = dr[tempName];
                            //如果非空，则赋给对象的属性  
                            if (value != DBNull.Value)
                                
                                pi.SetValue(t, value, null);
                        }
                    }
                    //对象添加到泛型集合中  
                    ts.Add(t);
                }

                return ts;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return default(List<T>);
            }


        }
        #endregion

        #region DataTable转IList-v1.0
        public static IList<T> ConvertTo<T>(DataTable table, out string msg)
        {
            msg = "";
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows, out msg);
        }

        public static IList<T> ConvertTo<T>(List<DataRow> rows, out string msg)
        {
            List<T> list = null;
            msg = "";
            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row, out msg);
                    list.Add(item);
                }
            }

            return list;
        }

        public static T CreateItem<T>(DataRow row, out string msg)
        {
            msg = "";
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        return default(T);
                    }
                }
            }

            return obj;
        }
        #endregion

        #region DataTable转List-v0.1
        ///// <summary>
        ///// DataTable转List
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="dt"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public List<T> DataTable2List<T>(DataTable dt, out string msg)
        //where T : new()
        //{
        //    try
        //    {

        //        msg = "";
        //        List<T> list = new List<T>();
        //        if (dt == null)
        //        {
        //            return list;
        //        }

        //        Type t = typeof(T);
        //        PropertyInfo[] pis = t.GetProperties();

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            T model = new T();
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                if (dt.Columns[j].ColumnName != "EntityKey" && dt.Columns[j].ColumnName != "EntityState")
        //                {
        //                    //给对应的属性赋值
        //                    pis[j].SetValue(model, ChangeType(dt.Rows[i][j], pis[j].PropertyType), null);
        //                }

        //            }

        //            list.Add(model);
        //        }
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //        return null;
        //    }


        //}
        #endregion



        #region Object转List
        /// <summary>
        /// Object转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public List<T> Object2List<T>(T obj, out string msg)
        where T : new()
        {
            try
            {

                msg = "";
                List<T> list = new List<T>();
                if (obj == null)
                {
                    return list;
                }
                list.Add(obj);

                return list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }


        }
        #endregion

        #region 把List装换成DataTable
        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        public DataTable ToDataTable<T>(List<T> items)
        {
            if (items == null)
            {
                return new DataTable();
            }

            var tb = new DataTable(typeof(T).Name);


            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                if (item != null)
                {
                    var values = new object[props.Length];

                    for (int i = 0; i < props.Length; i++)
                    {
                        values[i] = props[i].GetValue(item, null);
                    }

                    tb.Rows.Add(values);
                }
            }

            return tb;
        }
        #endregion

        #region 定义特殊的类型：空类型
        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        #endregion

        #region 反射获取值
        /// <summary>
        /// 反射获取值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type conversionType)
        {
            value = value is DBNull ? null : value;
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value != null)
                {
                    NullableConverter nullableConverter = new NullableConverter(conversionType);
                    conversionType = nullableConverter.UnderlyingType;
                    return Convert.ChangeType(value, conversionType);
                }

                return "null";
            }

            return Convert.ChangeType(value, conversionType);
        }
        #endregion
        #endregion

    }
}
