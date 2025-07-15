Feature: Invalid Login Test
  As a user
  I want to see appropriate error messages when I enter invalid credentials
  So that I know why my login attempt failed

  Scenario: Login with invalid username
    Given the user is on the invalid login page
    When the user enters invalid username 'invaliduser' and password 'Password123'
    And clicks the invalid login button
    Then the user should see an error message for invalid username

  Scenario: Login with invalid password
    Given the user is on the invalid login page
    When the user enters invalid username 'student' and password 'invalidpassword'
    And clicks the invalid login button
    Then the user should see an error message for invalid password

  Scenario: Login with both invalid username and password
    Given the user is on the invalid login page
    When the user enters invalid username 'invaliduser' and password 'invalidpassword'
    And clicks the invalid login button
    Then the user should see an error message for invalid credentials
