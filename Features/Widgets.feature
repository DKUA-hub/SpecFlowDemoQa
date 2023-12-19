Feature: Widgets

As an end user I want to make sure that AutoCompletion and Progress Bar 
features from Widgets section work properly

Background: 
	Given I am on the DemoQA homepage
	And I navigate to the "Widgets" menu
@tag1
Scenario: Autocompletion sugests items based on users input
	When I select "Auto Complete" from the menu
	And I enter "g" in the "Type multiple color names" field
	Then the dropdown list suggests 3 options
	And each list item contains "g"

Scenario: Colors can be added to and deleted from input field
	When I select "Auto Complete" from the menu
	And I enter colors in the "Type multiple color names" field
		| Color  |
		| Red    |
		| Yellow |
		| Green  |
		| Blue   |
		| Purple |
	And I delete colors from the input field
		| Color  |
		| Yellow |
		| Purple |
	Then Input field contains colors
		| Color  |
		| Red    |
		| Green  |
		| Blue   |

