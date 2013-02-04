using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data;
using Orchard.Data.Migration;
using Orchard.Indexing;

namespace RichSite {
    public class Migrations : DataMigrationImpl {

        public const string EMailRegexPattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})";

        /// <summary>
        /// default method - only runs once on the first time
        /// </summary>
        /// <returns></returns>
        public int Create()
        {

            ContentDefinitionManager.AlterTypeDefinition("RichDependency", builder => builder.WithPart("CommonPart")
                   .WithPart("TitlePart")
                   .WithPart("AutoroutePart")
                   );

            return 1;
        }

        public int UpdateFrom1()
        {

            ContentDefinitionManager.AlterTypeDefinition("RichDependency", builder =>
                builder.WithPart("BodyPart")
                .Creatable()
                .Draftable()
                );
            return 2;
        }

        //public int UpdateFrom2()
        //{

        //    SchemaBuilder.CreateTable("RichDependencyPartRecord", builder =>
        //                                                 builder.ContentPartRecord()
        //                                                     .Column<string>("Name")
        //        );

        //    ContentDefinitionManager.AlterTypeDefinition("Movie", builder =>
        //        builder.WithPart("MoviePart"));
        //    return 3;
        //}

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterPartDefinition("RichDependencyPart", builder =>
                builder.WithField("Name", fld =>
                    fld.OfType("InputField")
                    .WithSetting("DisplayName", "Name")
                    .WithSetting("InputFieldSettings.Type", "Text")
                    .WithSetting("InputFieldSettings.Required", "True")
                    .WithSetting("InputFieldSettings.AutoFocus", "True")
                    .WithSetting("InputFieldSettings.AutoComplete", "True")
                ));

            ContentDefinitionManager.AlterTypeDefinition("RichDependency", builder =>
                builder.WithPart("RichDependencyPart"));
            return 3;
        }

        public int UpdateFrom3()
        {
            ContentDefinitionManager.AlterPartDefinition("RichDependencyPart", builder =>
                builder.WithField("EMail", fld =>
                    fld.OfType("InputField")
                    .WithSetting("DisplayName", "Email")
                    .WithSetting("InputFieldSettings.Type", "Text")
                    .WithSetting("InputFieldSettings.Required", "True")                    
                    .WithSetting("InputFieldSettings.AutoComplete", "True")
                    .WithSetting("InputFieldSettings.Pattern", EMailRegexPattern)
                ));

            ContentDefinitionManager.AlterPartDefinition("RichDependencyPart", builder =>
                builder.WithField("NewsLetter", fld =>
                    fld.OfType("BooleanField")
                    .WithSetting("DisplayName", "Receive Newsletter")
                    .WithSetting("BooleanFieldSettings.SelectionMode", "Checkbox")
                ));
           
            ContentDefinitionManager.AlterPartDefinition("RichDependencyPart", builder =>
                builder.WithField("Message", fld =>
                    fld.OfType("TextField")
                    .WithSetting("DisplayName", "Message")                   
                ));
         
            return 4;
        }

    }
}