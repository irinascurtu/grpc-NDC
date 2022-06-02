using Grpc.Core;
using Server;

namespace Server.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<Response> SayHello(Request request, ServerCallContext context)
        {
            var contextv= context.GetHttpContext();

             var user = context.GetHttpContext().User;
            // ... access data from ClaimsPrincipal ...


            //  var clientCertificate = httpContext.Connection.ClientCertificate;

            var userAgent = context.RequestHeaders.GetValue("user-agent");
             context.ResponseTrailers.Add(new Metadata.Entry("Trailing", "i'm in front row acting like a header!"));
         

            return Task.FromResult(new Response
            {
                Message = $"Hello back with the : { request.ContentValue } from {context.Host.ToString()}"
            });;
        }

        public override async Task ServerStream(Request request, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < 10000; i++)
            {
                var message = new Response
                {
                    Message = "Hello " + i
                };


                await responseStream.WriteAsync(message);
            }

            Metadata.Entry myHeader = new Metadata.Entry("my-fake-header", "grpc-header");

            context.ResponseTrailers.Add(myHeader);
        }

        public override async Task<Response> ClientStream(IAsyncStreamReader<Request> requestStream, ServerCallContext context)
        {
            var baseMessage = "I got ";
            Response reply = new Response() { Message = baseMessage };

            while (await requestStream.MoveNext())
            {

                var payload = requestStream.Current;
                Console.WriteLine($"I got a request with: {payload}");
                reply.Message = baseMessage + payload.ContentValue.ToString();
            }
            return reply;
        }

        public override async Task BiDirectional(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            var baseMessage = "";

            Response reply = new Response() { Message = baseMessage };
            while (await requestStream.MoveNext())
            {
                var payload = requestStream.Current;


                reply.Message = baseMessage + payload.ContentValue.ToString();
                await responseStream.WriteAsync(reply);

            }
        }

    }
}