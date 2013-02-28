using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace Nwazet.Commerce.Migrations {
    [OrchardFeature("Nwazet.Paypal.Checkout")]
    public class PaypalCheckoutMigrations : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("PaypalCheckoutSettingsPartRecord", table => table
                .ContentPartRecord()
                .Column<string>("MerchantId")
                .Column<string>("AnalyticsId")
                .Column<string>("Currency", column => column.WithDefault("AUD"))
                .Column<string>("WeightUnit", column => column.WithDefault("LB"))
                .Column<bool>("UseSandbox", column => column.WithDefault(true))
                .Column<string>("ReturnUrl")
                .Column<string>("CancelUrl")
                .Column<string>("NotifyUrl")
            );

            return 1;
        }
    }
}
