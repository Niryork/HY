using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Baby.DAL
{
    public class GetStrConn
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConn()
        {
            string strconn = ConfigurationManager.ConnectionStrings["SQLConn"].ConnectionString;

            return strconn;
        }
    }
}
