using DataAccess.DTO;
using DataAccess.DTO.Garage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FavoriteListService
{
    public interface IFavoriteListService
    {
        void Add(FavoriteListDTO favoriteList);
        void Delete(int garageId, int userId);
        List<GarageDTO> GetListByUser(int id);
    }
}
