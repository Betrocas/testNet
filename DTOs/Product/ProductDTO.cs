using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int? CategoryId { get; set; }
        public CategoryDTO? Category { get; set; }
    }
}
