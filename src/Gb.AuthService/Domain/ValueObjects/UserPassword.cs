namespace Domain.ValueObjects
{
    public class UserPassword
    {
        public string Value { get; private set; }

        public UserPassword(string value)
        {
            Value = value;
        }
    }
}
