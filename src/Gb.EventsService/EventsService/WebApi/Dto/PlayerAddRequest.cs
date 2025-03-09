namespace WebApi.Dto
{
    public class PlayerAddRequest
    {
        public int UserId { get; set; }

        public int? EventId { get; set; }

        public string UserName {get;set;}
    }
}
