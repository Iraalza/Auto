using Grpc.Core;

namespace Auto.OwnerServer.Services
{
    public class OwnerService : Owner.OwnerBase
    {
        private readonly ILogger<OwnerService> _logger;

        public OwnerService(ILogger<OwnerService> logger)
        {
            this._logger = logger;
        }

        public override Task<OwnerByPhoneNumberResult> GetOwner(OwnerByPhoneNumberRequest request, ServerCallContext context)
        {
            string phonenumber = request.Phonenumber;
            string region;
            if (phonenumber[1] == '4' && phonenumber[2] == '9' && phonenumber[3] == '5')
            {
                region = "Moscow";
            }
            else
            {
                region = "Other";
            }

            return Task.FromResult(new OwnerByPhoneNumberResult() { Region = (string)region });

        }
    }
}
