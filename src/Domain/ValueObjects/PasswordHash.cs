namespace iot.Domain.ValueObjects
{
    public class PasswordHash
    {
        PasswordHash() { }
        PasswordHash(string password)
        {
            Value = password;
        }

        public string Value { get; private set; }

        public static PasswordHash Parse(string password)
        {
            return new PasswordHash(password);
        }
    }
}