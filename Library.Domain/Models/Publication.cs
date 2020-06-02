using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Models
{
    /// <summary>
    /// Single publication
    /// </summary>
    public class Publication
    {
        /// <summary>
        /// Id of publication
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ISBN of an publication
        /// </summary>
        public string ISBN { get; set; }
        ///<summary>
        /// Title of publication
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Date of publication
        /// </summary>
        public DateTime PublicationDate { get; set; }
        /// <summary>
        /// Type of publication
        /// </summary>
        public PublicationType PublicationType { get; set; }
        /// <summary>
        /// Count of pages
        /// </summary>
        public byte PageCount { get; set; }
        /// <summary>
        /// Categories of publication
        /// </summary>
        public ICollection<Category> Categories = new List<Category>();
    }
}
