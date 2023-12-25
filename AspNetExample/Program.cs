using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Инициализация классов для работы ботов с DI
builder.Services.AddBotHandlers();


void PrBotInstance_OnLogError(Exception ex, long? id)
{
    Console.WriteLine(ex.ToString());
}

void PrBotInstance_OnLogCommon(string msg, Enum typeEvent, ConsoleColor color)
{
    Console.WriteLine(msg);
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
var prBotInstance = new PRBot(new TelegramConfig
{
    Token = "",
    ClearUpdatesOnStart = true,
    BotId = 0,
},
app.Services.GetService<IServiceProvider>()
);


prBotInstance.OnLogCommon += PrBotInstance_OnLogCommon;
prBotInstance.OnLogError += PrBotInstance_OnLogError;
await prBotInstance.Start();


app.Run();
