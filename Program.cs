using carrinho_api.AplicationExtensions;
using carrinho_api.Caching;
using carrinho_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json.Serialization;
using System.Web.Http.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();

builder.Services.AddDbContext<DataContext>(config =>
{
    config.UseSqlite("Data Source=Data\\CartWeb.db");
});

var redisConfig = builder.Configuration.GetSection("Redis").Get<RedisConfig>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConfig.Configuration;
    options.InstanceName = redisConfig.InstanceName;
});

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseCors(builder => builder
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin()
           );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Transfer-Encoding", "chunked");
    await next();
});
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();



app.Run();
