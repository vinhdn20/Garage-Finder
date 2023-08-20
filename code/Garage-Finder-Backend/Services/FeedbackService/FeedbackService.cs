using AutoMapper;
using DataAccess.DTO.Feedback;
using GFData.Models.Entity;
using Mailjet.Client.Resources;
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
        private readonly IOderService _orderRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public FeedbackService(IFeedbackRepository feedbackRepository, IOderService orderRepository,
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
                throw new Exception("Số sao không được lớn hơn 5 và bé hơn 1");
            }
            
            var orders = _orderRepository.GetAllOrdersByUserId(userId);
            if (!orders.Any(x => x.GFOrderID == addFeedback.GFOrderID))
            {
                throw new Exception($"Can not find order with id = {addFeedback.GFOrderID}");
            }
            var order = orders.Find(x => x.GFOrderID.Equals(addFeedback.GFOrderID));
            var feebbacks = _feedbackRepository.GetListByGarage(order.GarageID);
            if(feebbacks.Any(x => x.OrderID == order.OrderID))
            {
                var fb = feebbacks.FirstOrDefault(x => x.OrderID == order.OrderID);
                fb.Star = addFeedback.Star;
                fb.Content = addFeedback.Content;
                _feedbackRepository.Update(fb);
                return;
            }
            if (feebbacks.Count >= orders.Count)
            {
                throw new Exception("Đã quá số lượt đánh giá có thể.");
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
                fbDTO.Name = user.Name;
                feedbackDTOs.Add(fbDTO);
            }
            return feedbackDTOs;
        }

        public FeedbackDTO GetFeedbackByGFOrderId(int gfId, int userId)
        {
            var order = _orderRepository.GetOrderByGFId(gfId);
            var feebbacks = _feedbackRepository.GetListByGarage(order.GarageID);
            if (feebbacks.Any(x => x.OrderID == order.OrderID))
            {
                var fb = feebbacks.FirstOrDefault(x => x.OrderID == order.OrderID);
                var fbDTO = _mapper.Map<FeedbackDTO>(fb);
                var car = _carRepository.GetCarById(order.CarID);
                var user = _usersRepository.GetUserByID(car.UserID);
                fbDTO.LinkImage = user.LinkImage;
                return fbDTO;
            }
            else
            {
                throw new Exception("Không tìm thấy lịch đặt này trong lịch sử sửa chữa");
            }
        }
    }
}
