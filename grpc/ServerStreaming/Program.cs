using Grpc.Core;
using Server;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1:5214", ChannelCredentials.Insecure);

            var client = new Server.Greeter.GreeterClient(channel);

          //  var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            ///Server Stream
            var cts = new CancellationTokenSource();

             using var streamingCall = client.ServerStream(new Request(), cancellationToken: cts.Token);
            
            
            //using var streamingCall = client.ServerStream(new Server.HelloRequest(),
            //                                            deadline: DateTime.UtcNow.AddMilliseconds(1),
            //                                            cancellationToken: cts.Token);

            try
            {
                await foreach (Response response in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
                {

                    Console.WriteLine($"{response.Message}");
                }

                var trailers = streamingCall.GetTrailers();
                var myValue = trailers.GetValue("my-fake-header");
                Console.WriteLine($"found some trailer trailer values:{myValue}");


            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
            {
                Console.WriteLine("Greeting timeout.");
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelled.");
            }
        }

    }
}
