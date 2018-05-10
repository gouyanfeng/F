using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPNET
{
    class Program
    {
        static void Main(string[] args)
        {
            CityService cityService = new CityService();
            cityService.Create();
            Console.ReadKey();
        }
    }
}
