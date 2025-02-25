﻿namespace RatingService.API.Models.Events;

public sealed record GetEventResponse(int Id,
                                      string Title,
                                      int OrganizerId,
                                      DateTime CreationDate,
                                      string Category,
                                      string State,
                                      float? Rating);
