using Baby.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyBirth
{
    /// <summary>
    /// DeleteByBIDapi 的摘要说明
    /// </summary>
    public class DeleteByBIDapi : IHttpHandler
    {
        BLLInfo bll = new BLLInfo();
        public void ProcessRequest(HttpContext context)
        {
            string bid = context.Request["BID"];
            if (!bll.Delete(bid))
            {
                context.Response.Write("删除失败");
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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