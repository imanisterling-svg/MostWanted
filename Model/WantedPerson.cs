using MostWanted.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MostWanted.Model
{
   



    [Table("WantedPerson")]

    public class WantedPerson
    {
        [PrimaryKey, AutoIncrement]

       
        public int Id { get; set; }

      
        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

    
        public string ImagePath { get; set; }
    }





    public class ApiResponse
    {
        public string Status { get; set; }
        public List<WantedPerson> Data { get; set; }
    }


}
