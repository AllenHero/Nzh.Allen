using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Model
{
    public class Pager
    {
        public static dynamic Paging(IEnumerable<dynamic> list, long total)
        {
            return new { code = 0, msg = "", count = total, data = list };
        }
    }
}
