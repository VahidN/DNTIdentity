rmdir /S /Q bin
rmdir /S /Q obj
dotnet tool update -g Microsoft.Web.LibraryManager.Cli
libman restore
dotnet restore
pause