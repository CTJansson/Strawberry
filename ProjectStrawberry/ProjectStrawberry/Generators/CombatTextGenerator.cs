using ProjectStrawberry.DAL;
using ProjectStrawberry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Generators
{
    public class CombatTextGenerator
    {
        private string attacker;
        private string defender;
        private string damage;
        private string bodypart;

        public string CombatTextMaleAttacker(string atk, string def, int dmg, string bp, int index)
        {
            attacker = atk;
            defender = def;
            damage = dmg.ToString();
            bodypart = bp;

            List<string> listOfSentences = new List<string>()
            {
                attacker + " rushes towards " + defender + " and aims for the " + bodypart + ", " + attacker + " hits " + defender + " for " + damage + " damage. ",
                "The audience starts screaming loudly when " + attacker + " starts to sprint towards " + defender + ", " + defender + " freezes for a moment and fails to avoid the attack (" + damage + "). ",
                "",
            };

            return listOfSentences.ElementAt(index);
        }

        public string CombatTextDodge(string atk, string weaponName, string def, int index)
        {
            List<string> flavors = new List<string>()
            {
                atk + " swings " + weaponName + " vicously, " + def + " manage to dodge " + atk + "'s attacks with ease. ",
            };

            return flavors.ElementAt(index);
        }

        public string CombatTextParry(string atk, string def, int index)
        {
            List<string> flavors = new List<string>()
            {
                def + " magically managed to parry " + atk + " attack. ",
            };

            return flavors.ElementAt(index);
        }

        public string CombatTextBlock(string atk, string def, string weaponNane, string shieldName, int index)
        {
            List<string> flavors = new List<string>()
            {
                "A crushing blow can be heard throughout the arena, the slam from " + atk + "'s " + weaponNane + " when it hits " + def + "'s " + shieldName + ". " ,
            };

            return flavors.ElementAt(index);
        }
    }
}