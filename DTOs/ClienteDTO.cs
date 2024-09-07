using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public int Edad { get; set; }
    }
}
