Feature: AlertWindows

As an end user I want to be ale to review content 
in new browser tabs and windows

Background:
	Given I am on the DemoQA homepage
	And I navigate to the "Alerts, Frame & Windows" menu

@newtab
Scenario: Content can be displayed in new tab
	When I select "Browser Windows" from the menu 
	And  I click on a "New Tab" button 
	Then I navigate to new tab
	And I see "This is a sample page" message

@newwindow
Scenario: Content can be displayed in new window
	When I select "Browser Windows" from the menu 
	And  I click on a "New Window" button 
	Then I navigate to new window
	And I see "This is a sample page" message