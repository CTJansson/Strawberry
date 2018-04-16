using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Models
{
    public class LeaderboardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool Alive { get; set; }

    }
}