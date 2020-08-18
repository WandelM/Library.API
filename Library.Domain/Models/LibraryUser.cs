using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Models
{
    public class LibraryUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime BirthDate { get; set; }
        public UserCard UserCard { get; set; }
    }
}
