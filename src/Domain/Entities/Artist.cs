﻿using System.Collections.Generic;

namespace Domain.DataModel
{
    public class Artist : BaseEntity
    {
        /// <summary>
        /// Every song that doesn't have an Album is on an album called "unnamed" //or zero maybe gonna change it
        /// </summary>
        public IList<Album> Albums { get; } = new List<Album>();
    }
}