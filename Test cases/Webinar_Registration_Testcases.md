# SPR TechForge Webinar Registration – Manual Test Scenarios

## Application
SPR TechForge – AI & the Future of Software Testing Free Live Webinar

## URL
https://sprtechforge.com/#/events/ai-the-future-of-software-testing-free-live-webinar?ref=singh

## Module
Webinar Registration

## Test Environment
- Browser: Chrome
- Platform: Windows
- Application Type: Web / SPA
- Registration Type: Webinar registration

---

# TC-001 – Verify Webinar Registration Page

### Objective
Verify that the webinar registration page loads successfully.

### Preconditions
- Internet connection is available.
- Webinar URL is accessible.

### Test Steps
1. Open the webinar registration URL.
2. Wait for the page to load.
3. Verify the webinar title.
4. Verify webinar date and time.
5. Verify the registration form is displayed.

### Expected Result
The webinar page loads successfully and displays:
- Webinar title
- Date and time
- Registration form
- Full Name field
- Email field
- Mobile field
- Submit button

### Priority
High

### Type
Functional / UI
# TC-002 – Verify Registration Form Fields

### Objective
Verify that all required registration fields are available.

### Test Steps
1. Open the webinar registration page.
2. Scroll to the registration form.
3. Verify Full Name field.
4. Verify Email field.
5. Verify Mobile field.
6. Verify Qualification/Degree dropdown.
7. Verify Passing Year field.
8. Verify Current Status dropdown.
9. Verify How did you hear about this event? dropdown.
10. Verify Biggest question about testing career dropdown.
11. Verify Terms & Conditions checkbox.
12. Verify Submit button.

### Expected Result
All expected registration controls are displayed and usable.

### Priority
High

### Type
Functional / UI
# TC-003 – Successful Registration With Valid Data

### Objective
Verify that a user can submit the registration form with valid information.

### Test Data
Name: John QA Tester
Email: john.qa.unique@example.com
Mobile: 9876543210
Qualification: B.Tech / B.E
Passing Year: 2024
Current Status: Working professional
Source: LinkedIn
Biggest Question: How do I move from manual to automation?

### Test Steps
1. Open the webinar registration page.
2. Enter a valid full name.
3. Enter a valid unique email address.
4. Enter a valid 10-digit mobile number.
5. Select a valid qualification.
6. Enter/select a valid passing year.
7. Select Current Status.
8. Select how the user heard about the event.
9. Select the biggest question.
10. Accept the Terms & Conditions checkbox.
11. Click Submit.

### Expected Result
The form should be accepted successfully and the application should display the appropriate registration confirmation/joining-link message.

### Priority
Critical

### Type
Functional / Positive

# TC-004 – Verify Required Name Validation

### Objective
Verify that registration cannot be submitted without a name.

### Test Steps
1. Open the registration page.
2. Leave Full Name empty.
3. Enter a valid email.
4. Enter a valid mobile number.
5. Fill the remaining required fields.
6. Accept the Terms & Conditions.
7. Click Submit.

### Expected Result
The registration should not be submitted.
A validation message should be displayed for the Full Name field.

### Priority
High

### Type
Validation / Negative
# TC-005 – Verify Invalid Email Validation

### Objective
Verify that an invalid email address is rejected.

### Test Data
Email: invalid-email

### Test Steps
1. Open the registration page.
2. Enter a valid name.
3. Enter `invalid-email` in the Email field.
4. Enter a valid mobile number.
5. Fill the remaining required fields.
6. Accept the Terms & Conditions.
7. Click Submit.

### Expected Result
The registration should not be submitted.
An appropriate email validation message should be displayed.

### Priority
High

### Type
Validation / Negative

# TC-006 – Verify Invalid Mobile Number Validation

### Objective
Verify that an invalid mobile number is rejected.

### Test Data
Mobile: 123

### Test Steps
1. Open the registration page.
2. Enter a valid name.
3. Enter a valid email.
4. Enter `123` as the mobile number.
5. Fill the remaining required fields.
6. Accept the Terms & Conditions.
7. Click Submit.

