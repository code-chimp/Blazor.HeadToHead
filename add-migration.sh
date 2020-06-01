#!/usr/bin/env bash

if [ -z "$1" ]; then
    echo "You need to supply a name for the migration"
    exit 1
fi

dotnet ef migrations add $1 -s ./H2H.Blazor.UI/H2H.Blazor.UI.csproj -p ./H2H.DataAccess/H2H.DataAccess.csproj
