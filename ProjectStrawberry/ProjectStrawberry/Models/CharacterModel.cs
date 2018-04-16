using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectStrawberry.Models
{
    public class CharacterModel
    {
        public string AccountId { get; set; }
        [Required]
        [MaxLength(14), MinLength(2)]
        public string Name { get; set; }
        [Required]
        public int GenderId { get; set; }
        public GenderModel Gender { get; set; }
        [Required]
        public int ClassId { get; set; }
        public ClassModel Class { get; set; }
        [Required]
        public string Avatar { get; set; }
        public decimal Gold { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }

        [Required]
        [Range(0, 60)]
        public int Stamina { get; set; }
        [Required]
        [Range(0, 60)]
        public int Strength { get; set; }
        [Required]
        [Range(0, 60)]
        public int Quickness { get; set; }

        [Required]
        [Range(0, 60)]
        public int Block { get; set; }
        [Required]
        [Range(0, 60)]
        public int Evasion { get; set; }
        [Required]
        [Range(0, 60)]
        public int Parry { get; set; }
        [Required]
        [Range(0, 60)]
        public int Vitality { get; set; }
        public int Health { get; set; }

        [Required]
        [Range(0, 60)]
        public int Sword { get; set; }
        [Required]
        [Range(0, 60)]
        public int Mace { get; set; }
        [Required]
        [Range(0, 60)]
        public int Dagger { get; set; }
        [Required]
        [Range(0, 60)]
        public int Spear { get; set; }
        [Required]
        [Range(0, 60)]
        public int Axe { get; set; }
        [Required]
        [Range(0, 60)]
        public int Polearm { get; set; }

        public bool Alive { get; set; }
    }
}