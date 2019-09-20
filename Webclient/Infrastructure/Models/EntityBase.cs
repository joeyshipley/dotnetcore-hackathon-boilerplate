using System;
using System.Collections.Generic;
using System.Linq;
using BOS.Webclient.Models.Messaging;
using FluentValidation;

namespace BOS.Webclient.Infrastructure.Models
{
    public abstract class EntityBase<T, TValdiator> 
        : IEntity
        where TValdiator : IValidator<T>, new()
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

        public (bool IsValid, List<Message> Messages) Validate()
        {
            var validator = new TValdiator();
            var results = validator.Validate(this);
            var messages = results.Errors
                .Select(e => Message.For(e.PropertyName, e.ErrorMessage))
                .Distinct()
                .ToList();
            return (IsValid: !messages.Any(), Messages: messages);
        }
    }
}