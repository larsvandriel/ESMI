﻿using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using SpecialistManagementSystem.Logic;
using System.Text;
using System.Text.Json;

namespace SpecialistManagementSystem.RabbitMQAccessLayer
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQHost"], Port = int.Parse(_configuration["RabbitMQPort"]) };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Direct);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Connected to MessageBus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
            }
        }

        public void SendSpecialistDeletedEvent(Specialist specialist)
        {
            var message = JsonSerializer.Serialize(specialist);
            if (_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
                SendMessage(message, "SpecialistDeletedEvent");
            }
        }

        private void SendMessage(string message, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                                routingKey: routingKey,
                                basicProperties: null,
                                body: body);
            Console.WriteLine($"--> We have sent {message} to routing {routingKey}");
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
    }
}