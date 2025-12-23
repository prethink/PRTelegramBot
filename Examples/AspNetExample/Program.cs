using AspNetExample;
using AspNetExample.BackgroundTasks;
using AspNetExample.BotController;
using AspNetExample.MiddleWares;
using AspNetExample.Services;
using Microsoft.EntityFrameworkCore;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Builders;
using PRTelegramBot.Converters.Inline;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.EventsArgs;
using TestDI.Models;

/****************************************************************************************
 * ######################################################################################
 * 
 * Актуальная документация https://prethink.gitbook.io/prtelegrambot
 * 
 * ######################################################################################
 ****************************************************************************************/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Инициализация классов для работы ботов с DI
builder.Services.AddTransient<ServiceTransient>();
builder.Services.AddScoped<ServiceScoped>();
builder.Services.AddSingleton<ServiceSingleton>();
builder.Services.AddTransient<BotInlineHandlerWithDependency>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MyInMemoryDb"));
builder.Services.AddBotHandlers();
builder.Services.AddSingleton<IInlineMenuConverter>(new FileInlineConverter());
// Фоновые задачи через DI.
builder.Services.AddTransient<IPRBackgroundTask, ExampleDIAttributeBackgroundTasks>();
builder.Services.AddTransient<IPRBackgroundTask, ExampleWithMetadataBackgroundTasks>();
builder.Services.AddTransient<IPRBackgroundTask, ExampleWithoutMetadataBackgroundTasks>();

//Middleware через DI

builder.Services.AddScoped<MiddlewareBase, DIMiddleware>();
builder.Services.AddScoped<MiddlewareBase, UserMiddleware>();

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
var prBotInstance = new PRBotBuilder("token")
    .SetClearUpdatesOnStart(true)
    .SetServiceProvider(serviceProvaider)
    .AddInlineClassHandler(ClassTHeader.DefaultTestClass, typeof(BotInlineHandlerWithDependency))
    .AddBackgroundTaskMetadata(new ExampleBackgroundTasksMetadata())
    .Build();

prBotInstance.Events.OnCommonLog += PrBotInstance_OnLogCommon;
prBotInstance.Events.OnErrorLog += PrBotInstance_OnLogError;
await prBotInstance.StartAsync();


app.Run();
