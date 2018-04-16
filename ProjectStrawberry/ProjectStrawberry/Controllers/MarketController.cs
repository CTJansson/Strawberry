using Microsoft.AspNet.Identity;
using ProjectStrawberry.DAL;
using ProjectStrawberry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ProjectStrawberry.Controllers
{
    public class MarketController : Controller
    {
        // 
        public ActionResult Index()
        {
            DAL.ProjectStrawberryEntities ctx = new DAL.ProjectStrawberryEntities();
            List<WeaponType> weaponTypes = ctx.WeaponTypes.ToList();

            return PartialView("Market", weaponTypes);
        }

        [HttpGet]
        public ActionResult GetWeapons(int weaponTypeId)
        {
            DAL.ProjectStrawberryEntities ctx = new DAL.ProjectStrawberryEntities();

            List<Weapon> weapons = ctx.Weapons.Where(w => w.WeaponTypeId == weaponTypeId).OrderBy(w => w.Price).ToList();

            return PartialView("_Weapons", weapons);
        }

        [HttpGet]
        public ActionResult GetEquipment()
        {
            var ctx = new ProjectStrawberryEntities();

            List<MarketEquipment> equipment = new List<MarketEquipment>();

            foreach (var armor in ctx.Armors)
            {
                if (armor.TourmanemntReward == false)
                {
                    equipment.Add(new MarketEquipment { ArmorId = armor.Id, Name = armor.Name, Price = armor.Price, AbsorbValue = armor.ArmorValue, Weight = armor.Weight, EquipmentType = armor.ArmorType.Name });
                }
            }

            foreach (var weapon in ctx.Weapons)
            {
                if (weapon.TournamentReward == false)
                {
                    equipment.Add(new MarketEquipment { WeaponId = weapon.Id, Name = weapon.Name, Price = (int)weapon.Price, MinimumDamage = weapon.MinimumDamage, MaximumDamage = weapon.MaximumDamage, Weight = weapon.Weight, EquipmentType = weapon.WeaponType.Name });
                }
            }

            foreach (var shield in ctx.Shields)
            {
                if (shield.TourmanemntReward == false)
                {
                    equipment.Add(new MarketEquipment { ShieldId = shield.Id, Name = shield.Name, Price = shield.Price, AbsorbValue = shield.BlockValue, Weight = shield.Weight, EquipmentType = "Shield" });
                }             
            }

            return PartialView("_BuyEquipment", equipment);
        }

        [Route("Market/BuyArmor/{armorId}")]
        public string BuyArmor(string id)
        {
            int armorId = -1;
            int.TryParse(id, out armorId);

            if (armorId > 0)
            {
                var ctx = new ProjectStrawberryEntities();
                var currentUser = User.Identity.GetUserId();

                var armor = ctx.Armors.FirstOrDefault(s => s.Id == armorId);
                var character = ctx.Characters.FirstOrDefault(c => c.AccountId == currentUser && c.Alive);

                if (armor != null && character != null && character.Gold - armor.Price >= 0)
                {
                    ctx.EquipmentCharacters.Add(new EquipmentCharacter { Armor = armor, Character = character });
                    character.Gold -= armor.Price;
                    ctx.SaveChanges();

                    return "You pay the storeowner " + armor.Price + " glods and recieve " + armor.Name;
                }
                else
                {
                    return "The storeowner looks at you with an eyebrow raised";
                }
            }
            else
            {
                return "The storeowner looks at you with an eyebrow raised";
            }
        }

        [Route("Market/BuyShield/{shieldId}")]
        public string BuyShield(string id)
        {
            int shieldId = -1;
            int.TryParse(id, out shieldId);

            if (shieldId > 0)
            {
                var ctx = new ProjectStrawberryEntities();
                var currentUser = User.Identity.GetUserId();

                var shield = ctx.Shields.FirstOrDefault(s => s.Id == shieldId);
                var character = ctx.Characters.FirstOrDefault(c => c.AccountId == currentUser && c.Alive);

                if (shield != null && character != null && character.Gold - shield.Price >= 0)
                {
                    ctx.EquipmentCharacters.Add(new EquipmentCharacter { Shield = shield, Character = character});
                    character.Gold -= shield.Price;
                    ctx.SaveChanges();

                    return "You pay the storeowner " + shield.Price + " glods and recieve " +shield.Name;
                }
                else
                {
                    return "The storeowner looks at you with an eyebrow raised";
                }
            }
            else
            {
                return "The storeowner looks at you with an eyebrow raised";
            }
        }

        [Route("Market/BuyWeapon/{weaponId}")]
        public string BuyWeapon(string id)
        {
            int weaponId = -1;
            int.TryParse(id, out weaponId);

            if (weaponId > 0)
            {
                var ctx = new ProjectStrawberryEntities();
                var currentUser = User.Identity.GetUserId();

                var weapon = ctx.Weapons.FirstOrDefault(s => s.Id == weaponId);
                var character = ctx.Characters.FirstOrDefault(c => c.AccountId == currentUser && c.Alive);

                if (weapon != null && character != null && character.Gold - weapon.Price >= 0)
                {
                    ctx.EquipmentCharacters.Add(new EquipmentCharacter { Weapon = weapon, Character = character });
                    character.Gold -= (int)weapon.Price;
                    ctx.SaveChanges();

                    return "You pay the storeowner " + weapon.Price + " glods and recieve " + weapon.Name;
                }
                else
                {
                    return "The storeowner looks at you with an eyebrow raised";
                }
            }
            else
            {
                return "The storeowner looks at you with an eyebrow raised";
            }
        }
    }
}