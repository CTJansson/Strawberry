using Microsoft.AspNet.Identity;
using ProjectStrawberry.DAL;
using ProjectStrawberry.Generators;
using ProjectStrawberry.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;

namespace ProjectStrawberry.Controllers
{
    public class BattleController : Controller
    {
        public ActionResult Arena()
        {
            var ctx = new ProjectStrawberryEntities();
            string currentUserId = currentUserId = User.Identity.GetUserId();

            if (ctx.Arenas.Any(a => a.Character.AccountId == currentUserId && a.Character.Alive))
            {
                return PartialView("RandomDuel", true);
            }

            return PartialView("Arena");
        }

        public ActionResult RandomDuel()
        {
            var ctx = new ProjectStrawberryEntities();
            string currentUserId = currentUserId = User.Identity.GetUserId();

            if (ctx.Arenas.Any(a => a.Character.Level == ctx.Characters.FirstOrDefault(c => c.AccountId == currentUserId && c.Alive).Level && a.CharacterId != ctx.Characters.FirstOrDefault(c => c.AccountId == currentUserId).Id))
            {
                //Battle();
            }
            else
            {
                if (!ctx.Arenas.Any(a => a.Character.AccountId == currentUserId && a.Character.Alive))
                {
                    ctx.Arenas.Add(new Arena { CharacterId = ctx.Characters.FirstOrDefault(c => c.AccountId == currentUserId && c.Alive).Id, Forfeit = 25, Queued = DateTime.Now });
                    ctx.SaveChanges();
                }
                else
                {
                    return PartialView("RandomDuel", true);
                }
            }
            return PartialView("RandomDuel", false);
        }

        public ActionResult Update()
        {
            string currentUserId = currentUserId = User.Identity.GetUserId();

            var ctx = new ProjectStrawberryEntities();
            //var combatlog = ctx.CombatLogs.FirstOrDefault(c => c.Character.AccountId == currentUserId && c.Character1.AccountId == currentUserId);

            //if (combatlog != null)
            //{
            //    List<Round> list = new List<Round>();

            //    using (MemoryStream ms = new MemoryStream(combatlog.Log))
            //    {
            //        BinaryFormatter bf = new BinaryFormatter();
            //        list = (List<Round>)bf.Deserialize(ms);
            //    }

            //    return PartialView("CombatLog", list);
            //}

            var arenaModel = ctx.Arenas.FirstOrDefault(a => a.Character.AccountId == currentUserId);

            DateTime currentDateTime = DateTime.Now;
            DateTime queuedDateTime = arenaModel.Queued; // if any combatlog.date > queueddatetime show dat

            if (queuedDateTime.AddSeconds(2) <= currentDateTime)
            {
                var combatLog = BattleNPC(arenaModel.CharacterId, arenaModel.Forfeit, null, null);
                ctx.SaveChanges();

                return PartialView("CombatLog", combatLog);
            }

            return PartialView("");
        }

