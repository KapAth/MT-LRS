using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// TODO cleanup imports
// TODO why is the DTO in repositories project?
namespace Repositories.Repository
{
    public class UserDto
    {
        /// <summary>
        /// TODO xml documentation
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsActive { get; set; }
        public string Title { get; set; } // TODO should return only the id of the title
        public string Type { get; set; } // TODO should return only the id of the type

    }
}