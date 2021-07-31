dotnet tool update --global dotnet-ef --version 5.0.8
dotnet build
dotnet ef --startup-project ../ASPNETCoreIdentitySample/ database update --context MsSqlDbContext
pause