# Blazor vs Razor: Head to Head

Blazor: SPA performance without all of the front-end JavaScript https://blazor.net

A simple project to highlight a use case of Server-Side Blazor vs Razor Pages hopefully highlighting client side 
performance differences.

## Datastore Setup

```shell
# generate a disposable SQL Server instance in Docker
# and run migrations to initialize databse
./setup.sh

# or on Windows
.\setup.ps1

# delete temporary SQL Server instance
./teardown.sh

# or on Windows
.\teardown.ps1

# seed the database
dotnet ef database update -s ./H2H.Blazor.UI/H2H.Blazor.UI.csproj -p ./H2H.DataAccess/H2H.DataAccess.csproj

# NOTE: easiest way to add new migrations from the command line
./add-migration.sh <MigrationName>
```
