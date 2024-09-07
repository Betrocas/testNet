using Microsoft.Data.SqlClient;

namespace Services
{
    public class DB
    {
        public static SqlConnection DBconnect()
        {
            /*SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
            conn.DataSource = "IBM-PF3Q9DZS";
            conn.InitialCatalog = "Prueba";
            conn.IntegratedSecurity= true;
            conn.Encrypt = false;*/
            try
            {
                var ctx = new SqlConnection("Server=IBM-PF3Q9DZS;Initial Catalog=Prueba;Integrated Security=true;Encrypt=false");
                Console.WriteLine("conectada");
                return ctx;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}