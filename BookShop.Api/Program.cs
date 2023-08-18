using BookShop.Api.Extensions;
using BookShop.Api.MiddleWares;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration().WriteTo
    .File(@"Loggers\BookShopErrors.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthenticationWithJwtBearer(builder.Configuration);
builder.Services.AddAuthorizationWithRoles();
builder.Services.AddSwaggerWithHeader();
builder.Services.AddBookshopDbContext(builder.Configuration);

builder.Services.AddBookshopServices();
builder.Services.AddBookShopRepositories();
builder.Services.AddValidators();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseErrorHandlerMiddleWare();

app.Run();
