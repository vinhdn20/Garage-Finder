using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Util;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public List<FeedbackDTO> GetListByGarage(int id)
        {
            return FeedbackDAO.Instance.GetList().Where(c => c.GarageID == id).Select(p => Mapper.mapToDTO(p)).ToList();
        }


        public void Add(FeedbackDTO feedback)
        {
            FeedbackDAO.Instance.SaveFeedback(Mapper.mapToEntity(feedback));
        }

        public void Update(FeedbackDTO feedback)
        {
            FeedbackDAO.Instance.UpdateFeedback(Mapper.mapToEntity(feedback));
        }
    }
}
