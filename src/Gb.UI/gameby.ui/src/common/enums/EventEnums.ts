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
    PendingAcceptance = "Pending acceptance",
    Declined = "Declined",
    Accepted = "Accepted",
    Cancelled = "Cancelled",
    Participated = "Participated",
    Unclarified = "Unclarified" 
}