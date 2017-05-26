using System;
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
        private string PartialViewPath(string name)
        {
            return "~/Views/Partials/Blog/" + name + ".cshtml";
        }

        public ActionResult RenderPostList(int numberOfItems)
        {
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");
            List<BlogPreview> model = new List<BlogPreview>();
            IPublishedContent blogPage = homePage.Children.Where(x => x.DocumentTypeAlias == "blog").FirstOrDefault();

            foreach (IPublishedContent page in blogPage.Children.OrderByDescending( x => x.UpdateDate).Take(numberOfItems))
            {
                int imageId = page.GetPropertyValue<int>("articleImage");
                var mediaItem = Umbraco.Media(imageId);
                model.Add(new BlogPreview(page.Name, page.GetPropertyValue<string>("articleIntro"), mediaItem.Url, page.Url));
            }

            return PartialView(PartialViewPath("_PostList"), model);
        }

    }
}