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
        DateTime time = DateTime.Now;
        DataTable dt = new DataTable();


        #region GetInfor
        /// <summary>
        /// Get info from v_info
        /// </summary>
        /// <returns></returns>
        public List<V_Info> GetV_Info()
        {
            string sql = string.Format("select * from V_Info");
            DataTable dt = dal.ExecuteAdapter(sql, out msg);
            List<V_Info> list = dal.DataTable2List<V_Info>(dt, out msg);
            return list;
        }
        /// <summary>
        /// Get Patient
        /// </summary>
        /// <param name="id">编辑事件所需id</param>
        /// <returns></returns>
        public List<Patience> GetPatient(string id)
        {
            string whColumns = "BID,p.MID,BName,Sex,Hid,Intime,Leavetime,Birthday,Birthtime,PregnantWeeks,BirthPlace,BabyNum,BabyTime,BabyWeight,BabyHeight,Midwife,HName";
            string sql = string.Format("select {1} from dbo.Patience p left join dbo.Hospital h on p.MID=h.MID where p.bid='{0}'", id, whColumns);
            //dal.MakeSelectSql<Patience>(p,)
            dt = dal.ExecuteAdapter(sql, out msg);
            List<Patience> list = dal.DataTable2List<Patience>(dt, out msg).ToList();

            return list;
        }

        /// <summary>
        /// 获取医院及接生人员列表
        /// </summary>
        /// <returns></returns>
        public List<Hospital> GetHospital()
        {
            Hospital h = new Hospital();
            string[] whColumns = { "distinct(HName)","Midwife" };
            string sql = dal.MakeSelectSql<Hospital>(h, whColumns);
            dt = dal.ExecuteAdapter(sql, out msg);

            return dal.DataTable2List<Hospital>(dt, out msg).ToList();
        }
        #endregion

        public bool InsertIntoPatience(Patience pSource)
        {
            string[] unInsertColumns = { "Midwife", "HName" };
            string sql = dal.MakeInsertSql<Patience>(pSource, unInsertColumns);
            return dal.ExecuteNonquery(sql, out msg, splist) > 0;
        }


        public bool Delete(string id)
        {
            Patience p = new Patience() { BID = Convert.ToInt32(id) };
            string[] col = { "BID" };
            string sql = dal.MakeDeleteSql<Patience>(p, col);

            return dal.ExecuteNonquery(sql, out msg) > 0;

        }

        public bool UptPatient(Patience model)
        {



            return false;
        }


        #region Useless
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
                Information i = new Information() { BID = item.BID, BName = item.BName, Sex = item.Sex, Hid = item.Hid, HName = item.HName };

                DateTime time = item.Birthday == null ? DateTime.Now : Convert.ToDateTime(item.Birthday);
                i.Birthday = time.ToString("yyyy-MM-dd");

                time = item.Intime == null ? DateTime.Now : Convert.ToDateTime(item.Intime);
                i.Intime = time.ToString("yyyy-MM-dd");

                time = item.Recordtime == null ? DateTime.Now : Convert.ToDateTime(item.Recordtime);
                i.Recordtime = time.ToString("yyyy-MM-dd");
                info.Add(i);
            }
            return info;
        }

        #endregion
    }
}