### Expected Result
The registration should not be submitted.
The application should display an appropriate mobile-number validation message.

### Priority
High

### Type
Validation / Negative

# TC-007 – Verify Terms & Conditions Validation

### Objective
Verify that registration cannot be submitted when the required consent checkbox is not selected.

### Test Steps
1. Open the registration page.
2. Enter valid registration information.
3. Do not select the Terms & Conditions checkbox.
4. Click Submit.

### Expected Result
Registration should not be completed.
The application should indicate that the required consent must be accepted.

### Priority
High

### Type
Validation / Negative


# TC-008 – Verify Country Code Selection

### Objective
Verify that the mobile country code can be changed.

### Test Steps
1. Open the registration page.
2. Click the country-code selector.
3. Verify the available country codes.
4. Select a country other than India.
5. Enter a valid mobile number according to the selected country.

### Expected Result
The selected country code should be displayed and the mobile field should accept the corresponding valid number.

### Priority
Medium

### Type
Functional / UI

# TC-009 – Verify Qualification Dropdown

### Objective
Verify that the Qualification/Degree dropdown works correctly.

### Test Steps
1. Open the registration page.
2. Click Qualification/Degree.
3. Verify the available options.
4. Select `B.Tech / B.E`.

### Expected Result
The selected qualification should be displayed in the dropdown.

### Priority
Medium

### Type
Functional / UI

# TC-010 – Verify Current Status Dropdown

### Objective
Verify that the Current Status dropdown works correctly.

### Test Steps
1. Click the Current Status dropdown.
2. Verify available options.
3. Select `Working professional`.

### Expected Result
`Working professional` should be selected successfully.

### Priority
Medium

### Type
Functional / UI

# TC-011 – Verify How Did You Hear About This Event Dropdown

### Objective
Verify that the event-source dropdown works correctly.

### Test Steps
1. Click the dropdown.
2. Verify the available options.
3. Select `LinkedIn`.

### Expected Result
`LinkedIn` should be selected successfully.

### Priority
Low

### Type
Functional / UI

# TC-012 – Verify Biggest Question Dropdown

### Objective
Verify that the user's biggest testing-career question can be selected.

### Test Steps
1. Click the Biggest Question dropdown.
2. Verify the available options.
3. Select `How do I move from manual to automation?`.

### Expected Result
The selected question should be displayed.

### Priority
Low

### Type
Functional / UI

# TC-013 – Verify Empty Form Submission

### Objective
Verify that the form does not submit when all required fields are empty.

### Test Steps
1. Open the registration page.
2. Leave all fields empty.
3. Click Submit.

### Expected Result
Registration should not be submitted.
Validation messages should be displayed for the required fields.

### Priority
High

### Type
Negative / Validation

# TC-014 – Verify Duplicate Email Handling

### Objective
Verify the application's behavior when an already registered email is submitted.

### Test Data
Use an email address that has already been registered.

### Test Steps
1. Open the registration page.
2. Enter valid registration details.
3. Enter an already registered email address.
4. Complete the required fields.
5. Accept the Terms & Conditions.
6. Click Submit.

### Expected Result
The application should handle the duplicate registration according to the application's defined business rule and display the appropriate message.

### Priority
High

### Type
Functional / Negative

# TC-015 – Verify Form Data Retention After Validation Error

### Objective
Verify that entered data is retained appropriately when validation fails.

### Test Steps
1. Enter valid name.
2. Enter invalid email.
3. Enter valid mobile number.
4. Fill the other fields.
5. Click Submit.

### Expected Result
The invalid field should display a validation error.
Previously entered valid information should remain populated unless the application intentionally clears it.

### Priority
Medium

### Type
Usability / Validation

# TC-016 – Verify Submit Button Behavior

### Objective
Verify that the Submit button behaves correctly during registration.

### Test Steps
1. Fill the form with valid information.
2. Click Submit.
3. Observe the Submit button while registration is processing.
4. Verify the resulting page/message.

