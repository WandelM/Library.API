using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Dtos
{
    public class CategoryOutputModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Name of category
        /// </summary>
        public string Name { get; set; }
    }
}
