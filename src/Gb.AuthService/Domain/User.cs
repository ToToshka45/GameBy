using Domain.ValueObjects;

namespace Domain
{
    public class User : BaseEntity
    {
        public UserName Login { get; set; }

        public UserPassword Password { get; set; }

        public UserEmail Email { get; set; }

        public bool IsEmailVerified { get; set; }

        public UserPhone? Phone { get; set; }

        public bool IsPhoneVerified { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public bool IsEnabled { get; set; }
    }
}
