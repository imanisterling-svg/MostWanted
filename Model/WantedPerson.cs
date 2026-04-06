using System;
using System.Collections.Generic;
using SQLite;

namespace MostWanted.Model
{
    [Table("WantedPerson")]

    public class WantedPerson : BaseEntity
    {
 

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;

       



    }
}
