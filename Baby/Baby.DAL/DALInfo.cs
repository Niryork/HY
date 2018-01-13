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

            return dal.DataTable2List<V_Info>(dt, out msg).ToList();
        }

        public bool InsertIntoPatience(Patience pSource)
        {
            string sql = dal.MakeInsertSql<Patience>(pSource, out splist, null);
            return (dal.ExecuteNonquery(sql, out msg, splist) > 0);
        }


    }
}
