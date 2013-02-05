using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data;
using Orchard.Data.Migration;
using Orchard.Indexing;
using Orchard.Rules.Models;
using Orchard.Rules.Services;

namespace RichSite
{
    public class Migrations : DataMigrationImpl
    {
        private readonly IRulesServices _rulesServices;

        public Migrations(IRulesServices rulesServices)
        {
            _rulesServices = rulesServices;
        }

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

        public int UpdateFrom4()
        {
            const string actionParameters = "<Form><Recipient>other</Recipient><RecipientOther>rich__smith@hotmail.com</RecipientOther><Subject>from {Content.Fields.RichDependencyPart.Name} </Subject><Body>{Content.Fields.RichDependencyPart.Message}</Body><__RequestVerificationToken>qseK-jMuPJ_Q6nbzdaAmHTGRyQcSKohKW07CO6mDdx4e6e9ptYAOyyNpt26Kct2TH_zV4Ze4DZiqTqKzK4DjZLbX9pU5KWm86dMqO8r7bt1myEPDIQqaWsQFzCK-oFEAdPHX3A2</__RequestVerificationToken><op>Save</op></Form>";
            const string eventParameters = "<Form><contenttypes>RichDependency</contenttypes><__RequestVerificationToken>hY7JeLxufPjMEeAw8DkYy6w5t4iH-kdP-YoFS-tYx0_ss3idy933F3IYjKeBY88ZPfGJKY9cB-y4zqlpbLjpSl8eJxIgoGmQXaFr0YXqsqSZPYQsKP3it-OSpBOoufYu5n85eg2</__RequestVerificationToken><op>Save</op></Form>";

            var rule = _rulesServices.CreateRule("SendEmail");

            rule.Actions.Add(new ActionRecord
            {
                Category = "Messaging",
                Type = "SendEmail",
                Position = rule.Actions.Count + 1,
                Parameters = actionParameters
            });

            rule.Events.Add(new EventRecord() {
                Category = "CustomForm",
                Type="Submitted",
                Parameters = eventParameters
            });

            return 5;
        }

    }
}