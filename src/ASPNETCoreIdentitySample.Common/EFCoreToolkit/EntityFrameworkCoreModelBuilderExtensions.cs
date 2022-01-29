using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASPNETCoreIdentitySample.Common.EFCoreToolkit;

public static class EntityFrameworkCoreModelBuilderExtensions
{
    public static void SetDecimalPrecision(this ModelBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal)
                                 || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18, 6)");
        }
    }

    public static void SetCaseInsensitiveSearchesForSQLite(this ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        modelBuilder.UseCollation("NOCASE");

        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(string)))
        {
            property.SetCollation("NOCASE");
        }
    }

    public static void AddDateTimeOffsetConverter(this ModelBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        // SQLite does not support DateTimeOffset
        foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(DateTimeOffset)))
        {
            property.SetValueConverter(
                new ValueConverter<DateTimeOffset, DateTime>(
                    dateTimeOffset => dateTimeOffset.UtcDateTime,
                    dateTime => new DateTimeOffset(dateTime)
                ));
        }

        foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(DateTimeOffset?)))
        {
            property.SetValueConverter(
                new ValueConverter<DateTimeOffset?, DateTime>(
                    dateTimeOffset => dateTimeOffset.Value.UtcDateTime,
                    dateTime => new DateTimeOffset(dateTime)
                ));
        }
    }

    public static void AddDateTimeUtcKindConverter(this ModelBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        // If you store a DateTime object to the DB with a DateTimeKind of either `Utc` or `Local`,
        // when you read that record back from the DB you'll get a DateTime object whose kind is `Unspecified`.
        // Here is a fix for it!
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => !v.HasValue ? v : ToUniversalTime(v),
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

        foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties()))
        {
            if (property.ClrType == typeof(DateTime))
            {
                property.SetValueConverter(dateTimeConverter);
            }

            if (property.ClrType == typeof(DateTime?))
            {
                property.SetValueConverter(nullableDateTimeConverter);
            }
        }
    }

    private static DateTime? ToUniversalTime(DateTime? dateTime)
    {
        if (!dateTime.HasValue)
        {
            return null;
        }

        return dateTime.Value.Kind == DateTimeKind.Utc ? dateTime : dateTime.Value.ToUniversalTime();
    }
}