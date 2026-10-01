using Microsoft.Data.SqlClient;

namespace AthleteDashboard.Api.Data;

public class SqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("AthleteDashboard")
            ?? throw new InvalidOperationException(
                "AthleteDashboard connection string is not configured.");
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}