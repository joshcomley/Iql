call lprun "D:\Code\PowerZero\Code\Projects\DeveloperBox\LINQPad\Queries\Brandless Project Tools\Increment version.xml.linq" "%~dp0version.xml"
call del Packaged\* /Q
call dotnet pack Code/ --output "%~dp0Packaged" --include-symbols --include-source -c Release
call del Packaged\Iql.TestBed.*
call del Packaged\Entity*