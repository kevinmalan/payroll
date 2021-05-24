# Payroll Calculator
## A Web based Tax Calculator built using TDD

### Payroll.MVC
Contains the bulk of the resources, including the API controllers, Markup files, and Services.

### Payroll.Data
A SSDT Project for publishing scripts to SQL Server

### Payroll.Tests
A list of unit tests built in a TDD fashion, where a test is built ahead of its solution. This project is split between service and controller tests.

Some architecture and design considerations:
- All test calculations were conducted manually on paper before being added as test cases to the various calculator unit tests, to ensure validity.
- All Decimals are stored with a precision of 4 decimal places.
- A localDb mdf file targeting SQL Server 2016 (or later) is included with the project, located at : Payroll.MVC/App_Data/TaxCalcDb_Primary.mdf
- A global exception filter attribute 'ApiExceptionFilterAttribute' catches all exceptions and returns a generic response object
- The generic response object 'ApiResponse<T>' ensures the client can always expect a consistent response object
- Entity Framework Core was used, as it provided extra benefits inside the Unit Testing project, where a InMemory provider was used.
- VueJs 2, Axios & Bootstrap 4 are the libraries used on the front-end.
- The bulk of the JavaScript used can be found at: Payroll.MVC/wwwroot/js/site.js
- The bulk of the HTML used can be found at: Payroll.MVC/Views/Home/Index.cshtml
