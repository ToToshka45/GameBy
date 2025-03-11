namespace Constants
{
    public enum EventStatus
    {
        Announced, 
        RegistrationInProcess,
        RegistrationClosed,
        InProgress,
        Finished,
        Canceled
    }

    public enum EventType
    {
        PlayerAdded,
        PlayerRemoved,
        EventFinished,
        EventCancelled,
        PlayerParticipate,
        PlayerNotParticipated,
        AdminAdded,
        AdminRemoved,
        LocationChanged,
        EventCreated
    }

    public enum EventUserRole
    {
        Player,
        Organizer,
        Admin
    }
}
