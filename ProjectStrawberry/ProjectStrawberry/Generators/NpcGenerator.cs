using ProjectStrawberry.DAL;
using ProjectStrawberry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Generators
{
    public class NpcGenerator
    {
        private int npcAttribute = 0;
        private int npcLevel = 0;
        private string firstName;
        private string lastName;
        NameGenerator generator;
        private ProjectStrawberryEntities ctx;
        private CharacterCombatModel model;

        public NpcGenerator(CharacterCombatModel model, int firstIndex, int lastIndex)
        {
            this.model = model;
            generator = new NameGenerator();
            firstName = generator.GenerateFirstName(firstIndex);
            lastName = generator.GenerateLastName(lastIndex);
        }

        private double stamina = 0;
        private double strength = 0;
        private double quickness = 0;
        private double block = 0;
        private double evasion = 0;
        private double parry = 0;
        private double vitality = 0;
        private double sword = 0;
        private double mace = 0;
        private double dagger = 0;
        private double spear = 0;
        private double axe = 0;
        private double polearm = 0;

        public CharacterCombatModel GenerateMirrorNpc()
        {
            ctx = new ProjectStrawberryEntities();

            npcLevel = model.Level;
            npcAttribute = 60 + (npcLevel - 1) * 15;
            var mainHandWeaponType = model.MainHand.WeaponType.Name.ToLower();

            if (model.MainHand != null)
                npcAttribute -= model.MainHand.ReqWeaponMastery;

            if (model.OffHand.shld != null)
            {
                vitality = Math.Round(npcAttribute * 0.50);
                strength = Math.Round(npcAttribute * 0.25);
                block = Math.Round(npcAttribute * 0.25);
            }
            else if (mainHandWeaponType == "axe")
            {
                vitality = Math.Round(npcAttribute * 0.55);
                strength = Math.Round(npcAttribute * 0.25);
                quickness = Math.Round(npcAttribute * 0.10);
                evasion = Math.Round(npcAttribute * 0.05);
                parry = Math.Round(npcAttribute * 0.05);
                axe = model.Axe;
            }
            else if (mainHandWeaponType == "dagger")
            {
                vitality = Math.Round(npcAttribute * 0.50);
                strength = Math.Round(npcAttribute * 0.25);
                evasion = Math.Round(npcAttribute * 0.25);
                dagger = model.Dagger;
            }
            else if (mainHandWeaponType == "mace")
            {
                vitality = Math.Round(npcAttribute * 0.40);
                strength = Math.Round(npcAttribute * 0.40);
                parry = Math.Round(npcAttribute * 0.20);
                mace = model.Mace;
            }
            else if (mainHandWeaponType == "polearm")
            {
                vitality = Math.Round(npcAttribute * 0.50);
                strength = Math.Round(npcAttribute * 0.35);
                evasion = Math.Round(npcAttribute * 0.15);
                polearm = model.Polearm;
            }
            else if (mainHandWeaponType == "spear")
            {
                vitality = Math.Round(npcAttribute * 0.50);
                strength = Math.Round(npcAttribute * 0.30);
                quickness = Math.Round(npcAttribute * 0.20);
                spear = model.Spear;
            }
            else if (mainHandWeaponType == "sword")
            {
                vitality = Math.Round(npcAttribute * 0.45);
                strength = Math.Round(npcAttribute * 0.30);
                parry = Math.Round(npcAttribute * 0.25);
                sword = model.Sword;
            }
            else
            {
                vitality = Math.Round(npcAttribute * 0.65);
                strength = Math.Round(npcAttribute * 0.35);
            }

            var npc = new CharacterCombatModel()
            {
                Name = firstName + " " + lastName,
                Vitality = (int)vitality,
                Axe = (int)axe,
                Block = (int)block,
                Dagger = (int)dagger,
                Evasion = (int)evasion,
                Health = (int)vitality,
                Level = npcLevel,
                Mace = (int)mace,
                Parry = (int)parry,
                Polearm = (int)polearm,
                Quickness = (int)quickness,
                Spear = (int)spear,
                Stamina = (int)stamina,
                Strength = (int)strength,
                Sword = (int)sword,
            };

            var fist = new WeaponModel()
            {
                MinimumDamage = 0,
                MaximumDamage = 0,
                Name = "fist",
                TwoHanded = false,
                WeaponType = new WeaponTypeModel() { Name = "else" },
            };

            var skin = new ArmorModel()
            {
                ArmorValue = 0,
                Name = "skin",
            };

            if (model.MainHand != null)
                npc.MainHand = model.MainHand;
            else
                npc.MainHand = fist;

            if (model.OffHand != null && !model.MainHand.TwoHanded)
                npc.OffHand = model.OffHand;

            npc.Equipment = model.Equipment;
            return npc;
        }
    }
}