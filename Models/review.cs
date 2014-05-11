using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace beachmaster.Models
{
    public class review
    {
        [Required]
        public int reviewId { get; set; }
        [Required]
        public int beachId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public System.DateTime reviewDate { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string text { get; set; }
    }
}