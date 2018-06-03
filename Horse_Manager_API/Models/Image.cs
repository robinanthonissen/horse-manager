using System;
using System.ComponentModel.DataAnnotations;

namespace Horse_Manager_API.Models
{
    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        public string ParentObjectType { get; set; }
        public int ParentObjectID { get; set; }
        public string ImageURL { get; set; }
    }
}
