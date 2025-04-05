namespace Application.Dto;

public class GetEventsByUserIdDto
{
    public int UserId { get; set; }
    public IEnumerable<GetShortEventDto> GamerEvents { get; set; }
    public IEnumerable<GetShortEventDto> OrganizerEvents { get; set; }
}