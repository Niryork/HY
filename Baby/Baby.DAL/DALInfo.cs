using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baby.Model;
using System.Data.SqlClient;
using System.Data;

namespace Baby.DAL
{
    public class DALInfo
    {
        DALCommon dal = DALCommon.CreateIntance();
        string msg = "";
        SqlParameter[] splist = null;
        /// <summary>
        /// Get info from v_info
        /// </summary>
        /// <returns></returns>
        public List<V_Info> GetInfo()
        {
            string sql = string.Format("select * from V_Info");
            DataTable dt = dal.ExecuteAdapter(sql, out msg);
            List<V_Info> list = dal.DataTable2List<V_Info>(dt, out msg);
            return list;
        }

        public bool InsertIntoPatience(Patience pSource)
        {
            string sql = dal.MakeInsertSql<Patience>(pSource, out splist, null);
            return (dal.ExecuteNonquery(sql, out msg, splist) > 0);
        }

        /// <summary>
        /// 将数据库获取的datetime类型转换为string类型-----未使用（格式转换在js中进行）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<Information> ToString(List<V_Info> list)
        {
            List<Information> info = new List<Information>();
            foreach (var item in list)
            {
                Information i = new Information() { BID=item.BID,BName = item.BName,Sex=item.Sex,Hid=item.Hid, HName = item.HName};

                DateTime time = item.Birthday == null ? DateTime.Now: Convert.ToDateTime(item.Birthday);
                i.Birthday = time.ToString("yyyy-MM-dd");

                time = item.Intime == null ? DateTime.Now: Convert.ToDateTime(item.Intime);
                i.Intime = time.ToString("yyyy-MM-dd");

                time = item.Recordtime == null ? DateTime.Now : Convert.ToDateTime(item.Recordtime);
                i.Recordtime = time.ToString("yyyy-MM-dd");
                info.Add(i);
            }
            return info;
        }
    }
}
