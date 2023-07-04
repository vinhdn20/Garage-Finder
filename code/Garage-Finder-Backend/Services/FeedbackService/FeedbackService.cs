using AutoMapper;
using DataAccess.DTO.Feedback;
using GFData.Models.Entity;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public FeedbackService(IFeedbackRepository feedbackRepository, IOrderRepository orderRepository,
            IMapper mapper, IUsersRepository usersRepository, ICarRepository carRepository) 
        {
            _feedbackRepository = feedbackRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _usersRepository = usersRepository;
            _carRepository = carRepository;
        }

        public void AddFeedback(AddFeedbackDTO addFeedback, int userId)
        {
            if(addFeedback.Star > 5 || addFeedback.Star < 1)
            {
                throw new Exception("Star not valid");
            }
            
            var orders = _orderRepository.GetAllOrdersByUserId(userId);
            if (!orders.Any(x => x.GFOrderID == addFeedback.GFOrderID))
            {
                throw new Exception($"Can not find order with id = {addFeedback.GFOrderID}");
            }
            var order = orders.Find(x => x.GFOrderID.Equals(addFeedback.GFOrderID));
            var feebbacks = _feedbackRepository.GetListByGarage(order.GarageID);
            if(feebbacks.Count >= orders.Count)
            {
                throw new Exception("Can not add order");
            }
            Feedback feedback = new Feedback() 
            {
                DateTime = DateTime.UtcNow,
                Content = addFeedback.Content,
                OrderID = order.OrderID,
                Star = addFeedback.Star
            };

            _feedbackRepository.Add(feedback);
        }

        public List<FeedbackDTO> GetFeedbackByGarage(int garageId)
        {
            List<FeedbackDTO> feedbackDTOs = new List<FeedbackDTO>();
            var feedbacks = _feedbackRepository.GetListByGarage(garageId);
            foreach (var fb in feedbacks)
            {
                var order = _orderRepository.GetOrderById(fb.OrderID);
                var car = _carRepository.GetCarById(order.CarID);
                var user = _usersRepository.GetUserByID(car.UserID);
                var fbDTO = _mapper.Map<FeedbackDTO>(fb);
                fbDTO.LinkImage = user.LinkImage;
                feedbackDTOs.Add(fbDTO);
            }
            return feedbackDTOs;
        }
    }
}
