// TODO cleanup imports
// TODO why is the DTO in repositories project?
namespace WebAPI.Models
{
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        public string? Surname { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>
        /// The is active.
        /// </value>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the user type identifier.
        /// </summary>
        /// <value>
        /// The user type identifier.
        /// </value>
        public int UserTypeId { get; set; }

        /// <summary>
        /// Gets or sets the user title identifier.
        /// </summary>
        /// <value>
        /// The user title identifier.
        /// </value>
        public int UserTitleId { get; set; }

        // public string? Title { get; set; } // TODO should return only the id of the title
        // public string? Type { get; set; } // TODO should return only the id of the type
    }
}