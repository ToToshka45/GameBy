namespace RatingService.Domain.ValueObjects.Identifiers
{
    public class ExternalParticipantId : ExternalEventId
    {
        public ExternalParticipantId(int id) : base(id) { }

        //public static implicit operator ExternalParticipantId(int id) => new ExternalParticipantId(id);
    }
}