using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Apache_Cassandra
{
    public interface IUserThread
    {
        void SyncUserThread { ISession session,Guid postOwnerId }
    }
}
