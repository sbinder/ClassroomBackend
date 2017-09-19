using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using System.Web.Http.Cors;

[assembly: OwinStartup("StartClassroom", typeof(ClassroomBackend.Startup))]
namespace ClassroomBackend
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            //app.MapSignalR();

            var config = new HttpConfiguration();

            //I use attribute-based routing for ApiControllers
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.Map("/signalr", map =>
            {
                var hubConfiguration = new HubConfiguration
                {

                };

                map.RunSignalR(hubConfiguration);
            });

            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);

            //config.EnableCors();
            config.EnsureInitialized(); //Nice to check for issues before first request

            app.UseWebApi(config);

        }
    }
}