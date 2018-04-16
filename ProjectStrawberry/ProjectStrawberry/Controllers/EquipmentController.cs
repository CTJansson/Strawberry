using Microsoft.AspNet.Identity;
using ProjectStrawberry.DAL;
using ProjectStrawberry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectStrawberry.Controllers
{
    public class EquipmentController : Controller
    {
        // Equip & Unequip Shield
        public ActionResult Index()
        {
            return PartialView("_ViewEquipment");
        }

        //public ActionResult GetBoughtWeapons()
        //{
        //    var weaponEquipment = new List<Models.EquipmentModel>();
        //    var ctx = new ProjectStrawberryEntities();
        //    string currentUserId = User.Identity.GetUserId();

        //    foreach (var charEquipment in ctx.WeaponCharacters.Where(wc => wc.Character.AccountId == currentUserId))
        //    {
        //        weaponEquipment.Add(new Models.EquipmentModel { Id = charEquipment.Id, MainHandEquipped = charEquipment.MainHandEquipped, OffHandEquipped = charEquipment.OffHandEquipped, WeaponId = charEquipment.WeaponId, WeaponName = ctx.Weapons.FirstOrDefault(w => w.Id == charEquipment.WeaponId).Name });
        //    }

        //    return PartialView("_BoughtWeapons", weaponEquipment);
        //}

        public ActionResult GetEquipment()
        {
            var equipment = new List<EquipmentModelTest>();
            var ctx = new ProjectStrawberryEntities();
            string currentUserId = User.Identity.GetUserId();

            foreach (var item in ctx.EquipmentCharacters.Where(ec => ec.Character.AccountId == currentUserId && ec.Character.Alive))
            {
                if (item.ArmorId != null)
                {
                    equipment.Add(new EquipmentModelTest { Id = item.Id, ModelArmor = new ArmorModel { Id = item.Armor.Id, Name = item.Armor.Name, ModelArmorType =  new ArmorTypeModel { Id = item.Armor.ArmorType.Id, Name = item.Armor.ArmorType.Name } }, Equipped = item.Equipped });
                }
                else if (item.ShieldId != null)
                {
                    equipment.Add(new EquipmentModelTest { Id = item.Id, ModelShield = ctx.Shields.Select(s => new ShieldModel { Id = s.Id, Name = s.Name, Weight = s.Weight, BlockValue = s.BlockValue }).FirstOrDefault(s => s.Id == item.ShieldId), OffHandEquipped = item.OffHandEquipped });
                }
                else if (item.WeaponId != null)
                {
                    equipment.Add(new EquipmentModelTest { Id = item.Id, MainHandEquipped = item.MainHandEquipped, OffHandEquipped = item.OffHandEquipped, ModelWeapon = new WeaponModel { Id = item.Weapon.Id, Name = item.Weapon.Name, WeaponType = new WeaponTypeModel { Id = item.Weapon.WeaponType.Id, Name = item.Weapon.WeaponType.Name } } });
                }            
            }

            return PartialView("_BoughtEquipment", equipment);
        }

        public ActionResult EquipWeapon(int id, int hand, int weaponId)
        {
            string currentUserId = User.Identity.GetUserId();

            var ctx = new ProjectStrawberryEntities();
            int characterId = ctx.Characters.FirstOrDefault(c => c.AccountId == currentUserId && c.Alive).Id;
            bool doesUserOwnWeapon = ctx.EquipmentCharacters.Any(wc => wc.Id == id && wc.CharacterId == characterId && wc.WeaponId == weaponId);
            bool isMainHandAlreadyEquipped = ctx.EquipmentCharacters.Any(wc => wc.Id == id && wc.CharacterId == characterId && wc.MainHandEquipped == true);
            bool isOffHandAlredyEquipped = ctx.EquipmentCharacters.Any(wc => wc.Id == id && wc.CharacterId == characterId && wc.OffHandEquipped == true); 

            if (!isMainHandAlreadyEquipped && hand == 1 && doesUserOwnWeapon)
            {
                ctx.EquipmentCharacters
                    .FirstOrDefault(wc => wc.Id == id && wc.OffHandEquipped == false)
                    .MainHandEquipped = true;
                ctx.SaveChanges();
            }
            else if (!isOffHandAlredyEquipped && hand == 2 && doesUserOwnWeapon)
            {
                ctx.EquipmentCharacters
                    .FirstOrDefault(wc => wc.Id == id && wc.MainHandEquipped == false)
                    .OffHandEquipped = true;
                ctx.SaveChanges();
            }

            return RedirectToAction("GetEquipment");
        }

        public ActionResult EquipArmor(int id, int slot)
        {
            string currentUserId = User.Identity.GetUserId();

            var ctx = new ProjectStrawberryEntities();
            int characterId = ctx.Characters.FirstOrDefault(c => c.AccountId == currentUserId && c.Alive).Id;

            bool doesUserOwnEqiupment = ctx.EquipmentCharacters.Any(ec => ec.CharacterId == characterId && ec.Id == id);
            bool doesUserHasSlotEquipped = ctx.EquipmentCharacters.Any(ec => ec.CharacterId == characterId && ec.Armor.ArmorType.Id == slot && ec.Equipped);

            if (doesUserOwnEqiupment &! doesUserHasSlotEquipped)
            {
                ctx.EquipmentCharacters.FirstOrDefault(ec => ec.Id == id).Equipped = true;
                ctx.SaveChanges();
            }

            return RedirectToAction("GetEquipment");
        }

        public ActionResult UnequipArmor(int id)
        {
            string currentUserId = User.Identity.GetUserId();

            var ctx = new ProjectStrawberryEntities();
            int characterId = ctx.Characters.FirstOrDefault(c => c.AccountId == currentUserId && c.Alive).Id;

            bool doesUserOwnEqiupment = ctx.EquipmentCharacters.Any(ec => ec.CharacterId == characterId && ec.Id == id && ec.Equipped);

            if (doesUserOwnEqiupment)
            {
                ctx.EquipmentCharacters.FirstOrDefault(ec => ec.Id == id && ec.CharacterId == characterId).Equipped = false;
                ctx.SaveChanges();
            }

            return RedirectToAction("GetEquipment");
        }

        public ActionResult UnequipWeapon(int id, int hand) // Måste säkra denna metod så inte andra spelare ändrar status på vapen andras karaktärer
        {
            var ctx = new ProjectStrawberryEntities();

            if (hand == 1)
            {
                ctx.EquipmentCharacters.FirstOrDefault(wc => wc.Id == id).MainHandEquipped = false;
            }
            else if (hand == 2)
            {
                ctx.EquipmentCharacters.FirstOrDefault(wc => wc.Id == id).OffHandEquipped = false;
            }

            ctx.SaveChanges();

            return RedirectToAction("GetEquipment");
        }
    }
}