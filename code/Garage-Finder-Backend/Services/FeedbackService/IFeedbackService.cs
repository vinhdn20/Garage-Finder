using DataAccess.DTO.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FeedbackService
{
    public interface IFeedbackService
    {
        void AddFeedback(AddFeedbackDTO addFeedback, int userId);
        List<FeedbackDTO> GetFeedbackByGarage(int garageId);
        FeedbackDTO GetFeedbackByGFOrderId(int gfId, int userId);
    }
}
