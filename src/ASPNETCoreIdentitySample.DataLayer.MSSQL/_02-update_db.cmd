dotnet tool update --global dotnet-ef --version 9.0.3
dotnet tool restore
dotnet build
dotnet ef --verbose --startup-project ../ASPNETCoreIdentitySample/ database update --context MsSqlDbContext
pause