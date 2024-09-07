using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Product
{
    public class ProductIndexDTO
    {
        public List<ProductDTO> Products{ get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
