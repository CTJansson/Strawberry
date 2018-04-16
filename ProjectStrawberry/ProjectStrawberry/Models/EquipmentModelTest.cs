using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Models
{
    public class EquipmentModelTest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? WeaponId { get; set; }
        public WeaponModel ModelWeapon { get; set; }
        public int? ArmorId { get; set; }
        public ArmorModel ModelArmor { get; set; }
        public int? ShieldId { get; set; }
        public ShieldModel ModelShield { get; set; }

        public bool MainHandEquipped { get; set; }
        public bool OffHandEquipped { get; set; }

        public bool Equipped { get; set; }
    }
}