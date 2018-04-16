using ProjectStrawberry.DAL;
using System;

namespace ProjectStrawberry.Models
{
    public class ArenaModel
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public int Forfeit { get; set; }
        public DateTime Queued { get; set; }
        public CharacterModel Character { get; set; }
    }
}