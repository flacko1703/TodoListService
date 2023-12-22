﻿namespace TodoListService.Shared.Messaging.Contracts;

public record TaskEntryAdded
{
    public Guid TodoListId { get; init; }
    
    public Guid TaskEntryId { get; init; }
    
    public string Title { get; init; }
    
    public string Text { get; init; }

    public IEnumerable<string> Tags { get; init; }

    public string Status{ get; init; }
}