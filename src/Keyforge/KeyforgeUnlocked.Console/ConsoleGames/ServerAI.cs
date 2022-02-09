using System.Linq;
using System.Net.Http;
using Grpc.Net.Client;
using KeyforgeUnlocked.Server;
using UnlockedCore;
using AI = KeyforgeUnlocked.Server.AI;

namespace KeyforgeUnlockedConsole.ConsoleGames
{
    public class ServerAI : IGameAI
    {
        private AI.AIClient? _client;


        public int[] DetermineAction(ICoreState state)
        {
            // TODO IoC this
            using var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions{HttpClient = new HttpClient(new HttpClientHandler{ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator})});
            _client = new AI.AIClient(channel);
            
            var response = _client.Simulate(new SimulateRequest { StateJson = state.ToString() });
            return response.MoveOrder.Select(r => (int)r).ToArray();
        }
    }
}