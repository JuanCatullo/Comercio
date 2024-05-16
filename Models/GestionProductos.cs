using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Comercio.Models
{
    public class GestionProductos
    {

        public static IEnumerable<Productos> ObtenerProductos(int iCategoria, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerProductos", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;




                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);


                List<Models.Productos> ListaProductos = new List<Models.Productos>();

                foreach (DataRow row in dt.Rows)
                {
                    var Producto = new Productos();
                    Producto.Id = int.Parse(row["id"].ToString());
                    Producto.Nombre = row["nombre"].ToString();
                    Producto.Precio = (decimal)row["precio"];
                    Producto.FechaCarga = (DateTime)row["fecha_carga"];
                    Producto.CategoriaId = int.Parse(row["categoria_id"].ToString());



                    ListaProductos.Add(Producto);
                }


                sResult = "";
                return ListaProductos;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }

        public static string InsertarProducto(Models.Productos nuevoProducto, ref int iProductoID)
        {
            string sRet = "";

            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MySqlCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MySqlCommand = new SqlCommand("spInsertarProducto", MyConnection);
                MySqlCommand.CommandType = CommandType.StoredProcedure;

                // Agregar parámetros de entrada
                MySqlCommand.Parameters.AddWithValue("@nombre", nuevoProducto.Nombre);
                MySqlCommand.Parameters.AddWithValue("@precio", nuevoProducto.Precio);
                MySqlCommand.Parameters.AddWithValue("@FechaCarga", nuevoProducto.FechaCarga);
                MySqlCommand.Parameters.AddWithValue("@CategoriaId", nuevoProducto.CategoriaId);

                // Agregar parámetro de salida
                SqlParameter pariProductoID = new SqlParameter("@ProductoID", SqlDbType.Int);
                pariProductoID.Direction = ParameterDirection.Output;
                MySqlCommand.Parameters.Add(pariProductoID);

                MyConnection.Open();
                MySqlCommand.ExecuteNonQuery();

                // Obtener el valor del parámetro de salida
                iProductoID = Convert.ToInt32(pariProductoID.Value);

                MyConnection.Close();
                MyConnection.Dispose();

                sRet = "";

            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }

            return sRet;
        }

        public static string ModificarProducto(Models.Productos ProductoExistente)
        {
            string sRet = "";

            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MySqlCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MySqlCommand = new SqlCommand("spModificarProducto", MyConnection);
                MySqlCommand.CommandType = CommandType.StoredProcedure;



                MySqlCommand.Parameters.AddWithValue("@id", ProductoExistente.Id);
                MySqlCommand.Parameters.AddWithValue("@nombre", ProductoExistente.Nombre);
                MySqlCommand.Parameters.AddWithValue("@precio", ProductoExistente.Precio);
                MySqlCommand.Parameters.AddWithValue("@FechaCarga", ProductoExistente.FechaCarga);
                MySqlCommand.Parameters.AddWithValue("@CategoriaId", ProductoExistente.CategoriaId);




                MyConnection.Open();
                MySqlCommand.ExecuteNonQuery();

                MyConnection.Close();
                MyConnection.Dispose();


                sRet = "";

            }
            catch (Exception ex)
            {
                sRet = ex.Message;

            }



            return sRet;
        }

        public static string EliminarProducto(int id)
        {
            string sRet = "";
            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MySqlCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MySqlCommand = new SqlCommand("spEliminarProducto", MyConnection);
                MySqlCommand.CommandType = CommandType.StoredProcedure;


                MySqlCommand.Parameters.AddWithValue("@IdProducto", id);



                MyConnection.Open();
                MySqlCommand.ExecuteNonQuery();

                MyConnection.Close();
                MyConnection.Dispose();


                sRet = "";

            }
            catch (Exception ex)
            {
                sRet = ex.Message;

            }



            return sRet;
        }

    }
}
