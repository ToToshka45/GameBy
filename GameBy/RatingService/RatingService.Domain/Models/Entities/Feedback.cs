using RatingService.Domain.Enums;
using RatingService.Domain.Models.ValueObjects;
using System.ComponentModel;

namespace RatingService.Domain.Models.Entities;

public class ParticipantFeedback
{
    public int Id { get; private set; }
    public AuthorId AuthorId { get; }
    public ReceiverId ReceiverId { get; }
    public UserRole UserRole { get; }
    public Description Description { get; }
    private ParticipantFeedback(AuthorId userId, ReceiverId receiverId, UserRole role)
    {
        AuthorId = userId;
        ReceiverId = receiverId;
        UserRole = role;
    }

    public static ParticipantFeedback Create();
}

