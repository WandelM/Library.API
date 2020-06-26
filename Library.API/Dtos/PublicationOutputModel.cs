using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Dtos
{
    public class PublicationOutputModel
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
        public IEnumerable<AuthorOutputModel> Authors { get; set; }
        /// <summary>
        /// Authors of an publication
        /// </summary>
        public IEnumerable<CategoryOutputModel> Categories { get; set; }
    }
}
