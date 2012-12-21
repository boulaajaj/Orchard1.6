using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Pluralsight.Movies {
    public class Migrations : DataMigrationImpl
    {
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


    }
}