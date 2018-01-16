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
    /// Informationapi 的摘要说明
    /// </summary>
    public class Informationapi : IHttpHandler
    {
        BLLInfo bll = new BLLInfo();
        public void ProcessRequest(HttpContext context)
        {
            string json = context.Request.Form["patient"];

            //将提交的信息转换为patience对象 
            Patience p = JSONConvert.Json2Object<Patience>(json);
            if (p == null)
            {
                return;
            }
            json = "写入成功";

            if (!bll.InsertIntoPatience(p))
            {
                json = "写入失败，请确认数据无误";
                context.Response.Write(json);
            }
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