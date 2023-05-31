using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class SubscribeDAO
    {
        private static SubscribeDAO instance = null;
        private static readonly object iLock = new object();
        public SubscribeDAO()
        {

        }

        public static SubscribeDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new SubscribeDAO();
                    }
                    return instance;
                }
            }
        }
    }
}
