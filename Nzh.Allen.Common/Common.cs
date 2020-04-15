using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Common
{
    public class Common
    {
        public static string GuId()
        {
            return Guid.NewGuid().ToString();
        }

        public static string CreateNo()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 10000).ToString(); //生成编号 
            string code = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom;//形如
            return code;
        }
    }
}
