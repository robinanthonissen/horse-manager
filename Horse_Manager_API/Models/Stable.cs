using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Horse_Manager_API.Models
{
    public class Stable
    {
        [Key]
        public int StableID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zip_Code { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string About { get; set; }
        public int UserID { get; set; }

        //public User Manager { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Worker> Workers { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Horse> Horses { get; set; }
    }
}