        public List<Round> BattleNPC(int player1Id, int player1Forfeit, int? player2Id, int? player2Forfeit)
        {
            var ctx = new ProjectStrawberryEntities();
            var random = new Random();
            var generate = new CombatTextGenerator();
            var ccm = new CharacterModelGenerator();

            // Generate Player1 model and properties
            var player1 = new CharacterCombatModel();
            player1 = ccm.ModelizeCharacter(player1Id);

            double player1HitChanceMainHand = PlayerHitChance(player1);
            double player1HitChanceOffHand = PlayerHitChance(player1);
            double player1ParryChance = diminishing_returns(player1.Parry, 7.5);
            double player1DodgeChance = diminishing_returns(player1.Evasion, 7.5);
            double player1BlockChance = diminishing_returns(player1.Block, 7.5);

            bool player1HasShield = player1.OffHand.shld != null;
            bool player1HasWeapon = player1.MainHand != null && player1.OffHand.wep != null;

            // Generate Player2 model and properties
            var player2 = new CharacterCombatModel();

            if (player2Id != null)
            {
                player2 = ccm.ModelizeCharacter((int)player2Id);
            }
            else
            {
                var gen = new NpcGenerator(player1, random.Next(0, 47), random.Next(0, 71));
                player2 = gen.GenerateMirrorNpc();
                player2Forfeit = 20;
            }

            double player2HitChanceMainHand = PlayerHitChance(player2);
            double player2HitChanceOffHand = PlayerHitChance(player2);
            double player2ParryChance = diminishing_returns(player2.Parry, 7.5);
            double player2DodgeChance = diminishing_returns(player2.Evasion, 7.5);
            double player2BlockChance = diminishing_returns(player2.Block, 7.5);

            bool player2HasShield = player2.OffHand.shld != null;
            bool player2HasWeapon = player2.MainHand != null && player2.OffHand.wep != null;

            // Combatlog properties
            List<Round> fullFight = new List<Round>();
            int roundNr = 0;
            string roundLog = "";
            string spoiler = "";

            bool keepFighting = true;
            bool player1HasHighestQuickness = player1.Quickness >= player2.Quickness;
            bool player1Won = false;
            bool player2Won = false;

            while (keepFighting)
            {
                int combatflavorindex = 2;

                int damage = 0;
                int bonusDamage = 0;
                int minDamage = 0;
                int maxDamage = 0;

                roundNr++;

                // Player1 # of attacks + Avoidance chance
                bool player1AttackWithMainHand = false;
                bool player1AttackWithOffHand = false;

                AttacksPlayerCanDoThisRound(player1, ref player1AttackWithMainHand, ref player1AttackWithOffHand);

                // Player2 # of attacks.
                bool player2AttackWithMainHand = false;
                bool player2AttackWithOffHand = false;

                AttacksPlayerCanDoThisRound(player2, ref player2AttackWithMainHand, ref player2AttackWithOffHand);

                if (player1HasHighestQuickness)
                {
                    if (keepFighting && player1AttackWithMainHand)
                    {
                        bonusDamage = player1.Strength / 10;
                        minDamage = player1.MainHand.MinimumDamage + bonusDamage;
                        maxDamage = player1.MainHand.MaximumDamage + bonusDamage;

                        var bodyPartId = DiceForBodyPart(random.Next(1, 97), player1.MainHand.WeaponType.Name);
                        var bodyPart = player2.Equipment.FirstOrDefault(e => e.ArmorTypeId == bodyPartId);

                        if (bodyPart == null)
                        {
                            bodyPart = new ArmorModel() { ArmorValue = 0, Name = "skin" };
                        }

                        if (minDamage - bodyPart.ArmorValue < 0)
                            minDamage = 0;
                        else
                            minDamage -= bodyPart.ArmorValue;

                        if (maxDamage - bodyPart.ArmorValue < 0)
                            maxDamage = 0;
                        else
                            maxDamage -= bodyPart.ArmorValue;

                        string action = WhatHappenedThatHit(player1HitChanceMainHand, player2DodgeChance, player2ParryChance, player2BlockChance, player2HasShield, player2HasWeapon, random);

                        if (action == "h")
                        {
                            damage = random.Next(minDamage, maxDamage + 1);
                            roundLog += generate.CombatTextMaleAttacker(player1.Name, player2.Name, damage, bodyPart.Name, random.Next(0, combatflavorindex));
                            player2.Health -= damage;
                        }
                        else if (action == "b")
                        {
                            roundLog += generate.CombatTextBlock(player1.Name, player2.Name, player1.MainHand.Name.ToLower(), player2.OffHand.shld.Name, random.Next(0, 1));
                        }
                        else if (action == "p")
                        {
                            roundLog += generate.CombatTextParry(player1.Name, player2.Name, random.Next(0, 1));
                        }
                        else if (action == "d")
                        {
                            roundLog += generate.CombatTextDodge(player1.Name, player1.MainHand.Name, player2.Name, random.Next(0, 1));
                        }

                        player1AttackWithMainHand = false;
                    }
                    else if (keepFighting && player1AttackWithOffHand)
                    {
                        bonusDamage = player1.Strength / 10;
                        minDamage = player1.OffHand.wep.MinimumDamage + bonusDamage;
                        maxDamage = player1.OffHand.wep.MaximumDamage + bonusDamage;

                        var bodyPartId = DiceForBodyPart(random.Next(1, 97), player1.OffHand.wep.Name);
                        var bodyPart = player2.Equipment.FirstOrDefault(e => e.ArmorTypeId == bodyPartId);

                        if (minDamage - bodyPart.ArmorValue < 0)
                            minDamage = 0;
                        else
                            minDamage -= bodyPart.ArmorValue;

                        if (maxDamage - bodyPart.ArmorValue < 0)
                            maxDamage = 0;
                        else
                            maxDamage -= bodyPart.ArmorValue;

                        string action = WhatHappenedThatHit(player1HitChanceMainHand, player2DodgeChance, player2ParryChance, player2BlockChance, player2HasShield, player2HasWeapon, random);

                        if (action == "h")
                        {
                            damage = random.Next(minDamage, maxDamage + 1);
                            roundLog += generate.CombatTextMaleAttacker(player1.Name, player2.Name, damage, bodyPart.Name, random.Next(0, combatflavorindex));
                            player2.Health -= damage;
                        }
                        else if (action == "b")
                        {
                            roundLog += generate.CombatTextBlock(player1.Name, player2.Name, player1.MainHand.Name.ToLower(), player2.OffHand.shld.Name, random.Next(0,1));
                        }
                        else if (action == "p")
                        {
                            roundLog += generate.CombatTextParry(player1.Name, player2.Name, random.Next(0, 1));
                        }
                        else if (action == "d")
                        {
                            roundLog += generate.CombatTextDodge(player1.Name, player1.MainHand.Name, player2.Name, random.Next(0,1));
                        }

                        player1AttackWithOffHand = false;
                    }

                    // Check if Player2 forfeits
                    if (player2.Health <= player2.Vitality * (player2Forfeit / 100))
                    {
                        keepFighting = false;
                        player1Won = true;
                    }

                    if (keepFighting && player2AttackWithMainHand)
                    {
                        bonusDamage = player2.Strength / 10;
                        minDamage = player2.MainHand.MinimumDamage + bonusDamage;
                        maxDamage = player2.MainHand.MaximumDamage + bonusDamage;

                        var bodyPartId = DiceForBodyPart(random.Next(1, 97), player2.MainHand.Name);
                        var bodyPart = player1.Equipment.FirstOrDefault(ec => ec.ArmorTypeId == bodyPartId);

                        if (minDamage - bodyPart.ArmorValue < 0)
                            minDamage = 0;
                        else
                            minDamage -= bodyPart.ArmorValue;

                        if (maxDamage - bodyPart.ArmorValue < 0)
                            maxDamage = 0;
                        else
                            maxDamage -= bodyPart.ArmorValue;

                        string action = WhatHappenedThatHit(player2HitChanceMainHand, player1DodgeChance, player1ParryChance, player1BlockChance, player1HasShield, player1HasWeapon, random);

                        if (action == "h")
                        {
                            damage = random.Next(minDamage, maxDamage + 1);
                            roundLog += generate.CombatTextMaleAttacker(player2.Name, player1.Name, damage, bodyPart.Name, random.Next(0, combatflavorindex));
                            player1.Health -= damage;
                        }
                        else if (action == "b")
                        {
                            roundLog += generate.CombatTextBlock(player2.Name, player1.Name, player2.MainHand.Name.ToLower(), player1.OffHand.shld.Name, random.Next(0, 1));
                        }
                        else if (action == "p")
                        {
                            roundLog += generate.CombatTextParry(player2.Name, player1.Name, random.Next(0, 1));
                        }
                        else if (action == "d")
                        {
                            roundLog += generate.CombatTextDodge(player2.Name, player2.MainHand.Name, player1.Name, random.Next(0, 1));
                        }

                        player2AttackWithMainHand = false;
                    }
                    else if (keepFighting && player2AttackWithOffHand)
                    {
                        bonusDamage = player2.Strength / 10;
                        minDamage = player2.OffHand.wep.MinimumDamage + bonusDamage;
                        maxDamage = player2.OffHand.wep.MaximumDamage + bonusDamage;

                        var bodyPartId = DiceForBodyPart(random.Next(1, 97), player2.OffHand.wep.Name);
                        var bodyPart = player1.Equipment.FirstOrDefault(ec => ec.ArmorTypeId == bodyPartId);

                        if (minDamage - bodyPart.ArmorValue < 0)
                            minDamage = 0;
                        else
                            minDamage -= bodyPart.ArmorValue;

                        if (maxDamage - bodyPart.ArmorValue < 0)
                            maxDamage = 0;
                        else
                            maxDamage -= bodyPart.ArmorValue;

                        string action = WhatHappenedThatHit(player2HitChanceMainHand, player1DodgeChance, player1ParryChance, player1BlockChance, player1HasShield, player1HasWeapon, random);

                        if (action == "h")
                        {
                            damage = random.Next(minDamage, maxDamage + 1);
                            roundLog += generate.CombatTextMaleAttacker(player2.Name, player1.Name, damage, bodyPart.Name, random.Next(0, combatflavorindex));
                            player1.Health -= damage;
                        }
                        else if (action == "b")
                        {
                            roundLog += generate.CombatTextBlock(player2.Name, player1.Name, player2.MainHand.Name.ToLower(), player1.OffHand.shld.Name, random.Next(0, 1));
                        }
                        else if (action == "p")
                        {
                            roundLog += generate.CombatTextParry(player2.Name, player1.Name, random.Next(0, 1));
                        }
                        else if (action == "d")
                        {
                            roundLog += generate.CombatTextDodge(player2.Name, player2.MainHand.Name, player1.Name, random.Next(0, 1));
                        }

                        player2AttackWithOffHand = false;
                    }

                    // Check if player1 forfeits
                    if (player1.Health <= player1.Vitality * (player1Forfeit / 100))
                    {
                        keepFighting = false;
                        player2Won = true;
                    }
                }
                else
                {
                    if (keepFighting && player2AttackWithMainHand)
                    {
                        bonusDamage = player2.Strength / 10;
                        minDamage = player2.MainHand.MinimumDamage + bonusDamage;
                        maxDamage = player2.MainHand.MaximumDamage + bonusDamage;

                        var bodyPartId = DiceForBodyPart(random.Next(1, 97), player2.MainHand.Name);
                        var bodyPart = player1.Equipment.FirstOrDefault(ec => ec.ArmorTypeId == bodyPartId);

                        if (minDamage - bodyPart.ArmorValue < 0)
                            minDamage = 0;
                        else
                            minDamage -= bodyPart.ArmorValue;

                        if (maxDamage - bodyPart.ArmorValue < 0)
                            maxDamage = 0;
                        else
                            maxDamage -= bodyPart.ArmorValue;

                        string action = WhatHappenedThatHit(player2HitChanceMainHand, player1DodgeChance, player1ParryChance, player1BlockChance, player1HasShield, player1HasWeapon, random);

                        if (action == "h")
                        {
                            damage = random.Next(minDamage, maxDamage + 1);
                            roundLog += generate.CombatTextMaleAttacker(player2.Name, player1.Name, damage, bodyPart.Name, random.Next(0, combatflavorindex));
                            player1.Health -= damage;
                        }
                        else if (action == "b")
                        {
                            roundLog += generate.CombatTextBlock(player2.Name, player1.Name, player2.MainHand.Name.ToLower(), player1.OffHand.shld.Name, random.Next(0, 1));
                        }
                        else if (action == "p")
                        {
                            roundLog += generate.CombatTextParry(player2.Name, player1.Name, random.Next(0, 1));
                        }
                        else if (action == "d")
                        {
                            roundLog += generate.CombatTextDodge(player2.Name, player2.MainHand.Name, player1.Name, random.Next(0, 1));
                        }

                        player2AttackWithMainHand = false;
                    }
                    else if (keepFighting && player2AttackWithOffHand)
                    {
                        bonusDamage = player2.Strength / 10;
                        minDamage = player2.OffHand.wep.MinimumDamage + bonusDamage;
                        maxDamage = player2.OffHand.wep.MaximumDamage + bonusDamage;

                        var bodyPartId = DiceForBodyPart(random.Next(1, 97), player2.OffHand.wep.Name);
                        var bodyPart = player1.Equipment.FirstOrDefault(ec => ec.ArmorTypeId == bodyPartId);

                        if (minDamage - bodyPart.ArmorValue < 0)
                            minDamage = 0;
                        else
                            minDamage -= bodyPart.ArmorValue;

                        if (maxDamage - bodyPart.ArmorValue < 0)
                            maxDamage = 0;
                        else
                            maxDamage -= bodyPart.ArmorValue;

                        string action = WhatHappenedThatHit(player2HitChanceMainHand, player1DodgeChance, player1ParryChance, player1BlockChance, player1HasShield, player1HasWeapon, random);

                        if (action == "h")
                        {
                            damage = random.Next(minDamage, maxDamage + 1);
                            roundLog += generate.CombatTextMaleAttacker(player2.Name, player1.Name, damage, bodyPart.Name, random.Next(0, combatflavorindex));
                            player1.Health -= damage;
                        }
                        else if (action == "b")
                        {
                            roundLog += generate.CombatTextBlock(player2.Name, player1.Name, player2.MainHand.Name.ToLower(), player1.OffHand.shld.Name, random.Next(0, 1));
                        }
                        else if (action == "p")
                        {
                            roundLog += generate.CombatTextParry(player2.Name, player1.Name, random.Next(0, 1));
                        }
                        else if (action == "d")
                        {
                            roundLog += generate.CombatTextDodge(player2.Name, player2.MainHand.Name, player1.Name, random.Next(0, 1));
                        }

                        player2AttackWithOffHand = false;
                    }

                    // Check if player 1 forfeits
                    if (player1.Health <= player1.Vitality * (player1Forfeit / 100))
                    {
                        keepFighting = false;
                        player2Won = true;
                    }

                    if (keepFighting && player1AttackWithMainHand)
                    {
                        bonusDamage = player1.Strength / 10;
                        minDamage = player1.MainHand.MinimumDamage + bonusDamage;
                        maxDamage = player1.MainHand.MaximumDamage + bonusDamage;

                        var bodyPartId = DiceForBodyPart(random.Next(1, 97), player1.MainHand.Name);
                        var bodyPart = player2.Equipment.FirstOrDefault(al => al.ArmorTypeId == bodyPartId);

                        if (minDamage - bodyPart.ArmorValue < 0)
                            minDamage = 0;
                        else
                            minDamage -= bodyPart.ArmorValue;

                        if (maxDamage - bodyPart.ArmorValue < 0)
                            maxDamage = 0;
                        else
                            maxDamage -= bodyPart.ArmorValue;

                        string action = WhatHappenedThatHit(player1HitChanceMainHand, player2DodgeChance, player2ParryChance, player2BlockChance, player2HasShield, player2HasWeapon, random);

                        if (action == "h")
                        {
                            damage = random.Next(minDamage, maxDamage + 1);
                            roundLog += generate.CombatTextMaleAttacker(player1.Name, player2.Name, damage, bodyPart.Name, random.Next(0, combatflavorindex));
                            player2.Health -= damage;
                        }
                        else if (action == "b")
                        {
                            roundLog += generate.CombatTextBlock(player1.Name, player2.Name, player1.MainHand.Name.ToLower(), player2.OffHand.shld.Name, random.Next(0, 1));
                        }
                        else if (action == "p")
                        {
                            roundLog += generate.CombatTextParry(player1.Name, player2.Name, random.Next(0, 1));
                        }
                        else if (action == "d")
                        {
                            roundLog += generate.CombatTextDodge(player1.Name, player1.MainHand.Name, player2.Name, random.Next(0, 1));
                        }

                        player1AttackWithMainHand = false;
                    }
                    else if (keepFighting && player1AttackWithOffHand)
                    {
                        bonusDamage = player1.Strength / 10;
                        minDamage = player2.OffHand.wep.MinimumDamage + bonusDamage;
                        maxDamage = player2.OffHand.wep.MaximumDamage + bonusDamage;

                        var bodyPartId = DiceForBodyPart(random.Next(1, 97), player2.OffHand.wep.Name);
                        var bodyPart = player2.Equipment.FirstOrDefault(al => al.ArmorTypeId == bodyPartId);

                        if (minDamage - bodyPart.ArmorValue < 0)
                            minDamage = 0;
                        else
                            minDamage -= bodyPart.ArmorValue;

                        if (maxDamage - bodyPart.ArmorValue < 0)
                            maxDamage = 0;
                        else
                            maxDamage -= bodyPart.ArmorValue;

                        string action = WhatHappenedThatHit(player1HitChanceMainHand, player2DodgeChance, player2ParryChance, player2BlockChance, player2HasShield, player2HasWeapon, random);

                        if (action == "h")
                        {
                            damage = random.Next(minDamage, maxDamage + 1);
                            roundLog += generate.CombatTextMaleAttacker(player1.Name, player2.Name, damage, bodyPart.Name, random.Next(0, combatflavorindex));
                            player2.Health -= damage;
                        }
                        else if (action == "b")
                        {
                            roundLog += generate.CombatTextBlock(player1.Name, player2.Name, player1.MainHand.Name.ToLower(), player2.OffHand.shld.Name, random.Next(0, 1));
                        }
                        else if (action == "p")
                        {
                            roundLog += generate.CombatTextParry(player1.Name, player2.Name, random.Next(0, 1));
                        }
                        else if (action == "d")
                        {
                            roundLog += generate.CombatTextDodge(player1.Name, player1.MainHand.Name, player2.Name, random.Next(0, 1));
                        }

                        player1AttackWithOffHand = false;
                    }

                    // check if player2 forfeits
                    if (player2.Health <= player2.Vitality * (player2Forfeit / 100))
                    {
                        keepFighting = false;
                        player1Won = true;
                    }
                }

                if (!keepFighting)
                {
                    if (player1Won)
                    {
                        roundLog += player2.Name + " choose to yield, " + player1.Name + " wins the duel.";
                        spoiler = player1.Name + " defeated " + player2.Name + " in a random duel.";
                    }
                    if (player2Won)
                    {
                        roundLog += player1.Name + " choose to yield, " + player2.Name + " wins the duel.";
                        spoiler = player2.Name + " defeated " + player1.Name + " in a random duel.";
                    }
                }

                var round = new Round()
                {
                    RoundNr = roundNr,
                    Log = roundLog,
                };

                fullFight.Add(round);

                roundLog = string.Empty;
            }

            if (player1.Health < 0)
            {
                int survivalDice = random.Next(1, 6);
                bool characterDied = FateOfCharacter(survivalDice, player1);

                if (characterDied)
                {
                    player1.Alive = false;

                    roundNr++;
                    var round = new Round()
                    {
                        RoundNr = roundNr,
                        Log = player1.Name + " dies from the wounds ",
                    };
                }
            }
            else if (player2.Health < 0)
            {
                int survivalDice = random.Next(1, 6);
                bool characterDied = FateOfCharacter(survivalDice, player2);

                if (characterDied)
                {
                    player2.Alive = false;

                    roundNr++;
                    var round = new Round()
                    {
                        RoundNr = roundNr,
                        Log = player2.Name + " dies from the wounds ",
                    };
                }
            }

            SaveCombatLogToDatabase(ctx, fullFight, player1, spoiler);

            if (player2Id != null)
            {
                SaveCombatLogToDatabase(ctx, fullFight, player2, spoiler);
            }

            ctx.Arenas.Remove(ctx.Arenas.FirstOrDefault(a => a.CharacterId == player1.Id));

            if (ctx.Arenas.Any(a => a.CharacterId == player2Id))
            {
                ctx.Arenas.Remove(ctx.Arenas.FirstOrDefault(a => a.CharacterId == player2.Id));

            }

            if (player1Won)
            {
                UpdateCharacterStats(player1);
            }
            else
            {
                if (player2Id != null)
                {
                    UpdateCharacterStats(player2);
                }
            }

            ctx.SaveChanges();

            return fullFight;
        }

