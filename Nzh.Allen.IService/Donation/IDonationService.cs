using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.IService
{
    public interface IDonationService : IBaseService<DonationModel>
    {
        /// <summary>
        /// 获得捐赠排行榜
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        IEnumerable<DonationModel> GetSumPriceTop(int num);
        /// <summary>
        /// 获得控制台显示数字
        /// </summary>
        /// <returns></returns>
        DonationModel GetConsoleNumShow();
    }
}
