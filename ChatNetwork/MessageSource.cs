﻿using CommonChat.DTO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatNetwork
{
    public class MessageSource : IMessageSource
    {
        private UdpClient _udpClient;
        private IPEndPoint _udpServerEndPoint;

        public MessageSource(int port, string adress, int portServer = 12345)
        {
            _udpClient = new UdpClient(port);
            _udpServerEndPoint = new IPEndPoint(IPAddress.Parse(adress), portServer);
        }

        public MessageSource(int portServer = 12345)
        {
            _udpClient = new UdpClient(portServer);
        }

        public IPEndPoint CreateNewIPEndPoint()
        {
            return new IPEndPoint(IPAddress.Any, 0);
        }

        public IPEndPoint GetServerIPEndPoint()
        {
            return new IPEndPoint(_udpServerEndPoint.Address, _udpServerEndPoint.Port);
        }

        public ChatMessage Receive(ref IPEndPoint ipEndPoint)
        {
            byte[] data = _udpClient.Receive(ref ipEndPoint);
            string jsonMessage = Encoding.UTF8.GetString(data);
            return ChatMessage.FromJson(jsonMessage);
        }


        public void SendMessage(ChatMessage chatMessage, IPEndPoint ipEndPoint)
        {
            byte[] data = Encoding.UTF8.GetBytes(chatMessage.ToJson());
            _udpClient.Send(data, data.Length, ipEndPoint);
        }
    }
}