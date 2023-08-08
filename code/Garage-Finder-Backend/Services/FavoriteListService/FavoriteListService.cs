using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Garage;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FavoriteListService
{
    public class FavoriteListService : IFavoriteListService
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteListRepository _favoriteListRepository;
        public FavoriteListService(IMapper mapper, IFavoriteListRepository favoriteListRepository)
        {
            _mapper = mapper;
            _favoriteListRepository = favoriteListRepository;
        }
        public List<GarageDTO> GetListByUser(int id)
        {
            return _favoriteListRepository.GetListByUser(id).ToList();
        }


        public void Add(FavoriteListDTO favoriteList)
        {
            _favoriteListRepository.Add(favoriteList);
        }

        public void Delete(int garageId, int userId)
        {
            _favoriteListRepository.Delete(garageId, userId);
        }
    }
}
