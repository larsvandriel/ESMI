using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using SpecialistManagementSystem.API.RabbitMQ;
using SpecialistManagementSystem.DataAccessLayer;
using SpecialistManagementSystem.Logic;
using SpecialistManagementSystem.RabbitMQAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.Configure<IISOptions>(options =>
{

});

ConfigurationManager config = builder.Configuration;
var connectionString = config["mssqlconnection:connectionString"];

Console.WriteLine(connectionString);

builder.Services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddHostedService<MessageBusSubscriberAppointmentCreatedEvent>();
builder.Services.AddHostedService<MessageBusSubscriberAppointmentCancelledEvent>();
builder.Services.AddHostedService<MessageBusSubscriberAppointmentFinishedEvent>();
builder.Services.AddHostedService<MessageBusSubscriberAppointmentEditedEvent>();

builder.Services.AddScoped<ISpecialistManager, SpecialistManager>();
builder.Services.AddScoped<ISpecialistRepository, SpecialistRepository>();
builder.Services.AddScoped<IMessageBusClient, MessageBusClient>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions()
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
