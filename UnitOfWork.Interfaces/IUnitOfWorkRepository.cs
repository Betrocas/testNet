using Repository.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        ClienteRepository ClienteRepository { get; }
        CategoryRepository CategoryRepository { get; }
        ProductRepository ProductRepository { get; }
    }
}
