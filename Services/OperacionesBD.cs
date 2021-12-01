using System.Data.SqlClient;
using System.Linq;
using Dapper;
using InventarioWebApiReact.Models;
using Microsoft.Extensions.Configuration;

namespace InventarioWebApiReact.Services
{
    public class OperacionesDB : iOperacionesBD
    {
        private readonly IConfiguration _config;
        public OperacionesDB(IConfiguration config)
        {
            this._config = config;
        }

        public async Task<List<Producto>> GetProductos()
        {
            using (var db = new SqlConnection(_config.GetConnectionString("ConnectionStrings")))
            {
                var query = "SELECT Id, Nombre, Precio, Stock, Id_Categoria FROM Productos";
                var productos = (await db.QueryAsync<Producto>(query)).ToList();
                return productos;
            }
        }

        public async Task<List<Producto>> GetProductosConCategoria()
        {
            using (var db = new SqlConnection(_config.GetConnectionString("ConnectionStrings")))
            {
                var query = "SELECT p.Id, p.Nombre, p.Precio, p.Stock, p.Id_Categoria, c.Id, c.Nombre FROM Productos as p inner join Categorias as c on p.Id_Categoria = c.Id";
                var productos = (await db.QueryAsync<Producto, Categoria, Producto>(query, (p, c) =>
                {
                    p.Categoria = c;
                    return p;
                }, splitOn: "Id")).ToList();
                return productos;
            }
        }

        public async Task<Producto> GetProducto(int id)
        {
            using (var db = new SqlConnection(_config.GetConnectionString("ConnectionStrings")))
            {
                var query = "SELECT * FROM Productos where Id = @id";
                var producto = await db.QueryFirstOrDefaultAsync<Producto>(query, new { id });
                return producto;
            }
        }

        public async Task AddProducto(Producto producto)
        {
            using (var db = new SqlConnection(_config.GetConnectionString("ConnectionStrings")))
            {
                var query = "INSERT INTO Productos (Nombre, Precio, Stock, Id_Categoria) VALUES (@Nombre, @Precio, @Stock, @Id_Categoria)";
                await db.ExecuteAsync(query, producto);
            }
        }

        public async Task UpdateProducto(Producto producto)
        {
            using (var db = new SqlConnection(_config.GetConnectionString("ConnectionStrings")))
            {
                var query = "UPDATE Productos SET Nombre = @Nombre, Precio = @Precio, Stock = @Stock, Id_Categoria = @Id_Categoria WHERE Id = @Id";
                await db.ExecuteAsync(query, producto);
            }
        }

        public async Task DeleteProducto(Producto producto)
        {
            using (var db = new SqlConnection(_config.GetConnectionString("ConnectionStrings")))
            {
                var query = "DELETE FROM Productos WHERE Id = @Id";
                await db.ExecuteAsync(query, new { Id = producto.Id });
            }
        }

        public async Task<List<Categoria>> GetCategorias()
        {
            using (var db = new SqlConnection(_config.GetConnectionString("ConnectionStrings")))
            {
                var query = "SELECT * FROM Categorias";
                var categorias = (await db.QueryAsync<Categoria>(query)).ToList();
                return categorias;
            }
        }

        // Login
        public async Task<Usuarios> Loguear(string u, string p)
        {
            using (var db = new SqlConnection(_config.GetConnectionString("ConnectionStrings")))
            {
                var query = "SELECT * FROM Usuarios WHERE Usuario=@usuario and Pwd=@pwd";
                var user = await db.QueryFirstOrDefaultAsync<Usuarios>(query, new { usuario = u, pwd = p });
                return user;
            }
        }
    }
}