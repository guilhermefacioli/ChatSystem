namespace ChatSystem.Model.Users
{
    public class User
    {
      public User(
            Guid id,
            string name,
            string email,
            string password
            )
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}
