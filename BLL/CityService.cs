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
        private BaseRepository<City> cityRepository { get; set; }
        public CityService()
        {
            cityRepository = new BaseRepository<City>();
            cityRepository.Context.Database.Log = Console.WriteLine;
        }

        public bool Create()
        {

            var bb = from a in cityRepository.Table select a;
            var cc = from a in cityRepository.Table select a;
            var dd = bb.Union(cc);
            var ff = dd.ToList();

            cityRepository.Create(new City { CountryCode = "2" });
            var i = cityRepository.Context.SaveChanges();
            return true;
        }

        public bool GetList()
        {
            cityRepository.GetList((c) => true);
            return true;
        }
    }
}
