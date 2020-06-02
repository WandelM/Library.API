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
    }
}
