@WebinarRegistration
@UI
Feature: Webinar registration
  As a webinar visitor
  I want to register for the AI software testing webinar
  So that I can verify the registration flow works correctly

  Background:
    Given I am on the AI software testing webinar registration page

  @Positive
  Scenario: Successful webinar registration
    When I submit the registration form with dynamically generated attendee data
    Then I should see a successful registration confirmation

  @Negative
  Scenario: Registration is rejected when required fields are invalid
    When I submit the registration form with empty name and invalid email
    Then I should see registration validation errors

  @InvalidEmail
  Scenario: Registration is rejected when email is invalid
    When I submit the registration form with valid name and invalid email
    Then I should see an email validation error

  @InvalidMobile
  Scenario: Registration is rejected when mobile number is invalid
    When I submit the registration form with valid name and invalid mobile number
    Then I should see a mobile validation error