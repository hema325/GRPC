using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GRPC.Client.WASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using static GRPC.Protos.TrackingService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7052") });

builder.Services.AddSingleton(sp =>
{
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    var channel = GrpcChannel.ForAddress("https://localhost:7052", new GrpcChannelOptions { HttpClient = httpClient });
    return new TrackingServiceClient(channel);
});

await builder.Build().RunAsync();
