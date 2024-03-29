﻿using AutoMapper;
using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.DTO.Garage;
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
            return FavoriteListDAO.Instance.GetList(id).Select(p => _mapper.Map<GFData.Models.Entity.Garage, GarageDTO>(p)).Where(x => x.Status == Constants.GARAGE_ACTIVE).ToList();
        }


        public void Add(FavoriteListDTO favoriteList)
        {
            FavoriteListDAO.Instance.SaveList(_mapper.Map<FavoriteListDTO, FavoriteList>(favoriteList));
        }

        public void Delete(int garageId, int userId)
        {
            FavoriteListDAO.Instance.DeleteByGarageId(garageId, userId);
        }
    }
}
