﻿namespace Constants
{
    public enum EventStatus
    {
        Upcoming,
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
        PlayerAccepted,
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
