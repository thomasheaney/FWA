﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using FionaWhitfieldArt.Models;
using Umbraco.Web;
using Umbraco.Core.Models;

namespace FionaWhitfieldArt.Controllers
{
    public class BlogController :SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Blog/";

        public ActionResult RenderPostList()
        {
            List<BlogPreview> model = new List<BlogPreview>();
            IPublishedContent blogPage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.DocumentTypeAlias == "blog").FirstOrDefault();

            foreach (IPublishedContent page in blogPage.Children.OrderByDescending( x => x.UpdateDate))
            {
                int imageId = page.GetPropertyValue<int>("articleImage");
                var mediaItem = Umbraco.Media(imageId);
                model.Add(new BlogPreview(page.Name, page.GetPropertyValue<string>("articleIntro"), mediaItem.Url, page.Url));
            }

            return PartialView(PARTIAL_VIEW_FOLDER + "_PostList.cshtml", model);
        }

    }
}