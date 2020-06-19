using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Dtos
{
    public class CategoryInputModel
    {
        /// <summary>
        /// Name of category
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
