using System.Data;

namespace Backend.DataAccess.Repositories.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
