using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstractions
{
    public interface IDbInitializer
    {
        public void InitializeDb();
    }
}
