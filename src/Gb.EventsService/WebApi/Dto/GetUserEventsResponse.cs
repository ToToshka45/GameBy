namespace WebApi.Dto;

public class GetUserEventsResponse
{
    public int UserId { get; set; }
    public IEnumerable<GetShortEventResponse> GamerEvents { get; set; }
    public IEnumerable<GetShortEventResponse> OrganizerEvents { get; set; }
}