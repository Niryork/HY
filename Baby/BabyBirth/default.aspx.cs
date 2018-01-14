using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Baby.Model;
using Baby.BLL;
using Baby.Common;
namespace BabyBirth
{
    public partial class _default : System.Web.UI.Page
    {
        BLLInfo bll = new BLLInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetInformation();
        }

        private void InsertInfo()
        {
            //Patience p = new Patience();
            
        }

        /// <summary>
        /// GVInfo Bind data here
        /// </summary>
        private void GetInformation()
        {
            List<Information> list = bll.GetInfo();
            string json = JSONConvert.Object2Json(list);
            Response.Write(json);
        }

    }
}