namespace RatingService.Domain.Enums;

public enum EntityType
{
    Organizer,
    Gamer,
    Event
}

public enum Category
{
    RPG,
    Strategy,
    Mafia,
    Quiz,
    Poker,
    Hide_and_seek,
    Unknown
}

public enum EventProgressionState
{
    Announced,
    RegistrationOpen,
    RegistrationClosed,
    Completed,
    Cancelled,
    Postponed,
    Unclarified
}

/// <summary>
/// Participation states of a Gamer in the Event.
/// </summary>
public enum ParticipationState
{
    /// <summary>
    /// A Gamer sent the request to take part in an event and now is awaiting for the response.
    /// </summary>
    PendingAcceptance,
    /// <summary>
    /// "An Organizer declined the request of a Gamer to take part in an event.
    /// </summary>
    Declined,
    /// <summary>
    /// A Gamer was accepted to take part in an event.
    /// </summary>
    Registered,
    /// <summary>
    /// A Gamer has cancelled his participation after he was registered.
    /// </summary>
    Cancelled,
    /// <summary>
    /// A Gamer has participated in an event.
    /// </summary>
    Participated,
    /// <summary>
    /// Clarification is required.
    /// </summary>
    Unclarified
}
