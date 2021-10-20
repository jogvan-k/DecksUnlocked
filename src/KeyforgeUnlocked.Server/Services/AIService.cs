using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace KeyforgeUnlocked.Server
{
    public class AIService : AI.AIBase
    {
        private readonly ILogger<AIService> _logger;

        public AIService(ILogger<AIService> logger)
        {
            _logger = logger;
        }

        public override Task<SimulateResponse> Simulate(SimulateRequest request, ServerCallContext context)
        {
            var response = new SimulateResponse
            {
                MoveOrder = { 0, 0, 0, 0 }
            };

            return Task.FromResult(response);
        }
    }
}