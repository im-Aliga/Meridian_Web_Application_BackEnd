﻿using Meridian_Web.Database.Models.Common;

namespace Meridian_Web.Database.Models
{
    public class BlogTag : BaseEntity<int>, IAuditable
    {
        public string TagName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        List<BlogAndBlogTag> Tags { get; set; }


    }
}
