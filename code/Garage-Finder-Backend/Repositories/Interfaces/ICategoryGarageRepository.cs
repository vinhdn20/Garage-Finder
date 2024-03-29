﻿using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICategoryGarageRepository
    {
        void Add(CategoryGarageDTO categoryGarageDTO);
        void Remove(int id);
        CategoryGarageDTO GetById(int id);
    }
}
