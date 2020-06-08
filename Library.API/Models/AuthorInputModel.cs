using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class AuthorInputModel
    {
        /// <summary>
        /// Authors name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Authors surname
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }
        /// <summary>
        /// Authors date of birth
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Authors date of death
        /// </summary>
        public DateTime? DateOfDeath { get; set; }
    }
}
