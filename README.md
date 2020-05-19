# Blazor vs Razor: Head to Head

A simple project to highlight a use case of Server-Side Blazor vs Razor Pages hopefully highlighting client side 
performance differences.

## Datastore Setup

```shell
# generate a disposable SQL Server instance in Docker
docker run --name headtohead -p 1433:1433 -e "SA_PASSWORD=P@ssw0rd" -e "ACCEPT_EULA=Y" -d mcr.microsoft.com/mssql/server:2019-latest

# seed the database
dotnet ef database update -s ./H2H.Blazor.UI/H2H.Blazor.UI.csproj -p ./H2H.DataAccess/H2H.DataAccess.csproj

# NOTE: easiest way to add new migrations from the command line
dotnet ef migrations add <Migration Name> -s ./H2H.Blazor.UI/H2H.Blazor.UI.csproj -p ./H2H.DataAccess/H2H.DataAccess.csproj
```
