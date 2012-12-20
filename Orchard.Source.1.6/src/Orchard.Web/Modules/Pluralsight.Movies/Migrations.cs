using System;
using System.Collections.Generic;
using System.Data;
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
                   .WithPart("AutoroutePart"));

            return 1;
        }

        public 
    }
}