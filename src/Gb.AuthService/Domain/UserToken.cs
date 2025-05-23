﻿namespace Domain
{
    public class UserToken
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<string> UserRoles { get; set; }
    }
}
