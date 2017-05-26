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

        private string PartialViewPath(string name)
        {
            return "~/Views/Partials/Home/" + name +".cshtml";
        }

        private const int MAXIMUN_TESTIMONIALS = 4;

        public ActionResult RenderFeatured()
        {
            List<FeaturedItem> model = new List<FeaturedItem>();
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");
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

            return PartialView(PartialViewPath("_Featured"), model);
        }

        public ActionResult RenderServices()
        {
            return PartialView(PartialViewPath("_Services"));
        }

        public ActionResult RenderBlog()
        {
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");

            string title = homePage.GetPropertyValue<string>("latestBlogPostsTitle");
            string introduction = homePage.GetPropertyValue("latestBlogPostsintroduction").ToString();

            LatestBlogPosts model = new LatestBlogPosts(title, introduction);
            return PartialView(PartialViewPath("_Blog"), model);
        }

        public ActionResult RenderTestimonials()
        {
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");

            string title = homePage.GetPropertyValue<string>("testimonialsTitle");
            string introduction = homePage.GetPropertyValue("testimonialsIntroduction").ToString();

            List<Testimonial> testimonialList = new List<Testimonial>();

            Testimonials model = new Testimonials(title, introduction, testimonialList);

            ArchetypeModel archetypeTestimonialList = homePage.GetPropertyValue<ArchetypeModel>("testimonialList");
            if (archetypeTestimonialList != null)
            {
                foreach (ArchetypeFieldsetModel testimonial in archetypeTestimonialList.Take(MAXIMUN_TESTIMONIALS))
                {
                    string name = testimonial.GetValue<string>("name");
                    string quote = testimonial.GetValue<string>("quote");
                    testimonialList.Add(new Testimonial(quote, name));

                }
            }
            return PartialView(PartialViewPath("_Testimonials"), model);
        }


    }
}