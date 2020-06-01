#!/usr/bin/env pwsh
param(
    [string]$migration = $(throw "-migration is required")
)

dotnet ef migrations add $migration -s .\H2H.Blazor.UI\H2H.Blazor.UI.csproj -p .\H2H.DataAccess\H2H.DataAccess.csproj
