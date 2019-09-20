using System;
using System.Collections.Generic;
using BOS.Webclient.Models.Messaging;

namespace BOS.Webclient.Infrastructure.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime UpdatedOn { get; set; }
        
        (bool IsValid, List<Message> Messages) Validate();
    }
}