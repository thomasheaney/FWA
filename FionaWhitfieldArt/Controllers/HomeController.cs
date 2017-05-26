using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web;
using Umbraco.Core.Models;
using Archetype.Models;
using FionaWhitfieldArt.Models;
using FionaWhitfieldArt.Library.Models;
using FionaWhitfieldArt.Library.Helpers;

namespace FionaWhitfieldArt.Controllers
{
    public class HomeController : SurfaceController
    {

        private string GetViewPath(string name)
        {
            return "~/Views/Partials/Home/" + name +".cshtml";
        }

        private const int MAXIMUN_TESTIMONIALS = 4;

        public ActionResult RenderFeatured()
        {
            HomeHelper homeHelper = new HomeHelper(CurrentPage, new UmbracoHelper(UmbracoContext.Current));
            List<FeaturedItem> model = homeHelper.GetFeaturedItemsModel();

            return PartialView(GetViewPath("_Featured"), model);
        }
  

        public ActionResult RenderBlog()
        {
            HomeHelper homeHelper = new HomeHelper(CurrentPage, new UmbracoHelper(UmbracoContext.Current));
            LatestBlogPosts model = homeHelper.GetLatestBlogPosts();
            return PartialView(GetViewPath("_Blog"), model);
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
            return PartialView(GetViewPath("_Testimonials"), model);
        }


    }
}