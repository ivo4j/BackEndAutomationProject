using System;
using AventStack.ExtentReports;
using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using Reqnroll;
using RestSharp;
using System.Collections.Generic;
using NUnit.Framework;
using Reqnroll.Assist;

namespace BackEndAutomation.Tests.BBDTests
{
    [Binding]
    public class SchoolAPIProjectTests_PositiveScenariosStepDefinitions
    {
        private RestCalls restCalls = new RestCalls();
        private ResponseDataExtractors extractResponseData = new ResponseDataExtractors();

        private readonly ScenarioContext _scenarioContext;
        private ExtentTest _test;
        private RestResponse userLoginResponse, createClassResponse;
        private List<double> numbers = new List<double>();
        private string response;

        public SchoolAPIProjectTests_PositiveScenariosStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _test = scenarioContext.Get<ExtentTest>("ExtentTest");
        }

        [Given("login data is being prepared")]
        [When("login data is being prepared")]
        public void GivenLoginDataIsPrepared()
        {
            //Console.WriteLine("Login data is prepared");
            _test.Log(Status.Info, "Login data is prepared");
        }

        [When("user data for logged in user is returned  # Token is received")]
        [Then("user data for logged in user is returned  # Token is received")]
        public void ThenUserDataForLoggedInUserIsReturnedTokenIsReceived()
        {
            if (userLoginResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                _test.Log(Status.Info, "User is logged in: " + userLoginResponse.Content);
                
            }
            else
            {
                _test.Log(Status.Fail, "User is not logged in: " + userLoginResponse.Content);
                Assert.Fail("User is not logged in: " + userLoginResponse.Content);
            }
        }

        [Given("execute login API call with {string} username and {string} password")]
        public void GivenExecuteLoginAPICallWithUsernameAndPassword(string username, string password)
        {
            userLoginResponse = restCalls.LoginCall("https://schoolprojectapi.onrender.com/", username, password);
            _test.Log(Status.Info, $@"Login call is executed with ""{username}"" username and ""{password}"" password");
            string tokenValue = extractResponseData.ExtractLoggedInUserToken(userLoginResponse.Content, "access_token");

            _scenarioContext.Add("UserToken", tokenValue);
        }

        [When("execute create class {string} API call with {string}")]
        public void WhenExecuteCreateClassAPICallWith(string className, string[] subjects)
        {

            // create class via API call
            string token = extractResponseData.ExtractLoggedInUserToken(userLoginResponse.Content);
           // string[] subjects = { "Math", "English", "Literature" };
            createClassResponse = restCalls.CreateClassCall("https://schoolprojectapi.onrender.com/", "Class1", subjects, (string)_scenarioContext["UserToken"]);

            // check that the response is 200 OK  -- in progress
            // need to add a https://docs.reqnroll.net/latest/automation/datatable-helpers.html  and read the rest from the Automation Features section 
        }

        [Then("class is created successfully")]
        public void ThenClassIsCreatedSuccessfully()
        {
            if (createClassResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                _test.Log(Status.Info, "The class is successfully created: " + createClassResponse.Content);
            }
            else
            {
                _test.Log(Status.Fail, "The class is not created: " + createClassResponse.Content);
                Assert.Fail("The class is not created: " + createClassResponse.Content);
            }
        }

        [When("add student {string} to class {string} API call with")]
        public void WhenAddStudentToClassAPICallWith(string studentName, string className) 
        {
            //  make the request first with Postman and then convert it to RestSharp
            throw new PendingStepException();
        }

        [Then("student is added successfully")]
        public void ThenStudentIsAddedSuccessfully()
        {
            throw new PendingStepException();
        }

        [When("add marks for student {string} in class {string} API call with {string}")]
        public void WhenAddMarksForStudentInClassAPICallWith(string p0, string p1, string p2)
        {
            throw new PendingStepException();
        }

        [Then("marks are added successfully")]
        public void ThenMarksAreAddedSuccessfully()
        {
            throw new PendingStepException();
        }

        [When("get marks for student {string} in class {string} API call with")]
        public void WhenGetMarksForStudentInClassAPICallWith(string p0, string p1)
        {
            throw new PendingStepException();
        }

        [Then("marks are received successfully")]
        public void ThenMarksAreReceivedSuccessfully()
        {
            throw new PendingStepException();
        }
    }
}
