using System;
using System.Collections.Generic;
using System.Linq;
using BOS.Webclient.Models.Messaging;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BOS.Webclient.Infrastructure.Models
{
    public abstract class IdentityUserBase<T, TValdiator>
        : IdentityUser<Guid>, IEntity
        where TValdiator : IValidator<T>, new()
    {
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