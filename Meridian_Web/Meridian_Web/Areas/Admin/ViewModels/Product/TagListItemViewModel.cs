﻿namespace Meridian_Web.Areas.Admin.ViewModels.Product
{
    public class TagListItemViewModel
    {

        public int Id { get; set; }
        public string Tagname { get; set; }
        public TagListItemViewModel(int ıd, string tagname)
        {
            Id = ıd;
            Tagname = tagname;
        }


      
    }
}
