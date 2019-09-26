SET DNTPath=%~dp0
IF "%DNTPath:~-1%"=="\" SET DNTPath=%DNTPath:~0,-1%
dotnet new -u %DNTPath%
dotnet new -i %DNTPath%
dotnet new --list
rem dotnet new dntidentity -n MyNewProj
pause