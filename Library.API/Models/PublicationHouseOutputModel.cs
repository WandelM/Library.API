using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class PublicationHouseOutputModel
    {
        /// <summary>
        /// Id of publication house
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of publication house
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Publications of publication house
        /// </summary>
        public ICollection<Publication> Publications { get; set; }
    }
}
