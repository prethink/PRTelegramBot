using AspNetExample.Services;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.EventsArgs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Инициализация классов для работы ботов с DI
builder.Services.AddTransient<ServiceTransient>();
builder.Services.AddScoped<ServiceScoped>();
builder.Services.AddSingleton<ServiceSingleton>();
builder.Services.AddBotHandlers();


async Task PrBotInstance_OnLogError(ErrorLogEventArgs e)
{
    Console.WriteLine(e.Exception.ToString());
}

async Task PrBotInstance_OnLogCommon(CommonLogEventArgs e)
{
    Console.WriteLine(e.Message);
}

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



//Создание и запуск бота
var serviceProvaider = app.Services.GetService<IServiceProvider>();
var prBotInstance = new PRBotBuilder("")
    .SetClearUpdatesOnStart(true)
    .SetServiceProvider(serviceProvaider)
    .Build();

prBotInstance.Events.OnCommonLog += PrBotInstance_OnLogCommon;
prBotInstance.Events.OnErrorLog += PrBotInstance_OnLogError;
await prBotInstance.Start();


app.Run();
