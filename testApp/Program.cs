
using Services;
using UnitOfWork.SqlServer;

var uow = new UnitOfWorkSqlServer();
ClienteService serv = new ClienteService(uow);
var resp = serv.Consultar();

Console.Read();


