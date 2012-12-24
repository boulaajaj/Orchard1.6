using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Data;
using Pluralsight.Movies.Models;
using Pluralsight.Movies.ViewModels;

namespace Pluralsight.Movies.Drivers {
    public class MoviePartDriver:ContentPartDriver<MoviePart> {
        private readonly IRepository<ActorRecord> _actorRepository;

        public MoviePartDriver(IRepository<ActorRecord> actorRepository) {
            _actorRepository = actorRepository;
        }

        protected override string Prefix
        {
            get { return "Movie"; }
        }

        protected override DriverResult Display(MoviePart part, string displayType, dynamic shapeHelper)
        {
            //displayType could be Detail, Summary, Summary_Admin
            return ContentShape("Parts_Movie", () => shapeHelper.Parts_Movie(MoviePart: part));
        }
       //get
        protected override DriverResult Editor(MoviePart part, dynamic shapeHelper) {
            return ContentShape("Parts_Movie_Edit", () =>
                                                    shapeHelper.EditorTemplate(TemplateName: "Parts/Movie", Model: BuildEditorViewModel(part), Prefix: Prefix));
        }

        //post
        protected override DriverResult Editor(MoviePart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        private MovieEditViewModel BuildEditorViewModel(MoviePart part) {
            return new MovieEditViewModel() {
                IMDB_Id = part.IMDB_Id,
                YearReleased = part.YearReleased,
                Rating = part.Rating,
                Actors = part.Cast.Select(c=>c.Id).ToList(),
                AllActors = _actorRepository.Table.OrderBy(a=>a.Name).ToList()
            };
        }
    }
}