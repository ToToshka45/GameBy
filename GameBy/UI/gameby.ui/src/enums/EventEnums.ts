export enum EventCategory {
    Quiz,
    Strategy,
    Mafia,
    Sports
}

export enum EventProgressionState {
    Announced,
    RegistrationInProcess,
    RegistrationClosed,
    Finished,
}

export enum ParticipationState {
    PendingAcceptance,
    Declined,
    Registered,
    Cancelled,
    Participated,
    Unclarified 
}