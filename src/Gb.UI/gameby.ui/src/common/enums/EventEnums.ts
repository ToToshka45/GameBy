export enum EventCategory {
    RPG,
    Strategy,
    Mafia,
    Quiz,
    Poker,
    HideAndSeek,
    Sports,
    Undefined
}

export enum EventStatus {
    Announced,
    RegistrationInProcess,
    RegistrationClosed,
    InProgress,
    Finished,
    Cancelled
}

export enum ParticipationState {
    PendingAcceptance,
    Declined,
    Accepted,
    Cancelled,
    Participated,
    Unclarified 
}