### Expected Result
The application should prevent unintended multiple submissions while processing and should provide an appropriate success or validation response.

### Priority
Medium

### Type
Functional / Usability

# TC-017 – Verify Registration On Page Refresh

### Objective
Verify application behavior when the registration page is refreshed.

### Test Steps
1. Open the registration page.
2. Enter some registration information.
3. Refresh the browser.
4. Observe the page.

### Expected Result
The page should load correctly without application errors.
The behavior of entered data should follow the application's expected design.

### Priority
Medium

### Type
UI / Compatibility

# TC-018 – Verify Registration Using Different Browser

### Objective
Verify registration functionality across supported browsers.

### Test Browsers
- Chrome
- Edge
- Firefox

### Test Steps
1. Open the registration page in the selected browser.
2. Fill the registration form with valid data.
3. Submit the form.
4. Verify the registration response.

### Expected Result
The registration flow should work consistently across supported browsers.

### Priority
Medium

### Type
Cross-browser

# TC-019 – Verify Mobile/Responsive Registration Form

### Objective
Verify that the registration page is usable on smaller screen sizes.

### Test Steps
1. Open the registration page.
2. Resize the browser to a mobile-sized viewport.
3. Verify the registration form.
4. Scroll through the form.
5. Enter valid data.
6. Submit the form.

### Expected Result
All important controls should remain accessible and usable without layout-breaking issues.

### Priority
Medium

### Type
Responsive / UI

# TC-020 – Verify Registration Page Links and Navigation

### Objective
Verify that links and navigation elements on the webinar page lead to the expected destinations.

### Test Steps
1. Open the webinar page.
2. Click available navigation links.
3. Verify each destination.
4. Return to the webinar registration page.
5. Verify the registration form remains accessible.

### Expected Result
Each clickable navigation element should redirect to its intended destination without broken links or unexpected pages.

### Priority
Medium

### Type
Navigation / UI



| TC     | Manual Scenario               | Automation Status                     | Next Action                        |
| ------ | ----------------------------- | ------------------------------------- | ---------------------------------- |
| TC-001 | Registration page loads       | ✅ Automated                           | Keep                               |
| TC-002 | Registration form fields      | ✅ Partially automated                 | Enhance                            |
| TC-003 | Successful registration       | ✅ Automated                           | Keep                               |
| TC-004 | Empty name validation         | ✅ Automated                           | Keep                               |
| TC-005 | Invalid email validation      | ⚠️ Partially automated                | Add dedicated test                 |
| TC-006 | Invalid mobile validation     | ⚠️ Partially automated                | Add dedicated test                 |
| TC-007 | Terms & Conditions validation | ⚠️ Manual only                        | Automate                           |
| TC-008 | Country code selection        | ❌ Manual only                         | Automate                           |
| TC-009 | Qualification dropdown        | ⚠️ Automated as part of positive flow | Add dedicated test                 |
| TC-010 | Current status dropdown       | ⚠️ Automated as part of positive flow | Add dedicated test                 |
| TC-011 | Event source dropdown         | ⚠️ Automated as part of positive flow | Add dedicated test                 |
| TC-012 | Biggest question dropdown     | ⚠️ Automated as part of positive flow | Add dedicated test                 |
| TC-013 | Empty form submission         | ❌ Manual only                         | Automate                           |
| TC-014 | Duplicate email handling      | ❌ Manual only                         | Automate after confirming behavior |
| TC-015 | Form data retention           | ❌ Manual only                         | Automate                           |
| TC-016 | Submit button behavior        | ⚠️ Partially automated                | Enhance                            |
| TC-017 | Page refresh                  | ❌ Manual only                         | Automate                           |
| TC-018 | Cross-browser                 | ❌ Manual only                         | Add browser configuration          |
| TC-019 | Responsive UI                 | ❌ Manual only                         | Add viewport tests                 |
| TC-020 | Navigation links              | ❌ Manual only                         | Automate                           |
