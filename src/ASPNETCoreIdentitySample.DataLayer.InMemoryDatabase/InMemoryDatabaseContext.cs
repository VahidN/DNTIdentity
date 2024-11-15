using ASPNETCoreIdentitySample.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreIdentitySample.DataLayer.InMemoryDatabase;

public class InMemoryDatabaseContext(DbContextOptions options) : ApplicationDbContext(options);