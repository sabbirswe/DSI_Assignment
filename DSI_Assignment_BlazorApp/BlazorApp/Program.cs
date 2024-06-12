using BlazorApp;
using BlazorApp.IService;
using BlazorApp.Service;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredToast();



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7202/api/") });
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<ISupplierService, SupplierService>();
builder.Services.AddTransient<IOrderService, OrderService>();


await builder.Build().RunAsync();



