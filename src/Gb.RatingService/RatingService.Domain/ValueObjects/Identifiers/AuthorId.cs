﻿namespace RatingService.Domain.ValueObjects.Identifiers;

public class AuthorId : BaseEntityId
{
    public AuthorId(int id) : base(id) { }

    public static implicit operator AuthorId(int id) => new AuthorId(id);
}