using DataAccess.DTO.Garage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageGarageService
{
    public interface IImageGarageService
    {
        void AddImageGarage(ImageGarageDTO imageGarageDTO);
        void RemoveImageGarage(int id);
    }
}
