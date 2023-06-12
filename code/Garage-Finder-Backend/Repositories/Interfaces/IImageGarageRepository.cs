using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IImageGarageRepository
    {
        void AddImageGarage(ImageGarageDTO imageGarageDTO);
    }
}
