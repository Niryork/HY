using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Baby.Model;
using Baby.BLL;
using Baby.Common;

namespace BabyBirth.api
{
    /// <summary>
    /// GetHospitalapi 的摘要说明
    /// </summary>
    public class GetHospitalapi : IHttpHandler
    {
        BLLInfo bll = new BLLInfo();
        public void ProcessRequest(HttpContext context)
        {
            List<Hospital> list = bll.GetHospital();
            string json = JSONConvert.Object2Json(list);

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}