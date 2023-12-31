﻿using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using AirLineSendAgent.Data;
using AirlineSendAgent.Client;
using AirlineSendAgent.Dtos;
using AirLineSendAgent.Dtos;

namespace AirLineSendAgent.App
{
    public class AppHost : IAppHost
    {

        private readonly SendAgentDbContext _context;
        private readonly IWebhookClient _webhookClient;

        public AppHost(SendAgentDbContext context, IWebhookClient webhookClient)
        {
            _context = context;
            _webhookClient = webhookClient;
        }

        public void Run()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                var queueName = channel.QueueDeclare().QueueName;

                channel.QueueBind(queue: queueName,
                    exchange: "trigger",
                    routingKey: "");

                var consumer = new EventingBasicConsumer(channel);
                Console.WriteLine("Listening on the message bus...");

                consumer.Received += async (ModuleHandle, ea) =>
                {
                    Console.WriteLine("Event is triggered!");

                    var body = ea.Body;
                    var notificationMessage = Encoding.UTF8.GetString(body.ToArray());
                    var message = JsonSerializer.Deserialize<NotificationMessageDto>(notificationMessage);

                    var webhookToSend = new FlightDetailChangePayloadDto()
                    {
                        WebhookType = message.WebhookType,
                        WebhookURi = string.Empty,
                        Secret = string.Empty,
                        Publisher = string.Empty,
                        OldPrice = message.OldPrice,
                        NewPrice = message.NewPrice,
                        FlightCode = message.FlightCode
                    };

                    foreach (var whs in _context.WebHookSubscriptions.Where(subs => subs.WebHookType.Equals(message.WebhookType)))
                    {
                        webhookToSend.WebhookURi = whs.WebHookURi;
                        webhookToSend.Secret = whs.Secrect;
                        webhookToSend.Publisher = whs.WebHookPublisher;

                        await _webhookClient.SendWebhookNotification(webhookToSend);
                    }

                };

                channel.BasicConsume(queue: queueName,
                        autoAck: true,
                        consumer: consumer);

                Console.ReadLine();

            }
        }
    }
    
}
