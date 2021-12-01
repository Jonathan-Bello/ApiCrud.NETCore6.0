using InventarioWebApiReact.Models;

namespace InventarioWebApiReact.Services
{
    // interface iOperacionesBD
    public interface iOperacionesBD
    {
        // List<Producto> GetProductos();
        Task<List<Producto>> GetProductos();
        Task AddProducto(Producto producto);
        Task UpdateProducto(Producto producto);
        Task DeleteProducto(Producto producto);
        Task<List<Producto>> GetProductosConCategoria();
        Task<Producto> GetProducto(int id);
        Task<List<Categoria>> GetCategorias();
        Task<Usuarios> Loguear(string usuario, string pwd);
    }
}