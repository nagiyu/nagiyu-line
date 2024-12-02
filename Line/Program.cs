using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Common.Utilities;

using DynamoDBAccessor.Interfaces;
using DynamoDBAccessor.Services;

using LineBotProcessor.Interfaces;
using LineBotProcessor.Services;

using OpenAIConnect.Services;
using OpenAIConnect.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// サービス登録
builder.Services.AddTransient<IMessageProcessor, MessageProcessor>();
builder.Services.AddTransient<IApiHandler, ApiHandler>();
builder.Services.AddTransient<IOpenAIClient, OpenAIClient>();
builder.Services.AddTransient<IDynamoDbService, DynamoDbService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

AppSettings.Initialize(builder.Configuration);

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

app.Run();
