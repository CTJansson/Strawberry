using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Models
{
    public class EquipmentModel
    {
        // Weapons
        public int Id { get; set; }
        public string WeaponName { get; set; }
        public int WeaponId { get; set; }
        public bool MainHandEquipped { get; set; }
        public bool OffHandEquipped { get; set; }
    }
}