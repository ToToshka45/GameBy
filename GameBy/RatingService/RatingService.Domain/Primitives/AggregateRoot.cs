﻿namespace RatingService.Domain.Primitives;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
{
    protected AggregateRoot()
    {
    }
}