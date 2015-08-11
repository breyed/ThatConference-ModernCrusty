using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(CrustyBike.WebServer.Startup))]

namespace CrustyBike.WebServer
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Initializes Web API.
			using (HttpConfiguration config = new HttpConfiguration()) {
				// Web API routes.
				config.MapHttpAttributeRoutes();
				config.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { id = RouteParameter.Optional }
				);
				app.UseWebApi(config);
			}

            // Initializes SignalR.
            app.MapSignalR();
		}
	}
}