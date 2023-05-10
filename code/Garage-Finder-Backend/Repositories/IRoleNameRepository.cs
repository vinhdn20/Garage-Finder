using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoleNameRepository
    {
        RoleNameDTO GetUserRole(int userId);
    }
}
