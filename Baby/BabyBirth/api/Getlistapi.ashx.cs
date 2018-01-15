using Baby.BLL;
using Baby.Common;
using Baby.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyBirth
{
    /// <summary>
    /// Getlistapi 的摘要说明
    /// </summary>
    public class Getlistapi : IHttpHandler
    {
        BLLInfo bll = new BLLInfo();
        public void ProcessRequest(HttpContext context)
        {
            List<V_Info> list = bll.GetV_Info();
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