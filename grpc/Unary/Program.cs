using Grpc.Core;
using Server;

Console.WriteLine("Hello, World!");

Channel channel = new Channel("127.0.0.1:5214", ChannelCredentials.Insecure);

var client = new Greeter.GreeterClient(channel);
var cts = new CancellationTokenSource();

Request request = new Request() { ContentValue = "NDC" };

Console.WriteLine($"sending: {request.ContentValue}");

var reply = client.SayHello(request, options: new CallOptions() { });

Console.WriteLine(reply.Message);