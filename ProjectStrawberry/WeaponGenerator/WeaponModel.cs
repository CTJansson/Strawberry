using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponGenerator
{
    public class WeaponModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WeaponTypeId { get; set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int ReqWeaponMastery { get; set; }
        public WeaponType WeaponType { get; set; }
        public int Weight { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return "#" + Id + " | " + Name + " | " + MinimumDamage + " - " + MaximumDamage + "dmg | " + ReqWeaponMastery + "wp | " + Weight + "kg | " + Price + " glods";
        }
    }
}
