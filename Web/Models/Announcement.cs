using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        [Required]
        [StringLength(244)]
        public String Title { get; set; }
        public DateTime DateTime { get; set; }
        public String Author { get; set; }
        [Required]
        public string Content { get; set; }
        //link announcement to user
        public virtual ApplicationUser User { get; set; }
    }
}