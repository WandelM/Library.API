﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Dtos
{
    public class PublicationHouseUpdateModel
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
