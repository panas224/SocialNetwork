using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.CassandraModels
{
    class UserThread
    {
        public string UserName { get; set; }
        public string text { get; set; }
        public bool like { get; set;}
        public bool dislike { get; set; }

    }
}
