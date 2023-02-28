using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsActive { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

    }
}
