dotnet tool install --global dotnet-ef --version 3.1.8
dotnet tool update --global dotnet-ef --version 3.1.8
dotnet build
dotnet ef --startup-project ../ASPNETCoreIdentitySample/ database update --context SQLiteDbContext
pause