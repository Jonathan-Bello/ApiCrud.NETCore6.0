using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventarioWebApiReact.Models;
using InventarioWebApiReact.Services;

namespace InventarioWebApiReact.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : Controller
    {
        private readonly iOperacionesBD db;
        public CategoriasController(iOperacionesBD bd)
        {
            this.db = bd;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            var categorias = await db.GetCategorias();
            return categorias;
        }
    }
}
