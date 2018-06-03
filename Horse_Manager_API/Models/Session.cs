using System;
using System.ComponentModel.DataAnnotations;

namespace Horse_Manager_API.Models
{
    public class Session
    {
        [Key]
        public int UserID { get; set; }
        public string Language { get; set; }
        public string LastVisitedPage { get; set; }
    }
}
