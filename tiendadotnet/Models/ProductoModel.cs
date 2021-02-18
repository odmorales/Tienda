using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Entity;
using  System.ComponentModel.DataAnnotations;

namespace tiendadotnet.Models
{
    public class ProductoInputModel
    {
        public string Id { get; set; }
        public decimal Precio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImageUrl { get; set; }
        
        
    }
    public class ProductoViewModel : ProductoInputModel {
        public ProductoViewModel () {

        }
        public ProductoViewModel (Producto producto) {
            Id = producto.Id;
            Precio = producto.Precio;
            Nombre = producto.Nombre;
            Descripcion = producto.Descripcion;
            ImageUrl = producto.ImageUrl;
        }
    }
}