namespace InventarioWebApiReact.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public short Stock { get; set; }
        public byte Id_Categoria { get; set; }
        public Categoria? Categoria { get; set; }
    }
}