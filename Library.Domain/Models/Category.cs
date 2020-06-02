using System;

namespace Library.Domain.Models
{
    /// <summary>
    /// Category od publication model
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Id of category
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of category
        /// </summary>
        public string Name { get; set; }
    }
}