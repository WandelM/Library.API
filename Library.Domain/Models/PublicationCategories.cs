using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Models
{
    /// <summary>
    /// Many to many linking model publication-category
    /// </summary>
    public class PublicationCategories
    {
        /// <summary>
        /// Id of publication
        /// </summary>
        public Guid PublicationId { get; set; }
        /// <summary>
        /// Publication for category
        /// </summary>
        public Publication Publication { get; set; }
        /// <summary>
        /// Id of category
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// Category for publication
        /// </summary>
        public Category Category { get; set; }
    }
}
