using CommonKit.Services;
using DynamoDBAccessor.Interfaces;
using DynamoDBAccessor.Services;
using LineBridge.Common.Interfaces.Message;
using LineBridge.Core.Services.Message;
using LineBridge.Interfaces.Webhook;
using LineBridge.Services.Webhook;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAIConnect.Common.Interfaces;
using OpenAIConnect.Services;
using SettingsManager.Services;
using SettingsRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("SettingsDBConnection")));

// サービス登録
builder.Services.AddScoped<AppSettingsService>();
builder.Services.AddTransient<IGyaruWebhook, GyaruWebhook>();
builder.Services.AddTransient<INagiyuWebhook, NagiyuWebhook>();
builder.Services.AddTransient<IReplyMessage, ReplyMessage>();
builder.Services.AddTransient<IOpenAIClient, OpenAIClient>();
builder.Services.AddTransient<IDynamoDbService, DynamoDbService>();
builder.Services.AddTransient<LogService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
