﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.18003
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Orchard.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Content rights management")]
    public partial class ContentRightsManagementFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ContentRights.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Content rights management", "  In order to ensure security\r\n  As a root Orchard system operator\r\n  I want only" +
                    " the allowed users to edit the content", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Administrators can manage a Page")]
        public virtual void AdministratorsCanManageAPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Administrators can manage a Page", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
    testRunner.When("I have a user \"user1\" with roles \"Administrator\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
    testRunner.Then("\"user1\" should be able to \"publish\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 10
        testRunner.And("\"user1\" should be able to \"edit\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Users can\'t create a Page if they don\'t have the PublishContent permission")]
        public virtual void UsersCanTCreateAPageIfTheyDonTHaveThePublishContentPermission()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Users can\'t create a Page if they don\'t have the PublishContent permission", ((string[])(null)));
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
    testRunner.When("I have a role \"CustomRole\" with permissions \"EditContent, DeleteContent\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
        testRunner.And("I have a user \"user1\" with roles \"CustomRole\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
    testRunner.Then("\"user1\" should not be able to \"publish\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 17
        testRunner.And("\"user1\" should be able to \"edit\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
        testRunner.And("\"user1\" should be able to \"delete\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Users can create a Page of others if they have PublishContent permission")]
        public virtual void UsersCanCreateAPageOfOthersIfTheyHavePublishContentPermission()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Users can create a Page of others if they have PublishContent permission", ((string[])(null)));
#line 20
this.ScenarioSetup(scenarioInfo);
#line 21
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 22
    testRunner.When("I have a role \"CustomRole\" with permissions \"PublishContent\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
        testRunner.And("I have a user \"user1\" with roles \"CustomRole\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
        testRunner.And("I have a user \"user2\" with roles \"Administrator\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
    testRunner.Then("\"user1\" should be able to \"publish\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 26
        testRunner.And("\"user1\" should be able to \"edit\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
        testRunner.And("\"user1\" should not be able to \"delete\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Users can create a Page if they have PublishOwnContent for Page")]
        public virtual void UsersCanCreateAPageIfTheyHavePublishOwnContentForPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Users can create a Page if they have PublishOwnContent for Page", ((string[])(null)));
#line 29
this.ScenarioSetup(scenarioInfo);
#line 30
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 31
    testRunner.When("I have a role \"CustomRole\" with permissions \"Publish_Page\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
        testRunner.And("I have a user \"user1\" with roles \"CustomRole\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
    testRunner.Then("\"user1\" should be able to \"publish\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 34
        testRunner.And("\"user1\" should be able to \"edit\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
        testRunner.And("\"user1\" should not be able to \"delete\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Users can create and edit a Page even if they only have the PublishOwnContent per" +
            "mission")]
        public virtual void UsersCanCreateAndEditAPageEvenIfTheyOnlyHaveThePublishOwnContentPermission()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Users can create and edit a Page even if they only have the PublishOwnContent per" +
                    "mission", ((string[])(null)));
#line 37
this.ScenarioSetup(scenarioInfo);
#line 38
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 39
    testRunner.When("I have a role \"CustomRole\" with permissions \"PublishOwnContent\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 40
        testRunner.And("I have a user \"user1\" with roles \"CustomRole\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
    testRunner.Then("\"user1\" should be able to \"publish\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 42
        testRunner.And("\"user1\" should be able to \"edit\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
        testRunner.And("\"user1\" should not be able to \"delete\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Users can\'t edit a Page if they don\'t have the EditContent permission")]
        public virtual void UsersCanTEditAPageIfTheyDonTHaveTheEditContentPermission()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Users can\'t edit a Page if they don\'t have the EditContent permission", ((string[])(null)));
#line 45
this.ScenarioSetup(scenarioInfo);
#line 46
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 47
    testRunner.When("I have a role \"CustomRole\" with permissions \"DeleteContent\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 48
        testRunner.And("I have a user \"user1\" with roles \"CustomRole\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 49
    testRunner.Then("\"user1\" should not be able to \"publish\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 50
        testRunner.And("\"user1\" should not be able to \"edit\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
        testRunner.And("\"user1\" should be able to \"delete\" a \"Page\" owned by \"user1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Users can\'t create a Page for others if they only have PublishOwnContent")]
        public virtual void UsersCanTCreateAPageForOthersIfTheyOnlyHavePublishOwnContent()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Users can\'t create a Page for others if they only have PublishOwnContent", ((string[])(null)));
#line 53
this.ScenarioSetup(scenarioInfo);
#line 54
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 55
    testRunner.When("I have a role \"CustomRole\" with permissions \"PublishOwnContent\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 56
        testRunner.And("I have a user \"user1\" with roles \"CustomRole\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
        testRunner.And("I have a user \"user2\" with roles \"Administrator\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
    testRunner.Then("\"user1\" should not be able to \"publish\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 59
        testRunner.And("\"user1\" should not be able to \"edit\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
        testRunner.And("\"user1\" should not be able to \"delete\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Users can\'t create a Page for others if they only have Publish_Page")]
        public virtual void UsersCanTCreateAPageForOthersIfTheyOnlyHavePublish_Page()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Users can\'t create a Page for others if they only have Publish_Page", ((string[])(null)));
#line 62
this.ScenarioSetup(scenarioInfo);
#line 63
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 64
    testRunner.When("I have a role \"CustomRole\" with permissions \"Publish_Page\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 65
        testRunner.And("I have a user \"user1\" with roles \"CustomRole\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
        testRunner.And("I have a user \"user2\" with roles \"Administrator\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
    testRunner.Then("\"user1\" should be able to \"publish\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 68
        testRunner.And("\"user1\" should be able to \"edit\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
        testRunner.And("\"user1\" should not be able to \"delete\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Users can create a Page for others if they only have Publish_Page")]
        public virtual void UsersCanCreateAPageForOthersIfTheyOnlyHavePublish_Page()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Users can create a Page for others if they only have Publish_Page", ((string[])(null)));
#line 71
this.ScenarioSetup(scenarioInfo);
#line 72
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 73
    testRunner.When("I have a role \"CustomRole\" with permissions \"Publish_Page\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 74
        testRunner.And("I have a user \"user1\" with roles \"CustomRole\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 75
        testRunner.And("I have a user \"user2\" with roles \"Administrator\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 76
    testRunner.Then("\"user1\" should be able to \"publish\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 77
        testRunner.And("\"user1\" should be able to \"edit\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
        testRunner.And("\"user1\" should not be able to \"delete\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Users can delete a Page for others if they only have Delete_Page")]
        public virtual void UsersCanDeleteAPageForOthersIfTheyOnlyHaveDelete_Page()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Users can delete a Page for others if they only have Delete_Page", ((string[])(null)));
#line 80
this.ScenarioSetup(scenarioInfo);
#line 81
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 82
    testRunner.When("I have a role \"CustomRole\" with permissions \"Delete_Page\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 83
        testRunner.And("I have a user \"user1\" with roles \"CustomRole\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
        testRunner.And("I have a user \"user2\" with roles \"Administrator\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
    testRunner.Then("\"user1\" should not be able to \"publish\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 86
        testRunner.And("\"user1\" should not be able to \"edit\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
        testRunner.And("\"user1\" should be able to \"delete\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Users can\'t delete a Page for others if they only have DeleteOwn_Page")]
        public virtual void UsersCanTDeleteAPageForOthersIfTheyOnlyHaveDeleteOwn_Page()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Users can\'t delete a Page for others if they only have DeleteOwn_Page", ((string[])(null)));
#line 90
this.ScenarioSetup(scenarioInfo);
#line 91
    testRunner.Given("I have installed Orchard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 92
    testRunner.When("I have a role \"CustomRole\" with permissions \"DeleteOwn_Page\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 93
        testRunner.And("I have a user \"user1\" with roles \"CustomRole\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 94
        testRunner.And("I have a user \"user2\" with roles \"Administrator\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 95
    testRunner.Then("\"user1\" should not be able to \"publish\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 96
        testRunner.And("\"user1\" should not be able to \"edit\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 97
        testRunner.And("\"user1\" should not be able to \"delete\" a \"Page\" owned by \"user2\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
