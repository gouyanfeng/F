using DAL;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CityService
    {
        public bool Create()
        {
            new CityRepository().Create(new City { CountryCode = "2" });
            DbContextFactory.GetCurrentDbContext()
            return true;
        }

        public bool GetList()
        {
            var list = new CityRepository().GetList(o => true);
            return true;
        }
    }
}
