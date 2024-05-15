using Comercio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

namespace Comercio.Controllers
{
    public class ProductosController : ApiController
    {

        // GET api/<controller>
        [HttpGet]
        [Route("api/ObtenerProductos")]
        public IEnumerable<Models.Productos> Get()
        {
           

            string sRet = "";
            List<Models.Productos> ListaProductos = (List<Productos>)Productos.ObtenerProductos(-1, ref sRet);

           

            return ListaProductos;
        }

   

        [HttpPost]
        [Route("api/InsertarProducto")]
        public IHttpActionResult InsertarProducto([FromBody] Models.Productos NuevoProducto)
        {
           

            string sRet = "";
            int iProductoID = 0;

            sRet = Models.Productos.InsertarProducto(NuevoProducto, ref iProductoID);


            if (sRet == "")
            {
                NuevoProducto.Id = iProductoID;
            }
            else
            {
                return BadRequest("Error al insertar el producto: " + sRet);
            }


            return Ok(NuevoProducto);
          
        }

        [HttpPut]
        [Route("api/ModificarProducto")]
        public IHttpActionResult ModificarProducto([FromBody] Models.Productos ProductoExistente)
        {
            
            string sRet = "";

            sRet = Models.Productos.ModificarProducto(ProductoExistente);


            
            if (sRet == "")
            {
               
            }
            else
            {
                return BadRequest("Error al modificar el producto: " + sRet);
            }

            



            return Ok(ProductoExistente);


        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/Productos/{id}")]
        public IHttpActionResult EliminarProducto(int id)
        {
            string sRet = "";

            sRet = Models.Productos.EliminarProducto(id);
            if (sRet == "")
            {
                
            }
            else
            {
                return BadRequest("Error al eliminar el producto: " + sRet);
            }

            return Ok("Se borro");
        }

        [HttpPost]
        [Route("api/Productos/FiltrarPorMonto")]
        public IHttpActionResult FiltrarPorMonto(FiltradoProductos request)
        {

            if (request.Monto < 1 || request.Monto > 1000000)
            {
                return BadRequest("El monto debe estar comprendido entre 1 y 1,000,000.");
            }

            try
            {
               
                string sRet = "";
                List<Productos> todosLosProductos = (List<Productos>)Productos.ObtenerProductos(-1, ref sRet);

                
                Dictionary<int, Productos> productosFiltrados = new Dictionary<int, Productos>();

                
                foreach (var producto in todosLosProductos)
                {
                    if (!productosFiltrados.ContainsKey(producto.CategoriaId))
                    {
                        productosFiltrados[producto.CategoriaId] = producto;
                    }
                    else if (producto.Precio > productosFiltrados[producto.CategoriaId].Precio)
                    {
                        productosFiltrados[producto.CategoriaId] = producto;
                    }
                }

                
                List<Productos> productosSeleccionados = new List<Productos>();
                decimal sumaPrecios = 0;
                foreach (var producto in productosFiltrados.Values)
                {
                    if (sumaPrecios + producto.Precio <= request.Monto)
                    {
                        productosSeleccionados.Add(producto);
                        sumaPrecios += producto.Precio;
                    }
                }

               
                if (productosSeleccionados.Count < 2)
                {
                    return BadRequest("No hay suficientes productos disponibles para ofrecer al cliente");
                }

                return Ok(productosSeleccionados);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
    
