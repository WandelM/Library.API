using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Models
{
    /// <summary>
    /// Entity that describes single author
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Id of an author
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Authors name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Authors surname
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Authors date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Authors date of death
        /// </summary>
        public DateTime? DateOfDeath { get; set; }
        /// <summary>
        /// Publications of an author
        /// </summary>
        public ICollection<PublicationAuthors> PublicationAuthors { get; set; }
    }
}
