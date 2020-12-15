using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Apache_Cassandra
{
    public interface IComments
    {
        public void AddComment { ISession session }
    }
}
