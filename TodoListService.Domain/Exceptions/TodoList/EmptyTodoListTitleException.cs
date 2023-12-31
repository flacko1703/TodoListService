﻿using TodoListService.Shared.Abstractions.SeedWork;

namespace TodoListService.Domain.Exceptions.TodoList;

public class EmptyTodoListTitleException : DomainException
{
    public EmptyTodoListTitleException() 
        : base($"TodoList Title cannot be empty.")
    {
    }
}