using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Models
{
    public class MarketEquipment
    {
        public string Name { get; set; }
        public int? ArmorId { get; set; }
        public string EquipmentType { get; set; }
        public int? ShieldId { get; set; }
        public int? WeaponId { get; set; }
        public int AbsorbValue { get; set; }
        public int Price { get; set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int Weight { get; set; }

        public int Mastery { get; set; }
        public bool TournamentItem { get; set; }
    }
}