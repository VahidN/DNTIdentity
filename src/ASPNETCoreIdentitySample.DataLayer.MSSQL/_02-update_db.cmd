dotnet tool update --global dotnet-ef --version 7.0.0
dotnet build
dotnet ef --startup-project ../ASPNETCoreIdentitySample/ database update --context MsSqlDbContext
pause