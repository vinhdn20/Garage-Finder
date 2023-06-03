﻿using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IBrandRepository
    {
        List<BrandDTO> GetBrand();
        void Add(BrandDTO brandDTO);
        void Update(BrandDTO brandDTO);
        void Delete(int id);
    }
}
