namespace Login.Models
{
    public class User:IEquatable<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool Equals(User? other)
        {
            return this.Email == other.Email && this.Password == other.Password;
        }
    }
}
