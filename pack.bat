call del Packaged\* /Q
call dotnet pack Code/ --output "%~dp0Packaged" --include-symbols --include-source -c Release
call del Packaged\Iql.TestBed.*
call del Packaged\Entity*