using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Comercio
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7A26F4A905EB1F7F8395E68B4D5C32B2"));
            var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); 
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear(); 

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>(); 
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap = new Dictionary<string, string>(); 
        }
    }
}