        private static string WhatHappenedThatHit(double playerHitChance, double enemyDodgeChance, double enemyParryChance, double enemyBlockChance, bool enemyHasShield, bool enemyHasWeapon, Random rand)
        {
            string weight = "";

            if (enemyHasShield)
            {
                for (int i = 0; i < enemyBlockChance; i++)
                {
                    weight += "b";
                }
            }
            if (enemyHasWeapon)
            {
                for (int i = 0; i < enemyParryChance; i++)
                {
                    weight += "p";
                }
            }

            for (int i = 0; i < enemyDodgeChance; i++)
            {
                weight += "d";
            }

            for (int i = 0; i < playerHitChance; i++)
            {
                weight += "h";
            }

            int n = rand.Next(weight.Length);

            string letter = "";

            for (int i = 0; i < weight.Length; i++)
            {
                if (i == weight[n])
                {
                    letter += weight[n];
                }
            }

            return letter;
        }

        private static double PlayerHitChance(CharacterCombatModel player)
        {
            string weaponType = player.MainHand.WeaponType.Name.ToLower();

            double weaponWP = 0;
            double playerWP = 0;

            weaponWP = player.MainHand.ReqWeaponMastery;

            if (weaponType == "axe")
            {
                playerWP = player.Axe;
            }
            else if (weaponType == "dagger")
            {
                playerWP = player.Dagger;
            }
            else if (weaponType == "mace")
            {
                playerWP = player.Mace;
            }
            else if (weaponType == "polearm")
            {
                playerWP = player.Polearm;
            }
            else if (weaponType == "spear")
            {
                playerWP = player.Spear;
            }
            else if (weaponType == "sword")
            {
                playerWP = player.Sword;
            }
            else if (weaponType == "fist")
            {
                return 100;
            }

            double hitchance = playerWP / weaponWP * 100;

            if (hitchance > 100)
            {
                double surplus = hitchance - 100;
                
                hitchance = Math.Pow(surplus, 0.75) + 100;
            }

            return hitchance;
        }

