﻿using TodoListService.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace TodoListService.Application.DTOs.Request.Tag;

public record GetTagRequestDto(TagId TagId);