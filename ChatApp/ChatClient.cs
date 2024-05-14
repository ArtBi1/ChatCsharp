﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ChatNetwork;
using CommonChat.DTO;
using CommonChat.Model;

namespace ChatApp
{
    public class ChatClient
    {
        private readonly string _name;
        private readonly IMessageSource _messageSource;
        private readonly IPEndPoint _serverEndPoint;

        public ChatClient(string name, IMessageSource messageSource)
        {
            _name = name;
            _messageSource = messageSource;
            _serverEndPoint = messageSource.GetServerIPEndPoint();
        }

        public void SendMessage(ChatMessage chatMessage, IPEndPoint ip)
        {
            _messageSource.SendMessage(chatMessage, ip);
        }

        public void Register()
        {
            var registerChat = new ChatMessage() { Command = Command.Register, FromName = _name};
            _messageSource.SendMessage(registerChat, _serverEndPoint);
        }

        public void ProcessSendMessage()
        {
            

            while (true)
            {
                Console.WriteLine("Input receiver's name");
                var receiver = Console.ReadLine();
                Console.WriteLine("Input your message");
                var text = Console.ReadLine();
                var chatMessage = new ChatMessage()
                    { Command = Command.Message, FromName = _name, ToName = receiver, Text = text };

                SendMessage(chatMessage, _serverEndPoint);
            }
        }

        public void Listen()
        {

            var ip = _messageSource.CreateNewIPEndPoint();
            Register();
            

            while (true)
            {
                var data = _messageSource.Receive(ref ip);
                Console.WriteLine($"Сообщение получено от {data.FromName}: \n{data.Text}");
                Confirmation(data, ip);
            }
        }

        public void Confirmation(ChatMessage chatMessage, IPEndPoint ip )
        {
            var message = new ChatMessage() {Command = Command.Confirmation, Id = chatMessage.Id};
            SendMessage(message, ip);

        }

        public void Start()
        {
            new Thread(() => ProcessSendMessage()).Start();
            Listen();
        }
    }
}