        private static double diminishing_returns(double val, double scale)
        {
            if (val < 0)
                return -diminishing_returns(-val, scale);
            double mult = val / scale;
            double trinum = (Math.Sqrt(8.0 * mult + 1.0) - 1.0) / 2.0;
            return trinum * scale;
        }

        private static void AttacksPlayerCanDoThisRound(CharacterCombatModel player, ref bool AttackWithMainHand, ref bool AttackWithOffHand)
        {
            if (player.MainHand.TwoHanded) // 2H WEP
            {
                AttackWithMainHand = true;
            }
            else if (player.MainHand != null && player.OffHand.wep != null) // WEP + WEP
            {
                AttackWithMainHand = true;
                AttackWithOffHand = true;
            }
            else if (player.MainHand != null && player.OffHand.shld != null && player.OffHand.wep == null) // 1H WEP + SHIELD
            {
                AttackWithMainHand = true;
            }
            else if (player.MainHand != null && player.OffHand.shld == null && player.OffHand.wep == null) // 1H WEP + NULL + NULL
            { 
                AttackWithMainHand = true;
            }
            else if (player.MainHand == null && player.OffHand.wep != null) // NULL + WEP
            {
                AttackWithOffHand = true;
            }
            else if (player.MainHand == null && player.OffHand.shld != null) // NULL + SHIELD 
            {
                AttackWithMainHand = true;
            }
            else if (player.MainHand == null && player.OffHand.wep == null && player.OffHand.shld == null) // NULL + NULL + NULL
            {
                AttackWithMainHand = true;
                AttackWithOffHand = true;
            }
        }

