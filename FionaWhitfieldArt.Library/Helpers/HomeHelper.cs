using Archetype.Models;
using FionaWhitfieldArt.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace FionaWhitfieldArt.Library.Helpers
{
    public class HomeHelper
    {
        private IPublishedContent _currentPage;
        private UmbracoHelper _uHelper;
        private string _homeDocTypeAlias;
        private string _featuredItemsAlias;
        private string _nameAlias;
        private string _imageAlias;
        private string _pageAlias;
        private string _categoryAlias;

        public HomeHelper(IPublishedContent currentPage, UmbracoHelper uHelper, string homeDocTypeAlias = "home",
            string featuredItemsAlias = "featuredItems", string nameAlias = "name", string imageAlias = "image", string pageAlias = "page", string categoryAlias = "category")
        {
            _currentPage = currentPage;
            _uHelper = uHelper;
            _homeDocTypeAlias = homeDocTypeAlias;
            _featuredItemsAlias = featuredItemsAlias;
            _nameAlias = nameAlias;
            _imageAlias = imageAlias;
            _pageAlias = pageAlias;
            _categoryAlias = categoryAlias;

        }

        public List<FeaturedItem> GetFeaturedItemsModel()
        {
            List<FeaturedItem> model = new List<FeaturedItem>();
            IPublishedContent homePage = _currentPage.AncestorOrSelf(_homeDocTypeAlias);
            ArchetypeModel featuredItems = homePage.GetPropertyValue<ArchetypeModel>(_featuredItemsAlias);

            foreach (ArchetypeFieldsetModel fieldSet in featuredItems)
            {
                string imageUrl = fieldSet.GetValue<IPublishedContent>(_imageAlias).Url;
                string linkUrl = fieldSet.GetValue<IPublishedContent>(_pageAlias).Url;

                model.Add(new FeaturedItem(fieldSet.GetValue<string>(_nameAlias), fieldSet.GetValue<string>(_categoryAlias), imageUrl, linkUrl));
            }
            return model;
        }


        public LatestBlogPosts GetLatestBlogPosts()
        {
            IPublishedContent homePage = _currentPage.AncestorOrSelf(_homeDocTypeAlias);

            string title = homePage.GetPropertyValue<string>("latestBlogPostsTitle");
            string introduction = homePage.GetPropertyValue("latestBlogPostsintroduction").ToString();

            LatestBlogPosts model = new LatestBlogPosts(title, introduction);
            return model;
        }

    }
}
