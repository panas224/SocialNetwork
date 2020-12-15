using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Apache_Cassandra
{
    public interface IPosts
    {
        void CreatePost {ISession session}
        void DeletePost { ISession session }
        void UpdatePost { ISession session }
    }
}
