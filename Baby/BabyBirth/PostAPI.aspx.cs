using Baby.BLL;
using Baby.Common;
using Baby.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BabyBirth
{
    public partial class PostAPI : System.Web.UI.Page
    {
        BLLInfo bll = new BLLInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            InsertInfo();
        }
        private void InsertInfo()
        {
            Patience p = new Patience();
            string json = "";
            json = Request.Form["patient"];
            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            //将json字符串转换为Patience
            p = JSONConvert.Json2Object<Patience>(json);

            json = "写入成功";
            if (!bll.InsertIntoPatience(p))
            {
                json = "写入失败，请确认数据无误";
                Response.Write(json);
            }
            Response.Write(json);
        }

    }
}