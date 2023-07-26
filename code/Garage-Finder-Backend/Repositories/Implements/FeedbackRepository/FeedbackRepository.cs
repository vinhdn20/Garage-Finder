using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO.Feedback;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements.FeedbackRepository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMapper _mapper;
        public FeedbackRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<Feedback> GetAll()
        {
            return FeedbackDAO.Instance.GetAll();
        }

        public List<Feedback> GetListByGarage(int id)
        {
            return FeedbackDAO.Instance.GetByGarage(id);
        }

        public List<Feedback> GetListByUserId(int userId)
        {
            return FeedbackDAO.Instance.GetByUserId(userId);
        }

        public void Add(Feedback feedback)
        {
            FeedbackDAO.Instance.SaveFeedback(feedback);
        }

        public void Update(Feedback feedback)
        {
            FeedbackDAO.Instance.UpdateFeedback(feedback);
        }
    }
}
