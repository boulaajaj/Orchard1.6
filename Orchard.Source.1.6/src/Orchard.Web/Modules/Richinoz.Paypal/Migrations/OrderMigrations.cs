using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Indexing;

namespace Richinoz.Paypal.Migrations
{
    public class OrderMigrations : DataMigrationImpl
    {
        /// <summary>
        /// default method - only runs once on the first time
        /// </summary>
        /// <returns></returns>
        public int Create()
        {

            ContentDefinitionManager.AlterTypeDefinition("Order", builder => builder.WithPart("CommonPart"));

            return 1;
        }

        public int UpdateFrom1()
        {

            SchemaBuilder.CreateTable("OrderPartRecord", builder =>
                                                         builder.ContentPartRecord()
                                                             .Column<string>("Details",col=>col.Unlimited())
                );

            ContentDefinitionManager.AlterTypeDefinition("Order", builder =>
                builder.WithPart("OrderPart"));
            return 2;
        }


        public int UpdateFrom2() {

            SchemaBuilder.AlterTable("OrderPartRecord", table =>
                table.AddColumn<decimal>("Amount"));

            SchemaBuilder.AlterTable("OrderPartRecord", table =>
                table.AddColumn<string>("TransactionId", col=>col.WithLength(100)));

            SchemaBuilder.AlterTable("OrderPartRecord", table =>
                table.AddColumn<string>("Status",col=>col.WithLength(10)));

            return 3;

        }

        public int UpdateFrom3() {
           
            return 4;
        }

        public int UpdateFrom4()
        {
            ContentDefinitionManager.AlterTypeDefinition("Order", builder =>
                 builder.
                 WithPart("TitlePart"));
            return 5;
        }


    }
}