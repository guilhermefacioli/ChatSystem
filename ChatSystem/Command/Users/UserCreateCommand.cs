namespace ChatSystem.Command.Users
{
    public class UserCreateCommand
    {
        public UserCreateCommand(
            string name,
            string email,
            string password
            )
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}