        private static bool FateOfCharacter(int survivalDice, CharacterCombatModel character)
        {
            bool characterDied = false;

            if (character.Health == -1)
            {
                int[] array = new int[] { 1 };
                if (array.Contains(survivalDice))
                    characterDied = true;
            }
            else if (character.Health == -2)
            {
                int[] array = new int[] { 2, 3 };
                if (array.Contains(survivalDice))
                    characterDied = true;
            }
            else if (character.Health == -3)
            {
                int[] array = new int[] { 4, 5, 1 };
                if (array.Contains(survivalDice))
                    characterDied = true;
            }
            else if (character.Health == -4)
            {
                int[] array = new int[] { 2, 3, 4, 5 };
                if (array.Contains(survivalDice))
                    characterDied = true;
            }
            else if (character.Health <= -5)
            {
                characterDied = true;
            }

            return characterDied;
        }

        private static void UpdateCharacterStats(CharacterCombatModel character)
        {
            int experienceGained = 14 + character.Level * 1;
            int goldEarned = 1 + character.Level / 5;

            if (character.Health >= 0)
            {
                character.Experience += experienceGained;
                character.Gold += goldEarned;
            }
        }

        private static void SaveCombatLogToDatabase(ProjectStrawberryEntities ctx, List<Round> fullFight, CharacterCombatModel character, string spoiler)
        {
            byte[] log;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, fullFight);

