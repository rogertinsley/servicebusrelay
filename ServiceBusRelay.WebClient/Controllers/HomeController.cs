using Microsoft.ServiceBus;
using ServiceBusRelay.Contract;
using System.ServiceModel;
using System.Web.Mvc;

namespace ServiceBusRelay.WebClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Write(string text)
        {
            var factory = new ChannelFactory<IConsoleService>(
                new NetTcpRelayBinding(),
                new EndpointAddress("sb://rogertinsley.servicebus.windows.net/console")
            );

            factory.Endpoint.Behaviors.Add(new TransportClientEndpointBehavior
            {
                TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(
                    issuerName: "ISSUER-NAME",
                    issuerSecret: "ISSUER-SECRET"
                )
            });

            var proxy = factory.CreateChannel();
            proxy.Write(text);

            (proxy as IClientChannel).Close();

            return Redirect(Request.ApplicationPath);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
