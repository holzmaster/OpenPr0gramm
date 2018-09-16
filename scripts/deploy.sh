#!/usr/bin/env bash

BASE_DIR=src/OpenPr0gramm

dotnet pack -c Release ${BASE_DIR}/OpenPr0gramm.csproj
dotnet nuget push -k $NUGET_KEY -s https://api.nuget.org/v3/index.json ${BASE_DIR}/bin/Release/*.nupkg
