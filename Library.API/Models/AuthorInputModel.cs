using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class AuthorInputModel
    {
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
        public DateTime DateOfDeath { get; set; }
    }
}
