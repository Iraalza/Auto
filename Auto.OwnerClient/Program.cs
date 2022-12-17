using Grpc.Net.Client;
using Auto.OwnerServer;

using var channel = GrpcChannel.ForAddress("https://localhost:7213");
var grpcClient = new Owner.OwnerClient(channel);
Console.WriteLine("Ready! Press any key to send a gRPC request (or Ctrl-C to quit).");
while (true)
{
    Console.ReadKey(true);
    var request = new OwnerByPhoneNumberRequest
    {
        Phonenumber = "74953286628"
    };

    var reply = grpcClient.GetOwner(request);
    Console.WriteLine($"Price: {reply.Region}");
}