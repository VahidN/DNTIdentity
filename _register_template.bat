SET DNTPath=%~dp0
IF "%DNTPath:~-1%"=="\" SET DNTPath=%DNTPath:~0,-1%
dotnet new uninstall %DNTPath%
dotnet new install %DNTPath%
dotnet new list
rem dotnet new dntidentity -n MyNewProj
pause