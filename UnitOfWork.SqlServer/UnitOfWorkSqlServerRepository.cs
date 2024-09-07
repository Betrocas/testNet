using Microsoft.Data.SqlClient;
using Repository.SqlServer;
using UnitOfWork.Interfaces;

namespace UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {
        public ClienteRepository ClienteRepository { get; }
        public CategoryRepository CategoryRepository { get; }
        public ProductRepository ProductRepository { get; }

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {
            ClienteRepository= new ClienteRepository(context, transaction);
            CategoryRepository= new CategoryRepository(context, transaction);
            ProductRepository= new ProductRepository(context, transaction);
        }

    }
}