using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Indexing;

namespace Richinoz.Paypal.Migrations
{
    public class Migrations : DataMigrationImpl
    {
        /// <summary>
        /// default method - only runs once on the first time
        /// </summary>
        /// <returns></returns>
        public int Create()
        {

            ContentDefinitionManager.AlterTypeDefinition("Order", builder => builder.WithPart("CommonPart")
                   .WithPart("TitlePart")
                   .WithPart("AutoroutePart")
                   );

            return 1;
        }

        public int UpdateFrom1()
        {

            ContentDefinitionManager.AlterTypeDefinition("Order", builder =>
                builder.WithPart("BodyPart")
                .Creatable()
                .Draftable()
                );
            return 2;
        }
        public int UpdateFrom2() {

            var patternDefinitions = "";

            ContentDefinitionManager.AlterTypeDefinition("Order", builder =>
                builder.WithPart("BodyPart", partBuilder =>
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

        public int UpdateFrom3()
        {

            SchemaBuilder.CreateTable("OrderPartRecord", builder =>
                                                         builder.ContentPartRecord()
                                                             .Column<string>("Details")
                );

            ContentDefinitionManager.AlterTypeDefinition("Order", builder =>
                builder.WithPart("OrderPart"));
            return 4;
        }

        public int UpdateFrom4()
        {
            ContentDefinitionManager.AlterPartDefinition("OrderPart", builder =>
                builder.WithField("Genre", fld =>
                fld.OfType("TaxonomyField")
                .WithSetting("DisplayName", "Genre")
                .WithSetting("TaxonomyFieldSettings.Taxonomy", "Genre")
                .WithSetting("TaxonomyFieldSettings.LeavesOnly", "False")
                .WithSetting("TaxonomyFieldSettings.SingleChoice", "False")
                .WithSetting("TaxonomyFieldSettings.Hint", "Select as many genres as required")
                ));
            return 5;
        }

    }
}