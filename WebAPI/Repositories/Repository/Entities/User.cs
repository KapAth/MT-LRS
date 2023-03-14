using Repositories.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Repository.Entities

{
    public class User
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
        /// Users name.
        /// </value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// Users surname.
        /// </value>
        public string? Surname { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// Users birth date.
        /// </value>
        public DateTime? BirthDate { get; set; }

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

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// Users email address.
        /// </value>
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>
        /// User is Active, boolean.
        /// </value>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the user title.
        /// </summary>
        /// <value>
        /// The user title.
        /// </value>
        public UserTitle? UserTitle { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        public UserType? UserType { get; set; }
    }
}