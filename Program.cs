using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using OneLife.Web;
using OneLife.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var bffBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5000";

builder.Services.AddHttpClient<FinanceWebService>(client =>
{
    client.BaseAddress = new Uri(bffBaseUrl);
});

builder.Services.AddMudServices();

await builder.Build().RunAsync();
