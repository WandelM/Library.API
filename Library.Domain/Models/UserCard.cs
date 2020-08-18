using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Models
{
    public class UserCard
    {
        public Guid Id { get; set; }
        public LibraryUser LibraryUser { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string CardNumber { get; set; }
    }
}
