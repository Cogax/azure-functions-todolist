using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model;
using Core.Ports;

namespace Functions
{
    public class InMemoryRepository : ITodoRepository
    {
        private List<TodoItem> _items = new List<TodoItem>();

        public Task<TodoItem> GetTodoItemById(Guid id)
        {
            return Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
        }

        public Task Add(TodoItem item)
        {
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task<List<TodoItem>> GetAll()
        {
            return Task.FromResult(_items);
        }
    }
}
