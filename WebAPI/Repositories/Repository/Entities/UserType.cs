namespace Repositories.Repository.Entities
{
    public class UserType
    {
        public UserType()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}