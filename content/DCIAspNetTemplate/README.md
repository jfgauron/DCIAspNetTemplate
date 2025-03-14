# DCIAspNetTemplate

## How to run

```
dotnet build
dotnet run --project src/DCIAspNetTemplate/DCIAspNetTemplate.csproj
```

## Run unit tests / integration tests

```
dotnet test test/DCIAspNetTemplate.Tests/DCIAspNetTemplate.Tests.csproj
```

### Run functional tests

```
# start the application
dotnet run --project src/DCIAspNetTemplate/DCIAspNetTemplate.csproj

# in another terminal:
dotnet test test/DCIAspNetTemplate.FunctionalTests/DCIAspNetTemplate.FunctionalTests.csproj
```