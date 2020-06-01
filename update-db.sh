#!/usr/bin/env bash

dotnet ef database update -s ./H2H.Blazor.UI/H2H.Blazor.UI.csproj -p ./H2H.DataAccess/H2H.DataAccess.csproj
