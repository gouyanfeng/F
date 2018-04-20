using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StoreContext : DbContext
    {

        static string con = "Data Source=localhost;port=3306;Initial Catalog=world;user id=root;password=123456;Character Set=utf8;";
        public StoreContext() : base(con)
        {
         
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<City> Citys { get; set; }


    }
}
