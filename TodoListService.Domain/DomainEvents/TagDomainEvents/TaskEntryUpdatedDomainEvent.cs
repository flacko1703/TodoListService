﻿using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.DomainEvents.TagDomainEvents;

public record TagUpdatedDomainEvent(Guid Id) : DomainEvent(Id)
{
    
}