Feature: SchoolAPIProjectTests - Positive scenarios

Test API calls with RestSharp and BDD tests

# login as an admin user
@login 
Scenario: Login with API call with parametrized steps   
	Given login data is being prepared
	When execute login API call with "admin1" username and "admin123" password
	Then user data for logged in user is returned  # Token is received

	# login as a teacher user, 
Scenario Outline: Teacher creates a class with subjects 
	Given login data is being prepared
	And execute login API call with "teacher1" username and "teacher1" password
	When execute create class "Class1" API call with "<subject>"
	Then class is created successfully
	Examples: 
	| subject    |
	| Math       | 
	| English    | 
	| Literature | 

Scenario: Teacher adds a student to the class
	Given login data is being prepared
	And execute login API call with "teacher1" username and "teacher1" password
	When add student "Peter Ivanov" to class "Class1" API call with
	Then student is added successfully


Scenario Outline: Teacher adds marks for the student
	Given login data is being prepared
	And execute login API call with "teacher1" username and "teacher1" password
	When add marks for student "Peter Ivanov" in class "Class1" API call with "<mark>"
	Then marks are added successfully
Examples:
	| mark |
	| 5    |
	| 6    |
	| 6    |


Scenario: Parent sees the marks of their child
	Given login data is being prepared
	And execute login API call with "parent1" username and "parent1" password
	When get marks for student "Peter Ivanov" in class "Class1" API call with
	Then marks are received successfully




