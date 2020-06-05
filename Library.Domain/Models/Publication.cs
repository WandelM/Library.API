using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Models
{
    /// <summary>
    /// Model of single publication
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
        /// PublicationHouse of publication
        /// </summary>
        public PublicationHouse PublicationHouse { get; set; }
        /// <summary>
        /// Id of publication house
        /// </summary>
        public Guid PublicationHouseId { get; set; }
        /// <summary>
        /// Count of pages
        /// </summary>
        public byte PageCount { get; set; }
        /// <summary>
        /// Categories of publication
        /// </summary>
        public ICollection<PublicationCategories> PublicationCategories { get; set; }
        /// <summary>
        /// Authors of an publication
        /// </summary>
        public ICollection<PublicationAuthors> PublicationAuthors { get; set; }
    }
}
