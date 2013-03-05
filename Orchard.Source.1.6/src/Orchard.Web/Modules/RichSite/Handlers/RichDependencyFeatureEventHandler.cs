using Contrib.Taxonomies.Models;
using Contrib.Taxonomies.Services;
using Orchard.ContentManagement;
using Orchard.Environment;
using Orchard.Environment.Extensions.Models;

namespace RichSite.Handlers
{
    public class RichDependencyFeatureEventHandler : IFeatureEventHandler
    {
        private readonly ITaxonomyService _taxonomyService;
        private readonly IContentManager _contentManager;

        public RichDependencyFeatureEventHandler(ITaxonomyService taxonomyService,
            IContentManager contentManager)
        {
            _taxonomyService = taxonomyService;
            _contentManager = contentManager;
        }

        public void Installing(Feature feature)
        {

        }

        public void Installed(Feature feature)
        {

        }

        public void Enabling(Feature feature)
        {

        }

        public void Enabled(Feature feature)
        {
           CreateTaxonomies();
        }

        private void CreateTaxonomies() {
            //if genre taxonomy not defined - the ncreate
            if (_taxonomyService.GetTaxonomyByName("Category") == null)
            {
                var genre = _contentManager.New<TaxonomyPart>("Taxonomy");
                genre.Name = "Category";
                _contentManager.Create(genre, VersionOptions.Published);

                CreateTerm(genre, "Clothes");
                //CreateTerm(genre, "Adventure");
                //CreateTerm(genre, "Animation");
                //CreateTerm(genre, "Comedy");
                //CreateTerm(genre, "Drama");
                //CreateTerm(genre, "Crime");
                //CreateTerm(genre, "Documentary");       

            }
        }

        private void CreateTerm(TaxonomyPart genre, string genreName)
        {
            var term = _taxonomyService.NewTerm(genre);
            term.Name = genreName;
            _contentManager.Create(term, VersionOptions.Published);
        }

        public void Disabling(Feature feature)
        {

        }

        public void Disabled(Feature feature)
        {

        }

        public void Uninstalling(Feature feature)
        {

        }

        public void Uninstalled(Feature feature)
        {

        }
    }
}