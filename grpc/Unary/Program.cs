using Grpc.Core;

Console.WriteLine("Hello, World!");

//Channel channel = new Channel("127.0.0.1:5000", ChannelCredentials.Insecure);

//var client = new Greeter.GreeterClient(channel);
//var cts = new CancellationTokenSource();

//HelloRequest request = new HelloRequest() { Name = "NDC" };

//Console.WriteLine($"sending: {request.Name}");

//var reply = client.SayHello(request, options: new CallOptions() { });

//Console.WriteLine(reply.Message);