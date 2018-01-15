using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baby.Model;
using Baby.DAL;
namespace Baby.BLL
{
    public class BLLInfo
    {
        DALInfo dal = new DALInfo();
        public List<V_Info> GetV_Info()
        {
            return dal.GetV_Info();
        }
        public Patience GetPatient(string id)
        {
            return dal.GetPatient(id).FirstOrDefault();
        }
        public List<Hospital> GetHospital()
        {
            return dal.GetHospital();
        }

        public bool InsertIntoPatience(Patience pSource)
        {
            return dal.InsertIntoPatience(pSource);
        }

        public bool Delete(string id)
        {
            return dal.Delete(id);
        }

    }
}
