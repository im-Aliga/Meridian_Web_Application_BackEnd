﻿namespace Meridian_Web.Areas.Admin.ViewModels.Banner
{
    public class ListBannerViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string MainContext { get; set; }
        public string Context { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListBannerViewModel(int ıd, string title, string mainContext, string context, string ımageUrl, DateTime createdAt, DateTime updatedAt)
        {
            Id = ıd;
            Title = title;
            MainContext = mainContext;
            Context = context;
            ImageUrl = ımageUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
