using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Dtos
{
    public class PublicationInputModel
    {
        /// <summary>
        /// ISBN of an publication
        /// </summary>
        [Required]
        public string ISBN { get; set; }
        ///<summary>
        /// Title of publication
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Date of publication
        /// </summary>
        [Required]
        public DateTime PublicationDate { get; set; }
        /// <summary>
        /// Type of publication
        /// </summary>
        [Required]
        public PublicationType PublicationType { get; set; }
        /// <summary>
        /// Id of publication house
        /// </summary>
        [Required]
        public Guid PublicationHouseId { get; set; }
        /// <summary>
        /// Count of pages
        /// </summary>
        [Required]
        public byte PageCount { get; set; }
        /// <summary>
        /// Categories of publication
        /// </summary>
        [Required]
        public ICollection<Guid> CategoryIds { get; set; }
        /// <summary>
        /// Authors of an publication
        /// </summary>
        [Required]
        public ICollection<Guid> AuthorIds { get; set; }
    }
}
