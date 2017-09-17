call rd /s /q Packed
call cd Code\Core

call cd Iql
call dotnet pack -c Release -o ..\..\..\Packed
call cd ..

call cd Iql.Parsing
call dotnet pack -c Release -o ..\..\..\Packed
call cd ..

call cd Iql.Queryable
call dotnet pack -c Release -o ..\..\..\Packed
call cd ..

call cd Iql.DotNet
call dotnet pack -c Release -o ..\..\..\Packed
call cd ..

call cd Iql.JavaScript
call dotnet pack -c Release -o ..\..\..\Packed
call cd ..

call cd Iql.OData
call dotnet pack -c Release -o ..\..\..\Packed
call cd ..

call cd ..
call cd ..