dotnet tool update --global dotnet-ef --version 7.0.3
dotnet tool restore
dotnet build
dotnet ef --startup-project ../ASPNETCoreIdentitySample/ database update --context MsSqlDbContext
pause