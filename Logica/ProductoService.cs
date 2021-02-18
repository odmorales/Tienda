using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Pipes;
using System.Security.Permissions;
using Datos;
using Entity;

namespace Logica
{
    public class ProductoService
    {
        private readonly ConnectionManager _conexion;
        private readonly ProductoRepository _repositorio;

        public ProductoService(string connectionString)
        {
            _conexion = new ConnectionManager(connectionString);
            _repositorio = new ProductoRepository(_conexion);
        }
        public GuardarProductoResponse Guardar(Producto producto)
        {

            try
            {
                Producto personaBuscar = new Producto();
                personaBuscar = BuscarxIdentificacion(producto.Id);
                if (personaBuscar != null)
                {
                    return new GuardarProductoResponse("El producto ya esta registrada");
                }
                else
                {
                    _conexion.Open();
                    _repositorio.Guardar(producto);
                    _conexion.Close();
                    return new GuardarProductoResponse(producto);
                }
            }
            catch (Exception e)
            {
                return new GuardarProductoResponse($"Error de la Aplicacion: {e.Message}");
            }
            finally { _conexion.Close(); }
        }
        public List<Producto> ConsultarTodos()
        {
            _conexion.Open();
            List<Producto> productos = _repositorio.ConsultarTodos();
            _conexion.Close();
            return productos;
        }
        public Producto BuscarxIdentificacion (string id) {
            _conexion.Open ();
            Producto producto = _repositorio.BuscarPorIdentificacion (id);
            _conexion.Close ();
            return producto;
        }
    }
    public class GuardarProductoResponse
    {
        public GuardarProductoResponse(Producto producto)
        {
            Error = false;
            Producto = producto;
        }
        public GuardarProductoResponse(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Producto Producto { get; set; }
    }
}