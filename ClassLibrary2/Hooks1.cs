using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace ClassLibrary2
{
    [Binding]
    public sealed class Hooks1
    {

        private static ScenarioContext scenariocontext;
        private static FeatureContext featurecontext;
        private static ExtentReports extentreports;
        private static ExtentHtmlReporter extenthtmlreport;
        private static ExtentTest feature;
        private static ExtentTest scenario;
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            extenthtmlreport = new ExtentHtmlReporter(@"c:\Data\");
            extentreports = new ExtentReports();
            extentreports.AttachReporter(extenthtmlreport);
        }

        [BeforeFeature]
        public static void BeforeFeatureStart(FeatureContext featurecontext)
        {
            if (null != featurecontext)
            {
                feature = extentreports.CreateTest<Feature>(featurecontext.FeatureInfo.Title,
                      featurecontext.FeatureInfo.Description);
            }
        }

        [BeforeScenario]
        public static void BeforeScenarioStart(ScenarioContext Scenariocontext)
        {

            if (null != Scenariocontext)
            {

                scenariocontext = Scenariocontext;
                scenario = feature.CreateNode<Scenario>(Scenariocontext.ScenarioInfo.Title,
                          Scenariocontext.ScenarioInfo.Description);
            }

        }

        [AfterStep]
        public void Aftereachstep()
        {

            ScenarioBlock scenarioblock = scenariocontext.CurrentScenarioBlock;
            switch (scenarioblock)
            {
                case ScenarioBlock.Given:
                    scenario.CreateNode<Given>(scenariocontext.StepContext.StepInfo.Text);
                    break;
                case ScenarioBlock.When:
                    scenario.CreateNode<When>(scenariocontext.StepContext.StepInfo.Text);
                    break;
                case ScenarioBlock.Then:
                    scenario.CreateNode<Then>(scenariocontext.StepContext.StepInfo.Text);
                    break;
                default:
                    scenario.CreateNode<And>(scenariocontext.StepContext.StepInfo.Text); break;
            }

        }

        [AfterTestRun]
        public static void AfterTestrun()
        {

            extentreports.Flush();


        }







    }
}
