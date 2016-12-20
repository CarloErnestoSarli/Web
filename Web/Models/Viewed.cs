using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Viewed
    {
        public int AnnouncementId { get; set; }
        public bool Seen { get; set; }
        public List<ApplicationUser> Students { get; set; }
    }
}