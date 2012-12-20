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
            string PatternDefinitions = "[{Name: 'Movie Title', Pattern: 'movies/{Content.Slug}', Description: 'movies/movie-titles' }, {Name: 'Film Title', Pattern: 'films/{Content.Slug}', Description: 'films/film-titles'}]";

            ContentDefinitionManager.AlterTypeDefinition("Movie", builder =>
                builder.WithPart("BodyPart", partBuilder=>
                    partBuilder.WithSetting("BodyTypePartSettings.Flavor", "text"))
                  .WithPart("AutoroutePart", routePart => 
                    routePart.WithSetting("AutorouteSettings.PerItemConfiguration", "false")
                    .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                    .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                    .WithSetting("AutorouteSettings.PatternDefinitions", PatternDefinitions)
                    .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                );
            return 3;
        }
    }
}