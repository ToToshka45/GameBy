namespace Domain.Entities
{
    public class Rank : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Gamer> Gamers { get; set; } = new();
    }
}
