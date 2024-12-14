namespace Domain.Entities
{
    public class Rank : BaseEntity
    {
        public string Name { get; set; }
        public List<Gamer> Gamers { get; set; } = new();
    }
}
