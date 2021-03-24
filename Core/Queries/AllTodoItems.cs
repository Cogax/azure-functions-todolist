using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Common;
using Core.Model;
using Core.Ports;
using MediatR;

namespace Core.Queries
{
    public class AllTodoItems
    {
        public class Query : IQuery<List<TodoItemDto>>
        { }

        public class Handler : IRequestHandler<Query, List<TodoItemDto>>
        {
            private readonly ITodoRepository _repository;

            public Handler(ITodoRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TodoItemDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return (await _repository.GetAll()).Select(TodoItemDto.FromModel).ToList();
            }
        }

        public class TodoItemDto
        {
            public Guid Id { get; set; }
            public string Label { get; set; }
            public bool Completed { get; set; }

            public static TodoItemDto FromModel(TodoItem model)
            {
                return new TodoItemDto
                {
                    Id        = model.Id,
                    Label     = model.Label,
                    Completed = model.Done
                };
            }
        }
    }
}