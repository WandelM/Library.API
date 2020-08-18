using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Dtos
{
    public class UserUpdateModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(80)]
        public string Surname { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
