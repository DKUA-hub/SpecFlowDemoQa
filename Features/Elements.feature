Feature: Elements

As a User I want to make sure
I'm able to use all web elements in Elements properly

Background:
	Given I am on the 'https://demoqa.com' page
	And I navigate to the "Elements" menu

@textBoxes
Scenario: Fill all Text Boxes and check the result
	When I select "Text Box" from the menu
	And I enter the following user information:
		| Field             | Value           |
		| Full Name         | D K             |
		| Email             | d.k@gmail.com   |
		| Current Address   | ul. Strzelcow 3 |
		| Permanent Address | ave. Soborniy 1 |
	And I click the "Submit" button
	Then I should see additional text box
	And I verify the submitted user information in the text box contains
		| Field             | Value           |
		| Full Name         | D K             |
		| Email             | d.k@gmail.com   |
		| Current Address   | ul. Strzelcow 3 |
		| Permanent Address | ave. Soborniy 1 |

@checkBox
Scenario: Verify selected Check Boxes reported in Output message
	When I select "Check Box" from the menu
	And I expand "Home" folder
	And I select "Desktop" folder
	And I expand "Documents" folder
	And I select "Angular" from "WorkSpace" folder
	And I select "Veu" from "WorkSpace" folder
	And I expand "Office" folder
	And I select each item from "Office" folder
	And I expand "Downloads" folder
	And I select "Downloads" folder by click on its name
	Then I see output message "You have selected : desktop notes commands angular veu office public private classified general downloads wordFile excelFile"