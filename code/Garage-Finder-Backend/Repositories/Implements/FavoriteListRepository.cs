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

namespace Repositories.Implements
{
    public class FavoriteListRepository : IFavoriteListRepository
    {
        private readonly IMapper _mapper;
        public FavoriteListRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<GarageDTO> GetListByUser(int id)
        {
            return FavoriteListDAO.Instance.GetList(id).Select(p => _mapper.Map<Garage, GarageDTO>(p)).ToList();
        }


        public void Add(FavoriteListDTO favoriteList)
        {
            FavoriteListDAO.Instance.SaveList(_mapper.Map<FavoriteListDTO, FavoriteList>(favoriteList));
        }

        public void Delete(int id)
        {
            FavoriteListDAO.Instance.Delete(id);
        }
    }
}
