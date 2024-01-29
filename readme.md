# InsuranceTest

Solution to satisfy the tech test requirements. Composed of 4 projects:

- `InsuranceTest.Api` - The web api, uses data annotations for validation, has 2 response models, but otherwise returns Dto since Models would've been too much duplication for little benefit in a project this size.
- `InsuranceTest.Service` - The service layer, simple manager setup with entity to Dto mappers.
- `InsuranceTest.Data` - The data layer, uses data annotations for validation, the hardcoded data can be found in each class. Entities are generated from EF scaffolding on empty DB.
- `InsuranceTest.Tests` - Unit test project, has tests for Controllers and Managers.

## Requirements

- Visual Studio 2022
- .net Core 8 SDK

## Running

`InsuranceTest.Api` as the start up and use IIS Express profile. Should open in the browser straight to swagger.

## Data

Here's the valid data values that have been hardcoded in and can be used in swagger.

### Claims UCR

- `Claim1`
- `Claim2`
- `Claim4`

### CompanyId

- `1`
- `2`

## EF Scaffold Script

The script that can be used to re-scaffold the DB. Just needs a connection string.

`Scaffold-DbContext '<connectionString>' Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Entities" -ContextDir "Contexts" -NoOnConfiguring -DataAnnotations -Force `