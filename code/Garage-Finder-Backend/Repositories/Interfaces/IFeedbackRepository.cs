using DataAccess.DTO.Feedback;
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
        List<Feedback> GetListByGarage(int id);
        void Add(Feedback feedback);
        void Update(Feedback feedback);

    }
}
