using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Models
{
    /// <summary>
    /// Many to many linking model publication-category
    /// </summary>
    class PublicationCategories
    {
        /// <summary>
        /// Id of publication
        /// </summary>
        public Guid PublicationId { get; set; }
        /// <summary>
        /// Id of category
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}
