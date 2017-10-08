dotnet pack Code/ --output "%~dp0Packaged" --include-symbols --include-source -c Release
del Packaged\Iql.TestBed.*