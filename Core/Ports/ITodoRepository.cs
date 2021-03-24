using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Ports
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetTodoItemById(Guid id);
        Task Add(TodoItem item);
        Task<List<TodoItem>> GetAll();
    }
}