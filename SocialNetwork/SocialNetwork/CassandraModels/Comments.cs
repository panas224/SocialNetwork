using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.CassandraModels
{
    public class Comments
    {
        public string Comments { get; set; }
        public Guid PostOwnerId { get; set; }
    }
}
