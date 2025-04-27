using System;
using AventStack.ExtentReports;
using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using Reqnroll;
using RestSharp;
using System.Collections.Generic;
using NUnit.Framework;

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

        [Given("login data is prepared")]
        [When("login data is prepared")]
        public void GivenLoginDataIsPrepared()
        {
            //Console.WriteLine("Login data is prepared");
            _test.Log(Status.Info, "Login data is prepared");
        }

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
        }

        [When("execute create class {string} API call with {string}")]
        public void WhenExecuteCreateClassAPICallWith(string className, string math)
        {

            // create class via API call
            string token = extractResponseData.ExtractLoggedInUserToken(userLoginResponse.Content);
            string[] subjects = { ""};
            createClassResponse = restCalls.CreateClassCall("https://schoolprojectapi.onrender.com/", "Class1", math);

            // check that the response is 200 OK
        }

        [Then("class is created successfully")]
        public void ThenClassIsCreatedSuccessfully()
        {
            throw new PendingStepException();
        }

        [When("add student {string} to class {string} API call with")]
        public void WhenAddStudentToClassAPICallWith(string p0, string p1)
        {
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
