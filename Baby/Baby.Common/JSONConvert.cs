using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Baby.Common
{
    public class JSONConvert
    {
        /// <summary>
        /// 对象 转 json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Object2Json<T>(T model)
        {
            string json;
            //need quote System.Web.Extensions
            JavaScriptSerializer js = new JavaScriptSerializer();
            json = js.Serialize(model);

            return json;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Json2Object<T>(string json)
        {
            T model = default(T);
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                model = js.Deserialize<T>(json);


                return model;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return model;

            }
        }
    }
}