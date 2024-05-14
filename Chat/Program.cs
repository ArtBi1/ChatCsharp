﻿using ChatApp;
using ChatNetwork;


if (args.Length == 0)
{

    var server = new ChatServer(new MessageSource());
    server.Work();

}
else if (args.Length == 2)
{
    var client1 = new ChatClient(args[0], new MessageSource(int.Parse(args[1]), "127.0.0.1"));
    client1.Start();
}
else
{
    Console.WriteLine("Error. Input name and port.");
}