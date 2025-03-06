namespace Domain.ValueObjects
{
    public class UserEmail
    {
        public string Value { get; private set; }

        public UserEmail(string value) { 
            Value = value;
            //throw new ValidationException("test");
        }
    }
}
