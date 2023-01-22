using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json.Linq;

public class Program
{
    const string SIGNALR_HUB_URL = "http://localhost:7027/hub";
    private static HubConnection hub;

    static async Task Main(string[] args)
    {
        hub = new HubConnectionBuilder().WithUrl(SIGNALR_HUB_URL).Build();
        await hub.StartAsync();
        Console.WriteLine("Hub started!");
        Console.WriteLine("Press any key...");
        var i = 0;
        while (true)
        {
            var input = Console.ReadLine();
            var message = $"Message #{i++} from Auto.SignalRClient {input}";
            await hub.SendAsync("NotifyWebUsers", "Auto.SignalRClient", message);
            Console.WriteLine($"Sent: {message}");
        }
    }
}