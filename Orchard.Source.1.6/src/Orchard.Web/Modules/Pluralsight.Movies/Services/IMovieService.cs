using Orchard;

namespace Pluralsight.Movies.Services {
    public interface IMovieService :IDependency {

        void UpdateMovie(ViewModels.MovieEditViewModel movieEditViewModel, Models.MoviePart part);
    }
}