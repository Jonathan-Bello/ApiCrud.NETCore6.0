using InventarioWebApiReact.Models;
using InventarioWebApiReact.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventarioWebApiReact.Controllers
{
    // Definimos rutas para el controlador de productos
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        // Definimos un objeto de tipo OperacionesBD
        private readonly iOperacionesBD db;
        // Definimos una propiedad para usar el servicio de operaciones en la base de datos
        public ProductosController(iOperacionesBD db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            var productos = await db.GetProductos();
            if (productos.Count() == 0)
            {
                return NotFound();
            }
            return productos;
        }


        [HttpGet("ProductosCategorias")]
        // [Authorize]
        public async Task<ActionResult<List<Producto>>> GetProductosConCategoria()
        {
            var productos = await db.GetProductosConCategoria();
            if (productos.Count() == 0)
            {
                return NotFound();
            }
            return productos;
        }

        [HttpGet("{id}")]
        // [Authorize]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await db.GetProducto(id);
            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }


        [HttpPost]
        public async Task<ActionResult> AddProducto([FromBody] Producto producto)
        {
            await db.AddProducto(producto);
            return NoContent();
        }


        [HttpPut]
        public async Task<ActionResult> UpdateProducto([FromBody] Producto producto)
        {
            await db.UpdateProducto(producto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProducto([FromBody] Producto producto)
        {
            await db.DeleteProducto(producto);
            return NoContent();
        }
    }
}