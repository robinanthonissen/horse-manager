using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Horse_Manager_API.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int StableID { get; set; }
        public int HorseCount { get; set; }

        //public ICollection<Horse> Horses { get; set; }
    }
}
