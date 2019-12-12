#!/bin/bash
#Add the dotnet path to the path
export PATH="$HOME/.dotnet":$PATH

if [ -d "./artifacts" ]
then
    rm -Rf "./artifacts"; 
fi

dotnet restore

echo "build: Version

dotnet build -c Release -v q /nologo

echo "Runing functional tests"
dotnet test ./test/FunctionalTests/FunctionalTests.csproj
