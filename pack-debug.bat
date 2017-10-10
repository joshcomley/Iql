call del Packaged\* /Q
call dotnet pack Code/ --output "%~dp0Packaged" --include-symbols -c Debug
call del Packaged\Iql.TestBed.*
call del Packaged\Entity*