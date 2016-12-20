using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ViewedPost
    {
        [Key]
        public int ViewedId { get; set; }
        public int AnnouncementId { get; set; }
        public bool Seen { get; set; }
        public List<ApplicationUser> Students { get; set; }
    }
}