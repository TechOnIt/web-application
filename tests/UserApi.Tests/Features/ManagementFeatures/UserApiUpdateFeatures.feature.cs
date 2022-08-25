﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace UserApi.Tests.Features.ManagementFeatures
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class UserApiUpdateFeaturesFeature : object, Xunit.IClassFixture<UserApiUpdateFeaturesFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "UserApiUpdateFeatures.feature"
#line hidden
        
        public UserApiUpdateFeaturesFeature(UserApiUpdateFeaturesFeature.FixtureData fixtureData, UserApi_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/ManagementFeatures", "UserApiUpdateFeatures", "In Order to Update users\r\nAs an api consumer\r\nI want to Update user info by sendi" +
                    "ng request with details and Id", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="updating a user with valid data")]
        [Xunit.TraitAttribute("FeatureTitle", "UserApiUpdateFeatures")]
        [Xunit.TraitAttribute("Description", "updating a user with valid data")]
        public void UpdatingAUserWithValidData()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("updating a user with valid data", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 6
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
 testRunner.Given("that a user exists in the system", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 8
 testRunner.When("i request to update the user by id and details Name: \'testName\' Surname: \'testSur" +
                        "name\' Email: \'testEmail@gamil.com\' ConcurrencyStamp: \'CreateInstance<Concurrency" +
                        ">\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 9
 testRunner.Then("the user should be updated", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 10
 testRunner.And("the response status code is \'200 ok\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="updating a non-existing user")]
        [Xunit.TraitAttribute("FeatureTitle", "UserApiUpdateFeatures")]
        [Xunit.TraitAttribute("Description", "updating a non-existing user")]
        public void UpdatingANon_ExistingUser()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("updating a non-existing user", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 12
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 13
 testRunner.Given("that a user dose not exists in the system", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 14
 testRunner.When("i request to update the user by id and details Name: \'testName\' Surname: \'testSur" +
                        "name\' Email: \'testEmail@gamil.com\' ConcurrencyStamp: \'CreateInstance<Concurrency" +
                        ">\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 15
 testRunner.Then("the response status code is \'404 Not Found\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="updating an user without id")]
        [Xunit.TraitAttribute("FeatureTitle", "UserApiUpdateFeatures")]
        [Xunit.TraitAttribute("Description", "updating an user without id")]
        public void UpdatingAnUserWithoutId()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("updating an user without id", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 17
 this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 18
 testRunner.Given("that a user dose not exists in the system", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 19
 testRunner.When("i request to update the user without id and details Name: \'testName\' Surname: \'te" +
                        "stSurname\' Email: \'testEmail@gamil.com\' ConcurrencyStamp: \'CreateInstance<Concur" +
                        "rency>\' and Id:", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 20
 testRunner.Then("the response status code is \'404 Not Found\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="updating a user with invalid data")]
        [Xunit.TraitAttribute("FeatureTitle", "UserApiUpdateFeatures")]
        [Xunit.TraitAttribute("Description", "updating a user with invalid data")]
        [Xunit.InlineDataAttribute("testName", "testSurname", "testEmail@gamil.com", "CreateInstance<Concurrency>", new string[0])]
        [Xunit.InlineDataAttribute("testName", "testSurname", "", "CreateInstance<Concurrency>", new string[0])]
        [Xunit.InlineDataAttribute("testName", "testSurname", "testEmail@gamil.com", "", new string[0])]
        [Xunit.InlineDataAttribute("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "testSurname", "testEmail@gamil.com", "CreateInstance<Concurrency>", new string[0])]
        [Xunit.InlineDataAttribute("testName", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "testEmail@gamil.com", "CreateInstance<Concurrency>", new string[0])]
        [Xunit.InlineDataAttribute("testName", "", "testEmail@gamil.com", "CreateInstance<Concurrency>", new string[0])]
        [Xunit.InlineDataAttribute("testName", "testSurname", "testemail", "CreateInstance<Concurrency>", new string[0])]
        [Xunit.InlineDataAttribute("", "", "", "", new string[0])]
        public void UpdatingAUserWithInvalidData(string name, string surname, string email, string concurrencystamp, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("name", name);
            argumentsOfScenario.Add("surname", surname);
            argumentsOfScenario.Add("email", email);
            argumentsOfScenario.Add("concurrencystamp", concurrencystamp);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("updating a user with invalid data", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 23
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 24
 testRunner.Given("that a user exists or dose not exists in the system", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 25
 testRunner.When(string.Format("i request to update the user by id and details Name \'{0}\' Surname \'{1}\' Email \'{2" +
                            "}\' ConcurrencyStamp \'{3}\'", name, surname, email, concurrencystamp), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 26
 testRunner.Then("The response status code is \'400 Bad Request\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                UserApiUpdateFeaturesFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                UserApiUpdateFeaturesFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
