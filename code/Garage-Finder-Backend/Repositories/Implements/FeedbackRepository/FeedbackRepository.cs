using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO;
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
        public List<FeedbackDTO> GetListByGarage(int id)
        {
            return FeedbackDAO.Instance.GetList().Where(c => c.GarageID == id).Select(p => _mapper.Map<Feedback, FeedbackDTO>(p)).ToList();
        }


        public void Add(FeedbackDTO feedback)
        {
            FeedbackDAO.Instance.SaveFeedback(_mapper.Map<FeedbackDTO, Feedback>(feedback));
        }

        public void Update(FeedbackDTO feedback)
        {
            FeedbackDAO.Instance.UpdateFeedback(_mapper.Map<FeedbackDTO, Feedback>(feedback));
        }
    }
}
