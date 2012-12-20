using Autofac;
using Moq;
using NUnit.Framework;
using Orchard.Messaging.Events;
using Orchard.Messaging.Services;
using Orchard.Tests.Messaging;
using Pluralsight.Movies;

namespace Orchard.Tests.Modules.PluralSight.Movies {
    [TestFixture]
    public class PluralSightMoviesTests {
       

        [SetUp]
        public void Init() {
           
        }
    
        [Test]
        public void can_add_one_JSONObject()
        {

            string patternDefinitions = "[{Name: 'Movie Title',Pattern: 'movies/{Content.Slug}',Description: 'movies/movie-titles'}]";
            

            var jb = new JSONBuilder();
            jb.AddNewObject();
            jb.AddProperty("Name", "Movie Title");
            jb.AddProperty("Pattern", "movies/{Content.Slug}");
            jb.AddProperty("Description", "movies/movie-titles");

            Assert.AreEqual(patternDefinitions, jb.Build());

        }

        [Test]
        public void can_add_two_JSONObjects()
        {

            string patternDefinitions = "[{Name: 'Movie Title',Pattern: 'movies/{Content.Slug}',Description: 'movies/movie-titles'},{Name: 'Film Title',Pattern: 'films/{Content.Slug}',Description: 'films/film-titles'}]";

            var jb = new JSONBuilder();
            jb.AddNewObject();
            jb.AddProperty("Name", "Movie Title");
            jb.AddProperty("Pattern", "movies/{Content.Slug}");
            jb.AddProperty("Description", "movies/movie-titles");

            jb.AddNewObject();
            jb.AddProperty("Name", "Film Title");
            jb.AddProperty("Pattern", "films/{Content.Slug}");
            jb.AddProperty("Description", "films/film-titles");

            Assert.AreEqual(patternDefinitions, jb.Build());
        }
    }
}