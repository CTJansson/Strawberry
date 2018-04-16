using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Models
{
    [Serializable()]
    public class Round
    {
        public int RoundNr { get; set; }
        public string Log { get; set; }
    }
}