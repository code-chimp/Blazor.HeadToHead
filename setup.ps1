#!/usr/bin/env pwsh

# stand up a SQL Server instance
docker run --name blazorh2h_dev `
    -p 1433:1433 `
    -e "ACCEPT_EULA=Y" `
    -e "SA_PASSWORD=P@ssw0rd" `
    -d mcr.microsoft.com/mssql/server:2019-latest

# give the server time to start
start-sleep -s 5

# run entity framework migrations
dotnet restore
dotnet ef database update -s .\H2H.Blazor.UI\H2H.Blazor.UI.csproj -p .\H2H.DataAccess\H2H.DataAccess.csproj
