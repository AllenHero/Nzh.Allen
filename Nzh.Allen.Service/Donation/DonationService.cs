using Nzh.Allen.IRepository;
using Nzh.Allen.IService;
using Nzh.Allen.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Service
{
    public class DonationService : BaseService<DonationModel>, IDonationService
    {
        public IDonationRepository DonationRepository { get; set; }

        public dynamic GetListByFilter(DonationModel filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.Name))
            {
                _where += " and Name=@Name";
            }
            return GetListByFilter(filter, pageInfo, _where);
        }

        public IEnumerable<DonationModel> GetSumPriceTop(int num)
        {
            return DonationRepository.GetSumPriceTop(num);
        }

        public DonationModel GetConsoleNumShow()
        {
            return DonationRepository.GetConsoleNumShow();
        }
    }
}
