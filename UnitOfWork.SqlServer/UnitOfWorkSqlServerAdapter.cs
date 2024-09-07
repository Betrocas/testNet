using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using UnitOfWork.Interfaces;

namespace UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerAdapter : IUnitOfWorkAdapter
    {
        private SqlConnection _context { get; set; }
        private SqlTransaction _transaction { get; set; }
        public IUnitOfWorkRepository Repositories { get; set; }

        public UnitOfWorkSqlServerAdapter()
        {

            _context= new SqlConnection("Server=IBM-PF3Q9DZS;Initial Catalog=Prueba;Integrated Security=true;Encrypt=false");
            _context.Open();
            _transaction = _context.BeginTransaction();
            Repositories = new UnitOfWorkSqlServerRepository(_context,_transaction);
        }


        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            if (_context != null)
            {
                _context.Close();
                _context.Dispose();
            }

            Repositories = null;
        }

        public void saveChanges()
        {
            _transaction.Commit();
        }
    }
}