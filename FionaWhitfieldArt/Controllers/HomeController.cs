using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using FionaWhitfieldArt.Models;
using Umbraco.Web;
using Umbraco.Core.Models;
using Archetype.Models;

namespace FionaWhitfieldArt.Controllers
{
    public class HomeController : SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Home/";

        public ActionResult RenderFeatured()
        {
            List<FeaturedItem> model = new List<FeaturedItem>();
            IPublishedContent homePage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.DocumentTypeAlias == "home").FirstOrDefault();
            ArchetypeModel featuredItems = homePage.GetPropertyValue<ArchetypeModel>("featuredItems");

            foreach (ArchetypeFieldsetModel fieldSet in featuredItems)
            {
                /*****************************************************
                 *  Published code doesn't work with this version of Umbraco/Archetype
                 * string imageID = fieldSet.GetValue<string>("image");
                var mediaItem = Umbraco.Media(imageID);
                string imageUrl = mediaItem.Url;

                string pageId = fieldSet.GetValue<string>("page");
                IPublishedContent linkedToPage = Umbraco.TypedContent(pageId);
                string linkUrl = linkedToPage.Url;*/

                string imageUrl = fieldSet.GetValue<IPublishedContent>("image").Url;
                string linkUrl = fieldSet.GetValue<IPublishedContent>("page").Url;

                model.Add(new FeaturedItem(fieldSet.GetValue<string>("name"),fieldSet.GetValue<string>("category"), imageUrl, linkUrl));
            }

            return PartialView(PARTIAL_VIEW_FOLDER  + "_Featured.cshtml", model);
        }

        public ActionResult RenderServices()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Services.cshtml");
        }

        public ActionResult RenderBlog()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Blog.cshtml");
        }

        public ActionResult RenderClients()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Clients.cshtml");
        }
    }
}