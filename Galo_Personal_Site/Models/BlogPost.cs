using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Galo_Personal_Site.Models
{
    public class BlogPost
    {
        public BlogPost()
        {
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset Created { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? Updated { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public string MediaURL { get; set; }
        public bool Published { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}