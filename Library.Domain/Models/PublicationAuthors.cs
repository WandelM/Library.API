using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Models
{
    /// <summary>
    /// Many to many linking model publication-author
    /// </summary>
    class PublicationAuthors
    {
        /// <summary>
        /// Id of publication
        /// </summary>
        public Guid PublicationId { get; set; }
        /// <summary>
        /// Id of an author
        /// </summary>
        public Guid AuthorId { get; set; }
    }
}
