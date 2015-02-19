using Microsoft.ServiceBus;
using ServiceBusRelay.Contract;
using System;
using System.ServiceModel;

namespace ServiceBusRelay.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(
                typeof(ConsoleService), 
                new Uri("sb://<NAMESPACE>.servicebus.windows.net")
            );

            var endpoint = host.AddServiceEndpoint(
                typeof(IConsoleService), 
                new NetTcpRelayBinding(), 
                "console"
            );

            endpoint.Behaviors.Add(new TransportClientEndpointBehavior
            {
                TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(
                    issuerName: "ISSUER-NAME", 
                    issuerSecret: "ISSUER-SECRET"
                )
            });

            host.Open();

            Console.WriteLine("The server is running");
            Console.ReadKey();

            host.Close();
        }
    }
}
