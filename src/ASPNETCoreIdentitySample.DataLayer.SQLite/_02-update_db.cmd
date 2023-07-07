dotnet tool update --global dotnet-ef --version 7.0.8
dotnet tool restore
dotnet build
dotnet ef --startup-project ../ASPNETCoreIdentitySample/ database update --context SQLiteDbContext
pause