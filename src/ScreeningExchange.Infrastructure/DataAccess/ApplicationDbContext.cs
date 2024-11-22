using System.Reflection;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ScreeningExchange.Infrastructure.DataAccess;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("screeningexchange");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    internal async Task<object?> ExecuteRawSql(string sql, object parameters)
    {
        var connection = Database.GetDbConnection() as SqlConnection;
        return await connection!.ExecuteScalarAsync( sql, param: parameters, commandType: System.Data.CommandType.Text);
    }

    internal async Task<T?> ExecuteRawSql<T>(string sql, object parameters)
    {
        var connection = Database.GetDbConnection() as SqlConnection;
        return await connection!.ExecuteScalarAsync<T>( sql, param: parameters, commandType: System.Data.CommandType.Text);
    }
}