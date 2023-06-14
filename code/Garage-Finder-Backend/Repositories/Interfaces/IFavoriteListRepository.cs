using DataAccess.DTO;
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
        void Delete(int id);
        List<GarageDTO> GetListByUser(int id);
    }
}
