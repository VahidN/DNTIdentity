dotnet tool update --global dotnet-ef --version 6.0.1
dotnet build
dotnet ef --startup-project ../ASPNETCoreIdentitySample/ database update --context MsSqlDbContext
pause