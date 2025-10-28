using Ecommerce.Store.Web.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStoreServices();
var app = builder.Build();

app.UseStoreServices();
//app.MapGet("/", () => "Hello World!");

app.Run();
