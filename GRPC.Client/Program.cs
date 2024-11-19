using GRPC.Client.Components;
using GRPC.Client.Services;
using static GRPC.Protos.TrackingService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddGrpcClient<TrackingServiceClient>(o=>
{
    o.Address = new Uri("https://localhost:7052");
});

builder.Services.AddGrpcClient<CustomeTrackingServiceClient>(o=>
{
    o.Address = new Uri("https://localhost:7052");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
