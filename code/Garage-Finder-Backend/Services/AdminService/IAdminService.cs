using DataAccess.DTO.Admin;
using DataAccess.DTO.Garage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AdminService
{
    public interface IAdminService
    {
        public List<UserAdminDTO> GetAllUser();
        public List<GarageDTO> GetGarages();
        public void BanUser(int id);
        public void UnBanUser(int id);
        public void BanGarage(int id);
        public void UnBanGarage(int id);
        public void AcceptGarage(int id);
        public void DeniedGarage(int id);
        public void SetStatusGarage(StatusGarageDTO garage);
        public void SetStatusUser(StatusUserDTO user);
    }
}
