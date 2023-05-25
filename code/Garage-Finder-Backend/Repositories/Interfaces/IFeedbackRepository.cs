using DataAccess.DTO;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        List<FeedbackDTO> GetListByGarage(int id);
        void Add(FeedbackDTO feedback);
        void Update(FeedbackDTO feedback);

    }
}
