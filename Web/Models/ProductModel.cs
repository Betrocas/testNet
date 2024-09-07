namespace Web.Models
{
    public class ProductModel
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
