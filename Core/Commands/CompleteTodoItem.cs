using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Common;
using Core.Ports;
using MediatR;

namespace Core.Commands
{
    public class CompleteTodoItem
    {
        public class Command : ICommand
        {
            public Guid Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly ITodoRepository _repository;

            public Handler(ITodoRepository repository)
            {
                _repository = repository;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                (await _repository.GetTodoItemById(request.Id))?.Complete();
            }
        }
    }
}
