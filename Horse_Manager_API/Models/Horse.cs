using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Horse_Manager_API.Models
{
    public class Horse
    {
        [Key]
        public int HorseID { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Level { get; set; }
        public string Gender { get; set; }
        public string Studbook { get; set; }
        public int CategoryID { get; set; }
        public int StableID { get; set; }
        public string Father { get; set; }
        public string Father_Father { get; set; }
        public string Mother_Father { get; set; }
        public string Mother { get; set; }
        public string Father_Mother { get; set; }
        public string Mother_Mother { get; set; }
        public string Owner { get; set; }
        public string Breeder { get; set; }
        public int Price { get; set; }
        public string Youtube_Link1 { get; set; }
        public string Youtube_Link2 { get; set; }
        public string Peedigree_Link { get; set; }
        public int RiderID { get; set; }
        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }
        public Category Category { get; set; }
    }
}
