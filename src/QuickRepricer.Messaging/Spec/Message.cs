﻿using QuickRepricer.Messaging.Extensions;
using System;
using System.IO;

namespace QuickRepricer.Messaging.Spec
{
    public class Message
    {
        public Type BodyType
        {
            get { return Body.GetType(); }
        }

        private object _body;

        public object Body
        {
            get { return _body; }
            set
            {
                _body = value;
                MessageType = _body.GetMessageType();
            }
        }

        public string ResponseAddress { get; set; }

        public string MessageType { get; set; }

        public TBody BodyAs<TBody>()
        {
            return (TBody)Body;
        }

        public static Message FromJson(Stream jsonStream)
        {
            var message = jsonStream.ReadFromJson<Message>();
            //the body is a JObject at this point - deserialize to the real message type:
            message.Body = message.Body.ToString().ReadFromJson(message.MessageType);
            return message;
        }

        public static Message FromJson(string json)
        {
            var message = json.ReadFromJson<Message>();
            //the body is a JObject at this point - deserialize to the real message type:
            message.Body = message.Body.ToString().ReadFromJson(message.MessageType);
            return message;
        }
    }
}
