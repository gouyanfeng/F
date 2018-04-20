using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbContextFactory
    {
        public static StoreContext GetCurrentDbContext()
        {
   
            StoreContext currentContext = CallContext.GetData("CurrentDbContext") as StoreContext;
            if (currentContext == null)
            {
                currentContext = new StoreContext();
                CallContext.SetData("CurrentDbContext", currentContext);
            }
            return currentContext;
        }
    }
}
