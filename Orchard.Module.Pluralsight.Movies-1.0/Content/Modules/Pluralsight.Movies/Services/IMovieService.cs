using System.Collections.Generic;
using Orchard;
using Pluralsight.Movies.Models;
using Pluralsight.Movies.ViewModels;

namespace Pluralsight.Movies.Services {
    public interface IMovieService : IDependency {
        void UpdateMovie(MovieEditViewModel viewModel, MoviePart part);
        void ImportMovie(int tmdbMovieId);
        void ImportMovies(IEnumerable<int> tmdbMovieIds);
    }
}