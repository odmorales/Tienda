using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Entity;

namespace Datos
{
    public class ProductoRepository
    {
        private readonly SqlConnection _connection;
        private readonly List<Producto> _productos = new List<Producto>();
        public ProductoRepository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }
        public void Guardar(Producto producto)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into Producto (Id,Precio,Nombre,Descripcion,ImageUrl) 
                                        values (@Id,@Precio,@Nombre,@Descripcion,@ImageUrl)";
                command.Parameters.AddWithValue("@Id", producto.Id);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@ImageUrl", producto.ImageUrl);
                var filas = command.ExecuteNonQuery();
            }
        }
        public List<Producto> ConsultarTodos()
        {
            SqlDataReader dataReader;
            List<Producto> productos = new List<Producto>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Select * from Producto ";
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Producto Producto = DataReaderMapToPerson(dataReader);
                        productos.Add(Producto);
                    }
                }
            }
            return productos;
        }
        private Producto DataReaderMapToPerson(SqlDataReader dataReader)
        {
            if (!dataReader.HasRows) return null;
            Producto producto = new Producto();
            producto.Id = (string)dataReader["Id"];
            producto.Precio = (decimal)dataReader["Precio"];
            producto.Nombre = (string)dataReader["Nombre"];
            producto.Descripcion = (string)dataReader["Descripcion"];
            producto.ImageUrl = (string)dataReader["ImageUrl"];

            return producto;
        }
        public Producto BuscarPorIdentificacion(string id)
        {
            SqlDataReader dataReader;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from producto where Id=@Id";
                command.Parameters.AddWithValue("@Id", id);
                dataReader = command.ExecuteReader();
                dataReader.Read();
                return DataReaderMapToPerson(dataReader);
            }
        }
    }
}