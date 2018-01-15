using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Baby.BLL;
using Baby.Common;
using Baby.Model;
namespace BabyBirth.api
{
    /// <summary>
    /// UptByBIDapi 的摘要说明
    /// </summary>
    public class UptByBIDapi : IHttpHandler
    {
        BLLInfo bll = new BLLInfo();
        public void ProcessRequest(HttpContext context)
        {
            string json = "";
            string bid = context.Request["BID"];
            Patience p = bll.GetPatient(bid);
            json = JSONConvert.Object2Json<Patience>(p);

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