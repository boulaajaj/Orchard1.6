using Contrib.Taxonomies.Models;
using Contrib.Taxonomies.Services;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Environment;
using Orchard.Environment.Extensions.Models;

namespace Pluralsight.Movies.Handlers {
    public class MovieFeatureEventHandler : IFeatureEventHandler {
        private readonly ITaxonomyService _taxonomyService;
        private readonly IContentManager _contentManager;

        public MovieFeatureEventHandler(ITaxonomyService taxonomyService,
            IContentManager contentManager) {
            _taxonomyService = taxonomyService;
            _contentManager = contentManager;
        }

        public void Installing(Feature feature) {
        }

        public void Installed(Feature feature) {
        }

        public void Enabling(Feature feature) {
        }

        public void Enabled(Feature feature) {
            // if Genre taxonomy does not exist
            //    create Genre taxonomy
            //    create several genre terms in the taxonomy
            if (_taxonomyService.GetTaxonomyByName("Genre") == null) {
                var genre = _contentManager.New<TaxonomyPart>("Taxonomy");
                genre.Name = "Genre";
                _contentManager.Create(genre, VersionOptions.Published);

                CreateTerm(genre, "Action");
                CreateTerm(genre, "Adventure");
                CreateTerm(genre, "Animation");
                CreateTerm(genre, "Comedy");
                CreateTerm(genre, "Crime");
                CreateTerm(genre, "Documentary");
                CreateTerm(genre, "Drama");
            }
        }

        private void CreateTerm(TaxonomyPart genre, string genreName) {
            var term = _taxonomyService.NewTerm(genre);
            term.Name = genreName;
            _contentManager.Create(term, VersionOptions.Published);
        }

        public void Disabling(Feature feature) {
        }

        public void Disabled(Feature feature) {
        }

        public void Uninstalling(Feature feature) {
        }

        public void Uninstalled(Feature feature) {
        }
    }
}