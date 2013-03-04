using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.Records;
using Orchard.Core.Contents;
using Orchard.Core.Contents.Extensions;
using Orchard.Core.Title.Models;
using Orchard.CustomForms.Models;
using Orchard.Data;
using Orchard.Data.Migration;
using Orchard.Email.Models;
using Orchard.Indexing;
using Orchard.Rules.Models;
using Orchard.Rules.Services;
using Orchard.Security;
using Orchard.Users.Models;

namespace RichSite
{
    public class Migrations : DataMigrationImpl
    {
        private readonly IRulesServices _rulesServices;
        private readonly IOrchardServices _orchardServices;
        private readonly IRepository<ContentTypeRecord> _contentTypeRepository;
        private readonly IRepository<ContentItemRecord> _contentItemRepository;
        private readonly IRepository<ContentItemVersionRecord> _contentItemVersionRepository;
        private readonly IMembershipService _membershipService;

        public Migrations(IRulesServices rulesServices,
            IOrchardServices orchardServices,
            IRepository<ContentTypeRecord> contentTypeRepository,
            IRepository<ContentItemRecord> contentItemRepository,
            IRepository<ContentItemVersionRecord> contentItemVersionRepository,
            IMembershipService membershipService)
        {
            _rulesServices = rulesServices;
            _orchardServices = orchardServices;
            _contentTypeRepository = contentTypeRepository;
            _contentItemRepository = contentItemRepository;
            _contentItemVersionRepository = contentItemVersionRepository;
            _membershipService = membershipService;
        }

        public const string EMailRegexPattern = UserPart.EmailPattern;// @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})";

        /// <summary>
        /// default method - only runs once on the first time
        /// </summary>
        /// <returns></returns>
        public int Create()
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


            //bolt onto content type!
            ContentDefinitionManager.AlterTypeDefinition("RichDependency", builder =>
                builder.WithPart("CommonPart")
                .WithPart("RichDependencyPart")                
                );
            return 1;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition("RichDependencyPart", builder =>
                builder.WithField("EMail", fld =>
                    fld.OfType("InputField")
                    .WithSetting("DisplayName", "Email")
                    .WithSetting("InputFieldSettings.Type", "Text")
                    .WithSetting("InputFieldSettings.Required", "True")
                    .WithSetting("InputFieldSettings.AutoComplete", "True")
                    .WithSetting("InputFieldSettings.Pattern", UserPart.EmailPattern)
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


            return 2;
        }

        public int UpdateFrom2()
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

            rule.Events.Add(new EventRecord()
            {
                Category = "CustomForm",
                Type = "Submitted",
                Parameters = eventParameters
            });

            return 3;
        }

        public int UpdateFrom3()
        {

            //var contentItem = _orchardServices.ContentManager.New("CustomForm");

            //Create(contentItem, VersionOptions.Draft);

            //Publish(contentItem);

            CreateCustomForm("RichTitle");

            return 4;
        }

        public int UpdateFrom4()
        {
            ContentDefinitionManager.AlterPartDefinition("ProductPart", builder =>
                builder.WithField("Category", fld =>
                fld.OfType("TaxonomyField")
                .WithSetting("DisplayName", "Category")
                .WithSetting("TaxonomyFieldSettings.Taxonomy", "Category")
                .WithSetting("TaxonomyFieldSettings.LeavesOnly", "False")
                .WithSetting("TaxonomyFieldSettings.SingleChoice", "False")
                .WithSetting("TaxonomyFieldSettings.Hint", "Select as many categories as required")
                ));
            return 5;
        }

        public int UpdateFrom5()
        {
            var smtpSettingsPart = _orchardServices.WorkContext.CurrentSite.As<SmtpSettingsPart>();

            smtpSettingsPart.Address = "info@richinoz.com";
            smtpSettingsPart.Host = "mail.richinoz.com";
            smtpSettingsPart.Port = 26;
            smtpSettingsPart.RequireCredentials = true;
            smtpSettingsPart.UserName = "Art1stLeeds@richinoz.com";
            smtpSettingsPart.Password = "B0llocks";

            return 6;
        }

        public int UpdateFrom6() {

            var user = _orchardServices.ContentManager.New<IUser>("User");
            user = _membershipService.CreateUser(new CreateUserParams(
                                                 "Test",
                                                 "tester1",
                                                 "test@richinoz.com",
                                                 null, null, true));

            user.
            return 7;
        }

        private void CreateCustomForm(string title) {
            const string subscriberFormDependency = "RichDependency";

            var formItem = _orchardServices.ContentManager.New("CustomForm");

            Create(formItem, VersionOptions.Draft);

            Publish(formItem);

            var form = _orchardServices.ContentManager.Get(formItem.Id);

            var customForm = form.As<CustomFormPart>();

            customForm.ContentType = subscriberFormDependency;
            var contentItem = _orchardServices.ContentManager.New(customForm.ContentType);

            
            var titlePart = form.As<TitlePart>();
            titlePart.Title = title;

            Create(contentItem, VersionOptions.Draft);

            contentItem.As<ICommonPart>().Container = customForm.ContentItem;

            customForm.CustomMessage = true;
            customForm.Message = string.Format("Thanks '{{Content.Fields.{0}Part.Name}}' - your message has been sent!", subscriberFormDependency);

        }
        private void Create(ContentItem contentItem, VersionOptions options)
        {
            if (contentItem.VersionRecord == null)
            {
                // produce root record to determine the model id
                contentItem.VersionRecord = new ContentItemVersionRecord
                {
                    ContentItemRecord = new ContentItemRecord
                    {
                        ContentType = AcquireContentTypeRecord(contentItem.ContentType)
                    },
                    Number = 1,
                    Latest = true,
                    Published = true
                };
            }

            // add to the collection manually for the created case
            contentItem.VersionRecord.ContentItemRecord.Versions.Add(contentItem.VersionRecord);

            // version may be specified
            if (options.VersionNumber != 0)
            {
                contentItem.VersionRecord.Number = options.VersionNumber;
            }

            // draft flag on create is required for explicitly-published content items
            if (options.IsDraft)
            {
                contentItem.VersionRecord.Published = false;
            }

            _contentItemRepository.Create(contentItem.Record);
            _contentItemVersionRepository.Create(contentItem.VersionRecord);

        }

        private ContentTypeRecord AcquireContentTypeRecord(string contentType)
        {
            var contentTypeRecord = _contentTypeRepository.Get(x => x.Name == contentType);
            if (contentTypeRecord == null)
            {
                //TEMP: this is not safe... ContentItem types could be created concurrently?
                contentTypeRecord = new ContentTypeRecord { Name = contentType };
                _contentTypeRepository.Create(contentTypeRecord);
            }
            return contentTypeRecord;
        }

        public void Publish(ContentItem contentItem)
        {
            if (contentItem.VersionRecord.Published)
            {
                return;
            }
            // create a context for the item and it's previous published record
            var previous = contentItem.Record.Versions.SingleOrDefault(x => x.Published);
          
            if (previous != null)
            {
                previous.Published = false;
            }
            contentItem.VersionRecord.Published = true;

        }

    }
}