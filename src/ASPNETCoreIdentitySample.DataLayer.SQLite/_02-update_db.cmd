dotnet tool update --global dotnet-ef --version 6.0.9
dotnet build
dotnet ef --startup-project ../ASPNETCoreIdentitySample/ database update --context SQLiteDbContext
pause