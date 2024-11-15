using System.Data;
using System.Data.Common;
using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ASPNETCoreIdentitySample.Common.PersianToolkit;

public class PersianYeKeCommandInterceptor : DbCommandInterceptor
{
    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        if (command != null)
        {
            ApplyCorrectYeKe(command);
        }

        return result;
    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = new())
    {
        if (command != null)
        {
            ApplyCorrectYeKe(command);
        }

        return ValueTask.FromResult(result);
    }

    public override InterceptionResult<int> NonQueryExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<int> result)
    {
        if (command != null)
        {
            ApplyCorrectYeKe(command);
        }

        return result;
    }

    public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (command != null)
        {
            ApplyCorrectYeKe(command);
        }

        return ValueTask.FromResult(result);
    }

    public override InterceptionResult<object> ScalarExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<object> result)
    {
        if (command != null)
        {
            ApplyCorrectYeKe(command);
        }

        return result;
    }

    public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<object> result,
        CancellationToken cancellationToken = new())
    {
        if (command != null)
        {
            ApplyCorrectYeKe(command);
        }

        return ValueTask.FromResult(result);
    }

    [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities",
        Justification = "`ApplyCorrectYeKe()` method is safe.")]
    private static void ApplyCorrectYeKe(DbCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        command.CommandText = command.CommandText.ApplyCorrectYeKe();

        foreach (DbParameter parameter in command.Parameters)
        {
            switch (parameter.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    if (parameter.Value is not DBNull and string)
                    {
                        parameter.Value =
                            Convert.ToString(parameter.Value, CultureInfo.InvariantCulture).ApplyCorrectYeKe();
                    }

                    break;
            }
        }
    }
}