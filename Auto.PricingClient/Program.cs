using Grpc.Net.Client;
using Auto.PricingServer;

using var channel = GrpcChannel.ForAddress("https://localhost:7157");
var grpcClient = new Pricer.PricerClient(channel);
Console.WriteLine("Ready! Press any key to send a gRPC request (or Ctrl-C to quit).");
while (true)
{
    Console.ReadKey(true);
    var request = new PriceRequest
    {
        Model = "volkswagen-beetle",
        Color = "Green",
        Year = 1985
    };

    var reply = grpcClient.GetPrice(request);
    Console.WriteLine($"Price: {reply.Price} {reply.CurrencyCode}");
}