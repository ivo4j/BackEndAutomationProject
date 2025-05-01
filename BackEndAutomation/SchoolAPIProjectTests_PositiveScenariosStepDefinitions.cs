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
        private RestResponse userLoginResponse, createClassResponse, addStudentResponse, addMarksResponse, getMarksResponse;
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
        public void WhenExecuteCreateClassAPICallWith(string className, string subject)
        {
            createClassResponse = restCalls.CreateClassCall("https://schoolprojectapi.onrender.com/", className, subject, (string)_scenarioContext["UserToken"]);
            _test.Log(Status.Info, $@"Class is created with ""{className}"" name and ""{subject}"" subjects");
                   
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
            addStudentResponse = restCalls.AddStudentCall("https://schoolprojectapi.onrender.com", studentName, className, (string)_scenarioContext["UserToken"]);
            string studentID = extractResponseData.ExtractStudentId(userLoginResponse.Content);
            _scenarioContext.Add("Student_ID", studentID);
            _test.Log(Status.Info, $@"Student is added with ""{studentName}"" name to ""{className}"" class");
        }

        [Then("student is added successfully")]
        public void ThenStudentIsAddedSuccessfully()
        {
            if (addStudentResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                _test.Log(Status.Info, "The student is successfully added to the class: " + addStudentResponse.Content);
            }
            else
            {
                _test.Log(Status.Fail, "The student is not added to the class: " + addStudentResponse.Content);
                Assert.Fail("The student is not added to the class: " + addStudentResponse.Content);
            }
        }

        
        [When("add marks for student {string} for subject {string} API call with {int}")]
        public void WhenAddMarksForStudentInClassAPICallWith(string studentName, string subject, int mark)
        {
            studentName = (string)_scenarioContext["Student_ID"];
            addMarksResponse = restCalls.AddMarksCall("https://schoolprojectapi.onrender.com", studentName, subject, mark, (string)_scenarioContext["UserToken"]);
            _test.Log(Status.Info, $@"Marks are added for ""{studentName}"" for ""{subject}"" subject");
        }

        [Then("marks are added successfully")]
        public void ThenMarksAreAddedSuccessfully()
        {
            if (addMarksResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                _test.Log(Status.Info, "The class is successfully created: " + addMarksResponse.Content);
            }
            else
            {
                _test.Log(Status.Fail, "The class is not created: " + addMarksResponse.Content);
                Assert.Fail("The class is not created: " + addMarksResponse.Content);
            }
        }

        [When("get marks for student {string} API call")]
        public void WhenGetMarksForStudentInClassAPICallWith(string studentName)
        {
            studentName = (string)_scenarioContext["Student_ID"];
            getMarksResponse = restCalls.GetMarksCall("https://schoolprojectapi.onrender.com", studentName, (string)_scenarioContext["UserToken"]);
            _test.Log(Status.Info, $@"Get marks for ""{studentName}""");
        }

        [Then("marks are received successfully")]
        public void ThenMarksAreReceivedSuccessfully()
        {
            if (getMarksResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                _test.Log(Status.Info, "The marks successfully recevied: " + getMarksResponse.Content);
            }
            else
            {
                _test.Log(Status.Fail, "The marks are not received: " + getMarksResponse.Content);
                Assert.Fail("The marks are not received: " + getMarksResponse.Content);
            }
        }
    }
}
