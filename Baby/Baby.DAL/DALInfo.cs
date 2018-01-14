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
        public List<Information> GetInfo()
        {
            string sql = string.Format("select * from V_Info");
            DataTable dt = dal.ExecuteAdapter(sql, out msg);
            List<V_Info> list = dal.DataTable2List<V_Info>(dt, out msg);

            List<Information> info = ToString(list);

            return info;
        }

        public bool InsertIntoPatience(Patience pSource)
        {
            string sql = dal.MakeInsertSql<Patience>(pSource, out splist, null);
            return (dal.ExecuteNonquery(sql, out msg, splist) > 0);
        }

        private List<Information> ToString(List<V_Info> list)
        {
            List<Information> info = new List<Information>();
            foreach (var item in list)
            {
                Information i = new Information() { BID=item.BID,BName = item.BName,Sex=item.Sex,Hid=item.Hid,
                HName = item.HName};



                DateTime time = item.Birthday == null ? DateTime.Now : Convert.ToDateTime(item.Birthday);
                i.Birthday = time.ToString("yyyy-MM-dd");

                DateTime intime = item.Intime == null ? DateTime.Now : Convert.ToDateTime(item.Intime);
                i.Intime = intime.ToString("yyyy-MM-dd");

                DateTime log = item.Recordtime == null ? DateTime.Now : Convert.ToDateTime(item.Recordtime);
                i.Recordtime = log.ToString("yyyy-MM-dd");
                info.Add(i);
            }
            return info;
        }
    }
}
