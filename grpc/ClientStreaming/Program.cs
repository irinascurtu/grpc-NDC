using Grpc.Core;
using Server;

Channel channel = new Channel("127.0.0.1:5214", ChannelCredentials.Insecure);

var client = new Greeter.GreeterClient(channel);

try
{
    using var call = client.ClientStream();

    for (var i = 0; i < 100000; i++)
    {

        await call.RequestStream.WriteAsync(new Request { ContentValue = i.ToString() });
    }

    await call.RequestStream.CompleteAsync();
    Response response = await call;

    Console.WriteLine($"Received: {response.Message}");
}
catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
{
    Console.WriteLine("Stream cancelled.");
}
