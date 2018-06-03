using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Horse_Manager_API.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Zip_Code { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public int Subscription_PlanID { get; set; }

        public ICollection<Stable> Own_Stables { get; set; }
        public ICollection<Worker> Work { get; set; }
        //public ICollection<Stable> Other_Stables { get; set; }
    }
}