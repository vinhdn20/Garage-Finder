using DataAccess.DTO;
using DataAccess.DTO.RequestDTO.Garage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IFavoriteListRepository
    {
        void Add(FavoriteListDTO favoriteList);
        void Delete(int garageId, int userId);
        List<GarageDTO> GetListByUser(int id);
    }
}
