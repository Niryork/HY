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
        public List<V_Info> GetInfo()
        {
            return dal.GetInfo();
        }

        public bool InsertIntoPatience(Patience pSource)
        {
            return dal.InsertIntoPatience(pSource);
        }
    }
}
