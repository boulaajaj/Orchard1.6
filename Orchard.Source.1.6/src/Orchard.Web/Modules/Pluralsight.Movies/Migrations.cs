using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data;
using Orchard.Data.Migration;
using Pluralsight.Movies.Models;

namespace Pluralsight.Movies {
    public class Migrations : DataMigrationImpl
    {
        private readonly IRepository<ActorRecord> _actorRepository;

        public Migrations(IRepository<ActorRecord> actorRepository) {
            _actorRepository = actorRepository;
        }

        /// <summary>
        /// default method - only runs once on the first time
        /// </summary>
        /// <returns></returns>
        public int Create() {

            ContentDefinitionManager.AlterTypeDefinition("Movie", builder => builder.WithPart("CommonPart")
                   .WithPart("TitlePart")
                   .WithPart("AutoroutePart")
                   );

            return 1;
        }

        public int UpdateFrom1() {

            ContentDefinitionManager.AlterTypeDefinition("Movie", builder => 
                builder.WithPart("BodyPart")
                .Creatable()
                .Draftable()
                );
            return 2;
        }
        public int UpdateFrom2() {
         
            var jb = new JSONBuilder();
            jb.AddNewObject();
            jb.AddProperty("Name", "Movie Title");
            jb.AddProperty("Pattern", "movies/{Content.Slug}");
            jb.AddProperty("Description", "movies/movie-titles");

            jb.AddNewObject();
            jb.AddProperty("Name", "Film Title");
            jb.AddProperty("Pattern", "films/{Content.Slug}");
            jb.AddProperty("Description", "films/film-titles");

            var patternDefinitions = jb.Build();

            ContentDefinitionManager.AlterTypeDefinition("Movie", builder =>
                builder.WithPart("BodyPart", partBuilder=>
                    partBuilder.WithSetting("BodyTypePartSettings.Flavor", "text"))
                  .WithPart("AutoroutePart", routePart => 
                    routePart.WithSetting("AutorouteSettings.PerItemConfiguration", "false")
                    .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                    .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                    .WithSetting("AutorouteSettings.PatternDefinitions", patternDefinitions)
                    .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                );
            return 3;
        }

        public int UpdateFrom3() {

            SchemaBuilder.CreateTable("MoviePartRecord", builder =>
                                                         builder.ContentPartRecord()
                                                             .Column<string>("IMDB_ID")
                                                             .Column<int>("YearReleased")
                                                             .Column<string>("Rating", col=>col.WithLength(4))
                );

            ContentDefinitionManager.AlterTypeDefinition("Movie", builder=>
                builder.WithPart("MoviePart"));
            return 4;
        }

        public int UpdateFrom4()
        {
            ContentDefinitionManager.AlterPartDefinition("MoviePart", builder=>
                builder.WithField("Genre", fld=>
                fld.OfType("TaxonomyField")
                .WithSetting("DisplayName", "Genre")
                .WithSetting("TaxonomyFieldSettings.Taxonomy", "Genre")
                .WithSetting("TaxonomyFieldSettings.LeavesOnly", "False")
                .WithSetting("TaxonomyFieldSettings.SingleChoice", "False")
                .WithSetting("TaxonomyFieldSettings.Hint", "Select as many genres as required")
                ));
            return 5;
        }

        public int UpdateFrom5() {
            SchemaBuilder.CreateTable("ActorRecord", table =>
                table
                .Column<int>("Id", col => col.PrimaryKey().Identity())
                .Column<string>("Name"));

            SchemaBuilder.CreateTable("MovieActorRecord", table =>
                table
                    .Column<int>("Id", col => col.PrimaryKey().Identity())
                    .Column<int>("MoviePartRecord_Id")
                    .Column<int>("ActorRecord_Id"));
            return 6;
        }

        public int UpdateFrom6() {
            _actorRepository.Create(new ActorRecord(){Name = "Mark Hamill"});
            _actorRepository.Create(new ActorRecord(){Name = "Carrie Fisher"});
            _actorRepository.Create(new ActorRecord(){Name = "Harrison Ford"});
            _actorRepository.Create(new ActorRecord(){Name = "Peter Cushing"});
            return 7;
        }

    }
}