using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataContext;

public class DapperDbContext(IConfiguration config)
{

/// <summary>
/// Retrieves the database connection associated with the current context.
/// </summary>
/// <returns>An instance of <see cref="IDbConnection"/> representing the database connection.</returns>
    public IDbConnection GetConnection() => new SqliteConnection(config.GetConnectionString("DefaultConnection"));
}