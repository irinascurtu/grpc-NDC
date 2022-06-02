using Grpc.Core;
using Grpc.Net.Client;
using Server;

Console.WriteLine("Hello, World!");

//Channel channel = new Channel("localhost:5000", ChannelCredentials.Insecure);

var channel = GrpcChannel.ForAddress("http://localhost:5061",
    new GrpcChannelOptions
    {
        Credentials = ChannelCredentials.Insecure,

    });


var client = new Greeter.GreeterClient(channel);
var cts = new CancellationTokenSource();

Request request = new Request() { ContentValue = "NDC" };

Console.WriteLine($"sending: {request.ContentValue}");

var response = client.SayHello(request, options: new CallOptions() { });

//var response = await client.SayHelloAsync(
//        request,
//       // headers: new Metadata().Add(new Entry("my-fake-header", "grpc-header")), 
//        deadline: DateTime.UtcNow.AddSeconds(5),
//        cancellationToken: cts.Token);


Console.WriteLine(response.Message);