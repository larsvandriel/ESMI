using AppointmentManagementSystem.API.RabbitMQ;
using AppointmentManagementSystem.DataAccessLayer;
using AppointmentManagementSystem.Logic;
using AppointmentManagementSystem.RabbitMqAccessLayer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddHostedService<MessageBusSubscriberSpecialistDeletedEvent>();
builder.Services.AddHostedService<MessageBusSubscriberPatientDeletedEvent>();

builder.Services.AddScoped<IAppointmentManager, AppointmentManager>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
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
