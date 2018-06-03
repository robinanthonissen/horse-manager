using System;
using System.ComponentModel.DataAnnotations;

namespace Horse_Manager_API.Models
{
    public class Subscription_Plan
    {
        [Key]
        public int Subscription_PlanID { get; set; }
        public string Subscription_Plan_Name { get; set; }
        public int Price { get; set; }
    }
}
