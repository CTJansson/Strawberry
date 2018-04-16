using ProjectStrawberry.DAL;
using ProjectStrawberry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Generators
{
    public class CharacterModelGenerator
    {
        // generate model then return
        public CharacterCombatModel ModelizeCharacter(int id)
        {
            var ctx = new ProjectStrawberryEntities();

            var character = ctx.Characters.FirstOrDefault(c => c.Id == id);

            var modelCharacter = new CharacterCombatModel();
            modelCharacter.Alive = character.Alive;
            modelCharacter.Axe = character.Axe;
            modelCharacter.Block = character.Block;
            modelCharacter.Dagger = character.Dagger;
            modelCharacter.Evasion = character.Evasion;
            modelCharacter.Experience = character.Experience;
            modelCharacter.Gold = character.Gold;
            modelCharacter.Health = character.Health;
            modelCharacter.Id = character.Id;
            modelCharacter.Mace = character.Mace;
            modelCharacter.Name = character.Name;
            modelCharacter.Parry = character.Parry;
            modelCharacter.Polearm = character.Polearm;
            modelCharacter.Quickness = character.Quickness;
            modelCharacter.Spear = character.Spear;
            modelCharacter.Stamina = character.Stamina;
            modelCharacter.Strength = character.Strength;
            modelCharacter.Sword = character.Sword;
            modelCharacter.Vitality = character.Vitality;
            modelCharacter.OffHand = new OffHandModel() { shld = null, wep = null };

            List<ArmorModel> equipment = new List<ArmorModel>();

            foreach (var e in character.EquipmentCharacters)
            {
                if (e.Armor != null)
                {
                    if (e.Equipped)
                    {
                        equipment.Add(new ArmorModel
                        {
                            Name = e.Armor.Name,
                            ArmorValue = e.Armor.ArmorValue,
                            ArmorTypeId = e.Armor.ArmorTypeId,
                            ModelArmorType = new ArmorTypeModel() { Name = e.Armor.ArmorType.Name },
                        });
                    }
                }
                else if (e.Shield != null)
                {
                    if (e.OffHandEquipped)
                    {
                        modelCharacter.OffHand.shld = new ShieldModel
                        {
                            BlockValue = e.Shield.BlockValue,
                            Name = e.Shield.Name,
                            ReqShieldMastery = e.Shield.ReqShieldMastery,
                        };
                    }
                }
                else if (e.Weapon != null)
                {
                    if (e.MainHandEquipped)
                    {
                        modelCharacter.MainHand = new WeaponModel
                        {
                            MinimumDamage = e.Weapon.MinimumDamage,
                            MaximumDamage = e.Weapon.MaximumDamage,
                            Name = e.Weapon.Name,
                            WeaponType = new WeaponTypeModel { Id = e.Weapon.WeaponType.Id, Name = e.Weapon.WeaponType.Name },   
                            ReqWeaponMastery = e.Weapon.ReqWeaponMastery,  
                            TwoHanded = e.Weapon.TwoHanded,                   
                        };

                    }
                    else if (e.OffHandEquipped)
                    {
                        modelCharacter.OffHand.wep = new WeaponModel
                        {
                            MinimumDamage = e.Weapon.MinimumDamage,
                            MaximumDamage = e.Weapon.MaximumDamage,
                            Name = e.Weapon.Name,
                            WeaponType = new WeaponTypeModel { Id = e.Weapon.WeaponType.Id, Name = e.Weapon.WeaponType.Name },
                            ReqWeaponMastery = e.Weapon.ReqWeaponMastery,
                            TwoHanded = e.Weapon.TwoHanded,
                        };
                    }
                }
            }

            modelCharacter.Equipment = equipment;


            return modelCharacter;
        }

        // update character to DB
        public void UpdateCharacter(CharacterCombatModel model)
        {
            var ctx = new ProjectStrawberryEntities();

            var character = ctx.Characters.FirstOrDefault(c => c.Id == model.Id);
            character.Alive = model.Alive;
            character.Gold = model.Gold;
            character.Health = model.Health;
            character.Level = model.Level;

            ctx.SaveChanges();
        }
    }

    public class CharacterCombatModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Stamina { get; set; }
        public int Strength { get; set; }
        public int Quickness { get; set; }
        public int Block { get; set; }
        public int Evasion { get; set; }
        public int Parry { get; set; }
        public int Vitality { get; set; }
        public int Health { get; set; }
        public int Sword { get; set; }
        public int Mace { get; set; }
        public int Dagger { get; set; }
        public int Spear { get; set; }
        public int Axe { get; set; }
        public int Polearm { get; set; }
        public bool Alive { get; set; }

        public GenderModel Gender { get; set; }
        public ClassModel Class { get; set; }

        public List<ArmorModel> Equipment { get; set; }

        public WeaponModel MainHand { get; set; }
        public OffHandModel OffHand { get; set; }
    }

}