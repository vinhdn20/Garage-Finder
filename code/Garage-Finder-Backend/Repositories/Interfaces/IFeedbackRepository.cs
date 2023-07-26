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
        List<Feedback> GetAll();
        List<Feedback> GetListByGarage(int id);
        List<Feedback> GetListByUserId(int userId);
        void Add(Feedback feedback);
        void Update(Feedback feedback);

    }
}
