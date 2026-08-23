﻿using AspNetWebHook;
using AspNetWebHook.Controllers;
using AspNetWebHook.Services;
using PRTelegramBot.Builders;
using PRTelegramBot.Core.Factories;

/****************************************************************************************
 * ######################################################################################
 * 
 * Up-to-date documentation: https://prethink.gitbook.io/prtelegrambot
 * 
 * ######################################################################################
 ****************************************************************************************/

var builder = WebApplication.CreateBuilder(args);

//Webhooks require controllers and newtonsoftJson!!!
builder.Services.AddControllers().AddNewtonsoftJson();

#region Bot startup service

builder.Services.AddHostedService<BotHostedService>();

#region Adding the bots

new PRBotBuilder("5623652365:Token")
    .UseFactory(new PRBotWebHookFactory())
    .SetUrlWebHook("https://domain.ru/botendpoint")
    .SetClearUpdatesOnStart(true)
    .Build();

new PRBotBuilder("555555:Token")
    .UseFactory(new PRBotWebHookFactory())
    .SetUrlWebHook("https://domain.ru/botendpoint")
    .SetClearUpdatesOnStart(true)
    .SetBotId(1)
    .Build();

// Instances of these bots can be found through the BotCollection class

#endregion

#endregion

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

#region creating the webhook route

app.MapBotWebhookRoute<BotController>("/botendpoint");
app.MapControllers();

#endregion

app.Run();