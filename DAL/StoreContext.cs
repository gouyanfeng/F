using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StoreContext : DbContext
    {

        static string con = "Data Source=192.168.41.202;Initial Catalog=TEST;User ID=sa;Password=111111";
        public StoreContext() : base(con)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //生成表名不用复数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<City> Citys { get; set; }


    }
}
