using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Models
{
    public class WeaponModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WeaponTypeId { get; set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int Weight { get; set; }
        public int ReqWeaponMastery { get; set; }
        public WeaponTypeModel WeaponType { get; set; }
        public bool TwoHanded { get; set; }
        public decimal Price { get; set; }
    }
}