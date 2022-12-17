using Grpc.Core;

namespace Auto.PricingServer.Services
{
    public class PricerService : Pricer.PricerBase
    {
        private readonly ILogger<PricerService> _logger;

        public PricerService(ILogger<PricerService> logger)
        {
            this._logger = logger;
        }

        public override Task<PriceReply> GetPrice(PriceRequest request, ServerCallContext context)
        {
            return Task.FromResult(new PriceReply() { CurrencyCode = "RUB", Price = 400 });
        }
    }
}
