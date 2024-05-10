using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Comercio.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCarga { get; set; }
        public int CategoriaId { get; set; }

        public static IEnumerable<Productos> ObtenerProductos(int iCategoria, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerProductos", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@categoria_id", iCategoria);


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


                MySqlCommand.Parameters.AddWithValue("@nombre", nuevoProducto.Nombre);
                MySqlCommand.Parameters.AddWithValue("@precio", nuevoProducto.Precio);
                MySqlCommand.Parameters.AddWithValue("@fecha_carga", nuevoProducto.FechaCarga);               
                MySqlCommand.Parameters.AddWithValue("@categoria_id", nuevoProducto.CategoriaId);


                // Agrego los Parámetros al SPROC (OUT)

                SqlParameter pariProductoID = new SqlParameter("@ProductoID", SqlDbType.Int);
                pariProductoID.Direction = ParameterDirection.Output;

                MySqlCommand.Parameters.Add(pariProductoID);



                MyConnection.Open();
                MySqlCommand.ExecuteNonQuery();

                //OBTENGO LOS VALORES DE LOS PARAMETROS DE SALIDA
                iProductoID = int.Parse(pariProductoID.Value.ToString());

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
                MySqlCommand.Parameters.AddWithValue("@fecha_carga", ProductoExistente.FechaCarga);
                MySqlCommand.Parameters.AddWithValue("@categoria_id", ProductoExistente.CategoriaId);
                



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


                MySqlCommand.Parameters.AddWithValue("@id", id);



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