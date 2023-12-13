Feature: Homepage
As a not logged user,
I can visit DemoQA web site
And verify the existence of required links

Background: 
    Given I am on the DemoQA homepage

@homepage
Scenario: Navigate to Elements
    When I click on the "Elements" link
    Then I am navigated to the "Elements" page

Scenario: Verify Links Existence
    Then I should see the following links:
        | Link                    |
        | Forms                   |
        | Alerts, Frame & Windows |
        | Widgets                 |
        | Interactions            |

