using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class AnnouncementView
    {
        public Announcement Announcement;
        public Comment Comment;
        public List<Comment> Comments;
    }
}