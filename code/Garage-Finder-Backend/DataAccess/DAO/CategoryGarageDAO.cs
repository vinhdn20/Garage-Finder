﻿using GFData.Data;
using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryGarageDAO
    {
        private static CategoryGarageDAO instance = null;
        private static readonly object iLock = new object();

        public CategoryGarageDAO()
        {

        }

        public static CategoryGarageDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryGarageDAO();
                    }
                    return instance;
                }
            }
        }

        public void Add(CategoryGarage categoryGarage)
        {
            try
            {
                var db = new GFDbContext();
                db.CategoryGarage.Add(categoryGarage);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}