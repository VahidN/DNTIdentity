dotnet tool update --global dotnet-ef --version 9.0.7
dotnet tool restore
dotnet build
dotnet ef --startup-project ../ASPNETCoreIdentitySample/ database update --context SQLiteDbContext
pause