using PRTelegramBot.Core;
using PRTelegramBot.Core.Factory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var serviceProvaider = app.Services.GetService<IServiceProvider>();
var prBotInstance = new PRBotBuilder("5555:Token")
    .UseFactory(new PRBotWebHookFactory())
    .SetClearUpdatesOnStart(true)
    .SetServiceProvider(serviceProvaider)
    .Build();

app.Run();
