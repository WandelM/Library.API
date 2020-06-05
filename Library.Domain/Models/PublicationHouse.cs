using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Models
{
    /// <summary>
    /// Publication house data model
    /// </summary>
    public class PublicationHouse
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
