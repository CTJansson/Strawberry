using ProjectStrawberry.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Models
{
    public class NpcModel
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public double Stamina { get; set; }
        public double Strength { get; set; }
        public double Quickness { get; set; }
        public double Block { get; set; }
        public double Evasion { get; set; }
        public double Parry { get; set; }
        public double Vitality { get; set; }
        public double Health { get; set; }
        public double Sword { get; set; }
        public double Mace { get; set; }
        public double Dagger { get; set; }
        public double Spear { get; set; }
        public double Axe { get; set; }
        public double Polearm { get; set; }
        public Weapon MainHand { get; set; }
        public Weapon OffHand { get; set; }
        public Armor Head { get; set; }
        public Armor Shoulder { get; set; }
        public Armor Chest { get; set; }
        public Armor Gloves { get; set; }
        public Armor Legs { get; set; }
        public Armor Boots { get; set; }
        public List<Armor> ArmorList { get; set; }
    }
}