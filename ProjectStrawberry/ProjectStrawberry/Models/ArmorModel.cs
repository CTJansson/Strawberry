using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Models
{
    public class ArmorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArmorTypeId { get; set; }
        public int ArmorValue { get; set; }
        public int Weight { get; set; }
        public bool TournamentReward { get; set; }
        public int Price { get; set; }
        public ArmorTypeModel ModelArmorType { get; set; }
    }
}