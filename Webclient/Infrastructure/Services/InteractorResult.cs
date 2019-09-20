using System;
using System.Collections.Generic;
using BOS.Webclient.Models.Messaging;

namespace BOS.Webclient.Infrastructure.Services
{
    public class InteractorResult<T>
        where T : class
    {
        public bool ActionSuccess { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public T Data { get; set; }
        public Exception Exception { get; set; }

        public static InteractorResult<T> ForSuccess(T data)
        {
            return new InteractorResult<T>
            {
                ActionSuccess = true,
                Data = data
            };
        }
        
        public static InteractorResult<T> ForFailure(Message message)
        {
            var messages = new List<Message> { message };
            return ForFailure(messages);
        }
                
        public static InteractorResult<T> ForFailure(List<Message> messages)
        {
            return new InteractorResult<T>
            {
                ActionSuccess = false,
                Messages = messages
            };
        }
        
        public static InteractorResult<T> ForFailure(Exception exception)
        {
            var messages = new List<Message> { Message.For("Exception", exception.Message) };
            return new InteractorResult<T>
            {
                ActionSuccess = false,
                Messages = messages,
                Exception = exception
            };
        }
    }

    public class VoidInteractorResult : InteractorResult<dynamic>
    {
        public static VoidInteractorResult ForSuccess()
        {
            return new VoidInteractorResult
            {
                ActionSuccess = true
            };
        }
        
        public static VoidInteractorResult ForFailure(Message message)
        {
            var messages = new List<Message> { message };
            return ForFailure(messages);
        }
                
        public static VoidInteractorResult ForFailure(List<Message> messages)
        {
            return new VoidInteractorResult
            {
                ActionSuccess = false,
                Messages = messages
            };
        }
        
        public static VoidInteractorResult ForFailure(Exception exception)
        {
            var messages = new List<Message> { Message.For("Exception", exception.Message) };
            return new VoidInteractorResult
            {
                ActionSuccess = false,
                Messages = messages,
                Exception = exception
            };
        }
    }
}