                log = ms.ToArray();

                ctx.CombatLogs.Add(new CombatLog() { Player = character.Id, Log = log, GameDate = DateTime.Now, Spoiler = spoiler });
                ctx.SaveChanges();
            }
        }

        private static int DiceForBodyPart(int dice, string weaponType)
        {
            // Head 1, shoulder 2, torso 3, gloves 4, legs 5, feet 6

            if (weaponType.ToLower() == "axe")
            {
                if (dice >= 1 && dice <= 14)
                    return 1;
                else if (dice >= 15 && dice <= 37)
                    return 2;
                else if (dice >= 38 && dice <= 55)
                    return 3;
                else if (dice >= 56 && dice <= 77)
                    return 4;
                else if (dice >= 78 && dice <= 88)
                    return 5;
                else if (dice >= 89 && dice <= 96)
                    return 6;
            }
            else if (weaponType.ToLower() == "dagger")
            {
                if (dice >= 1 && dice <= 10)
                    return 1;
                else if (dice >= 11 && dice <= 36)
                    return 2;
                else if (dice >= 37 && dice <= 71)
                    return 3;
                else if (dice >= 72 && dice <= 86)
                    return 4;
                else if (dice >= 87 && dice <= 94)
                    return 5;
                else if (dice >= 95 && dice <= 96)
                    return 6;
            }
            else if (weaponType.ToLower() == "mace")
            {
                if (dice >= 1 && dice <= 14)
                    return 1;
                else if (dice >= 15 && dice <= 37)
                    return 2;
                else if (dice >= 38 && dice <= 55)
                    return 3;
                else if (dice >= 56 && dice <= 77)
                    return 4;
                else if (dice >= 78 && dice <= 88)
                    return 5;
                else if (dice >= 89 && dice <= 96)
                    return 6;
            }
            else if (weaponType.ToLower() == "polearm")
            {
                if (dice >= 1 && dice <= 14)
                    return 1;
                else if (dice >= 15 && dice <= 37)
                    return 2;
                else if (dice >= 38 && dice <= 55)
                    return 3;
                else if (dice >= 56 && dice <= 77)
                    return 4;
                else if (dice >= 78 && dice <= 88)
                    return 5;
                else if (dice >= 89 && dice <= 96)
                    return 6;
            }
            else if (weaponType.ToLower() == "spear")
            {
                if (dice >= 1 && dice <= 14)
                    return 1;
                else if (dice >= 15 && dice <= 37)
                    return 2;
                else if (dice >= 38 && dice <= 55)
                    return 3;
                else if (dice >= 56 && dice <= 77)
                    return 4;
                else if (dice >= 78 && dice <= 88)
                    return 5;
                else if (dice >= 89 && dice <= 96)
                    return 6;
            }
            else if (weaponType.ToLower() == "sword")
            {
                if (dice >= 1 && dice <= 14)
                    return 1;
                else if (dice >= 15 && dice <= 37)
                    return 2;
                else if (dice >= 38 && dice <= 55)
                    return 3;
                else if (dice >= 56 && dice <= 77)
                    return 4;
                else if (dice >= 78 && dice <= 88)
                    return 5;
                else if (dice >= 89 && dice <= 96)
                    return 6;
            }
            else
            {
                if (dice >= 1 && dice <= 33)
                    return 1;
                else if (dice >= 34 && dice <= 48)
                    return 2;
                else if (dice >= 49 && dice <= 81)
                    return 3;
                else if (dice >= 82 && dice <= 96)
                    return 4;
            }

            Random random = new Random();

            return random.Next(1, 7);
        }

        private static ArmorModel GenerateSkinArmor(int bodyPartId)
        {
            if (bodyPartId == 1)
            {
                return new ArmorModel() { Name = "skin", ArmorValue = 0, ModelArmorType = new ArmorTypeModel() { Id = bodyPartId, Name = "head" } };
            }
            else if (bodyPartId == 2)
            {
                return new ArmorModel() { Name = "skin", ArmorValue = 0, ModelArmorType = new ArmorTypeModel() { Id = bodyPartId, Name = "shoulder" } };
            }
            else if (bodyPartId == 3)
            {
                return new ArmorModel() { Name = "skin", ArmorValue = 0, ModelArmorType = new ArmorTypeModel() { Id = bodyPartId, Name = "chest" } };
            }
            else if (bodyPartId == 4)
            {
                return new ArmorModel() { Name = "skin", ArmorValue = 0, ModelArmorType = new ArmorTypeModel() { Id = bodyPartId, Name = "hand" } };
            }
            else if (bodyPartId == 5)
            {
                return new ArmorModel() { Name = "skin", ArmorValue = 0, ModelArmorType = new ArmorTypeModel() { Id = bodyPartId, Name = "legs" } };
            }
            else if (bodyPartId == 6)
            {
                return new ArmorModel() { Name = "skin", ArmorValue = 0, ModelArmorType = new ArmorTypeModel() { Id = bodyPartId, Name = "feet" } };
            }

            return new ArmorModel() { Name = "skin", ArmorValue = 0, ModelArmorType = new ArmorTypeModel() { Id = bodyPartId, Name = "bodypart" } };
        }
    }
}