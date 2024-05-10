using Comercio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IEnumerable<Models.Productos> Get()
        {
           

            string sRet = "";
            List<Models.Productos> ListaProductos = (List<Productos>)Productos.ObtenerProductos(-1, ref sRet);

            

            return ListaProductos;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IHttpActionResult InsertarProducto([FromBody] Models.Productos NuevoProducto)
        {
            //CODIGO PARA INSERTAR UN PRODUCTO

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
    }
}