using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Indexing;
using Pluralsight.Movies.Models;

namespace Pluralsight.Movies.Handlers
{
    public class MovieHandler : ContentHandler
    {
        private readonly IContentManager _contentManager;
        private readonly IIndexManager _indexManager;

        public MovieHandler(IRepository<MoviePartRecord> moviePartRepository,
            IContentManager contentManager,
            IIndexManager indexManager)
        {
            _contentManager = contentManager;
            _indexManager = indexManager;
            Filters.Add(StorageFilter.For(moviePartRepository));

            OnIndexing<MoviePart>((context, part) =>
            {
                foreach (var keyword in part.Keywords.Split(',').Select(k => k.Trim()))
                {
                    context.DocumentIndex.Add("movie-keywords", keyword).Analyze();
                }
            });
        }

        protected override void BuildDisplayShape(BuildDisplayContext context)
        {
            if (context.ContentItem.ContentType == "Movie" && context.DisplayType == "Detail")
            {
                var relatedMovies = GetRelatedMovies(context.ContentItem);
                if (relatedMovies.Any())
                {
                    context.Shape.SimilarMovies = context.New.SimilarMovies(Movies: relatedMovies);
                }
            }
        }

        private IEnumerable<ContentItem> GetRelatedMovies(ContentItem displayedMovie)
        {
            var searchBuilder = GetSearchBuilder();

            var movie = displayedMovie.As<MoviePart>();
            // foo,bar,fizz buzz => "foo" "bar" "fizz buzz"
            var keywords = string.Join(" ", movie.Keywords.Split(',').Select(k => '"' + k + '"'));

            var relatedItemIds = searchBuilder
                .WithField("type", "Movie").Mandatory().ExactMatch()
                .Parse("movie-keywords", keywords).Mandatory()
                .Search()
                .Where(h => h.ContentItemId != displayedMovie.Id)
                .Select(h => h.ContentItemId)
                .Take(5).ToList();

            return _contentManager.GetMany<ContentItem>(relatedItemIds, VersionOptions.Published, QueryHints.Empty);
        }

        private ISearchBuilder GetSearchBuilder()
        {
            return _indexManager.HasIndexProvider()
                       ? _indexManager.GetSearchIndexProvider().CreateSearchBuilder("Search")
                       : new NullSearchBuilder();
        }
    }
}