using Microsoft.Extensions.Configuration;
using Npgsql;

namespace FridgeApp.Infrastructure.Persistence.Contexts;

internal sealed class DapperContext
{
    private readonly string _connectionString;
    
    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionString");
    }
    public NpgsqlConnection CreateConnection()
        => new (_connectionString);
}
