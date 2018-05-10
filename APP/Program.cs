using BLL;
using System;

namespace APP
{
    class Program
    {
        static void Main(string[] args)
        {

            CityService cityService = new CityService();
            cityService.Create();
            Console.WriteLine("Hello World!");
        }
    }
}
