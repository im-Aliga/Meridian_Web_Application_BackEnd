﻿namespace Meridian_Web.Areas.Client.ViewModels.Home
{
    public class BlogListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TagList> Tags { get; set; }
        public string ImageUrl { get; set; }
        public BlogListItemViewModel(string title, string description, DateTime createdAt, List<TagList> tags, string ımageUrl, int ıd)
        {
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            Tags = tags;
            ImageUrl = ımageUrl;
            Id = ıd;
        }



    }
    public class TagList
    {

        public string Tagname { get; set; }
        public TagList(string tagname)
        {
            Tagname = tagname;
        }

    }
}
