﻿using DietManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietManager.DataLayer
{
    public class DbSession
    {
        private static DbSession _instance;
        private AppDbContext _dbContext;


        private DbSession()
        {
            _dbContext = new AppDbContext();
        }

        public static DbSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbSession();
                }
                return _instance;
            }
        }

        public AppDbContext GetAppDbcontext ()
        {
            return Instance._dbContext;
        }
    }
}
