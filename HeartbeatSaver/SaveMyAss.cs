﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using fCraft;
namespace HeartbeatSender
{
    class HeartbeatSender
    {
        public static bool On = true;


        static void Main()
        {
            string port = Server.Port.ToString();
            string Salt = Heartbeat.Salt;
            string Name = ConfigKey.ServerName.GetString();

            // this is what we are sending
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("public=True&max=20&users=120&port=25565&version=7&salt={1}&name=Au70Galaxy", port, Salt, Name);
            string post_data = sb.ToString();
            Console.WriteLine("Sending " + sb.ToString() + "\n");


            // this is where we will send it
            string uri = "http://www.minecraft.net/heartbeat.jsp";

            // create a request
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create(uri); request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";

            // turn our request string into a byte stream
            byte[] postBytes = Encoding.ASCII.GetBytes(post_data);


            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // send it

            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            Console.WriteLine("Sent\n");
            On = false;

            Thread.Sleep(15000);
            Main();
        }
    }
   

}

