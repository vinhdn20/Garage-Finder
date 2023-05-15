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
    internal class RoleNameRepository : IRoleNameRepository
    {
        public RoleNameDTO GetUserRole(int userId)
        {
            return Mapper.mapToDTO(RoleDAO.Instance.FindById(userId));
        }
    }
}
