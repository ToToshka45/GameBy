namespace RatingService.Domain.Exceptions;

public class ParticipantExistsException : Exception
{
    public ParticipantExistsException(int participantId) : base($"The Participant with Id {participantId} already exists.") { }
}
