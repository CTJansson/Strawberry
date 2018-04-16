namespace ProjectStrawberry.Models
{
    public class ShieldModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BlockValue { get; set; }
        public int ReqShieldMastery { get; set; }
        public int Weight { get; set; }
        public bool TournamentReward { get; set; }
        public int Price { get; set; }

    }
}