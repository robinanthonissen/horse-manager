using System;
using System.ComponentModel.DataAnnotations;

namespace Horse_Manager_API.Models
{
    public class Worker
    {
        [Key]
        public int WorkerID { get; set; }
        public int UserID { get; set; }
        public int StableID { get; set; }
        public string Job { get; set; }

        public User User { get; set; }
        public Stable Stable { get; set; }
    }
}
