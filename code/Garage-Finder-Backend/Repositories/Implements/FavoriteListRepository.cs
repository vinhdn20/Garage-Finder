using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.Util;
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
        public List<FavoriteListDTO> GetListByUser(int id)
        {
            return FavoriteListDAO.Instance.GetList().Where(c => c.UserID == id).Select(p => Mapper.mapToDTO(p)).ToList();
        }


        public void Add(FavoriteListDTO favoriteList)
        {
            FavoriteListDAO.Instance.SaveList(Mapper.mapToEntity(favoriteList));
        }

        public void Delete(int id)
        {
            FavoriteListDAO.Instance.Delete(id);
        }
    }
}
