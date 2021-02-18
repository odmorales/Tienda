using System.Net.Http.Headers;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using tiendadotnet.Models;

namespace tiendadotnet.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _productoService;
        public IConfiguration Configuration { get; }
        public ProductoController (IConfiguration configuration) {
            Configuration = configuration;
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _productoService = new ProductoService (connectionString);
        }
        // GET: api/Producto
        [HttpGet]
        public IEnumerable<ProductoViewModel> Gets () {
            var productos = _productoService.ConsultarTodos ().Select (p => new ProductoViewModel (p));
            return productos;
        }
        // GET: api/Producto/5
        [HttpGet ("{identificacion}")]
        public ActionResult<ProductoViewModel> Get (string identificacion) {
            var producto = _productoService.BuscarxIdentificacion (identificacion);
            if (producto == null) return NotFound ();
            var productoViewModel = new ProductoViewModel (producto);
            return productoViewModel;
        }
        // POST: api/producto
        [HttpPost]
        public ActionResult<ProductoViewModel> Post (ProductoInputModel productoInput) {
            Producto producto = MapearProducto (productoInput);
            var response = _productoService.Guardar (producto);
            if (response.Error) {
                ModelState.AddModelError("Guardar producto",response.Mensaje);
                var problemDetails = new ValidationProblemDetails(ModelState){
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest (problemDetails);
            }
            return Ok (response.Producto);
        }
        private Producto MapearProducto (ProductoInputModel productoInput) {
            var producto = new Producto {
                Id = productoInput.Id,
                Precio = productoInput.Precio,
                Nombre = productoInput.Nombre,
                Descripcion = productoInput.Descripcion,
                ImageUrl = productoInput.ImageUrl

            };
            return producto;
        }
    }
}