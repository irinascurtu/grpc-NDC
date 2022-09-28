using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Server;
using System.Net.NetworkInformation;
using Unary;

#region interceptor
//using var channel = GrpcChannel.ForAddress("https://localhost:5000");
//var invoker = channel.Intercept(new ErrorHandlerInterceptor());
//var client = new Greeter.GreeterClient(invoker);
#endregion

Channel channel = new Channel("localhost:5000", ChannelCredentials.Insecure);
var client = new Greeter.GreeterClient(channel);
var cts = new CancellationTokenSource();

Request request = new Request() { ContentValue = "NDC!" };

Console.WriteLine($"sending: {request.ContentValue}");

var response = client.SayHello(request, options: new CallOptions() { });

//var metadata = new Metadata();
//metadata.Add(new Metadata.Entry("first-key", "first-key-value"));
//metadata.Add(new Metadata.Entry("secondkey", "second-key-value"));

//var response = await client.SayHelloAsync(
//        request,
//        headers: metadata,
//        deadline: DateTime.UtcNow.AddMilliseconds(1),
//        cancellationToken: cts.Token);


Console.WriteLine(response.Message);