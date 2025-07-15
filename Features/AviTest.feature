Feature: Avi Login Test
  As a user
  I want to login to the practice test site
  So that I can access the secure area

  Scenario: Successful login and logout with valid credentials
    Given the user is on the Avi login page
    When the user enters username 'student' and password 'Password123'
    And clicks the login button
    Then the user should be navigated to the secure area
    When the user clicks the logout button
    Then the user should be logged out and redirected to the login page