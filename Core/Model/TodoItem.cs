using System;
using Ardalis.GuardClauses;

namespace Core.Model
{
    public class TodoItem
    {
        public Guid Id { get; private set; }
        public string Label { get; private set; }
        public bool Done { get; private set; }

        public static TodoItem Create(Guid id, string label)
        {
            Guard.Against.Default(id, nameof(id));
            Guard.Against.NullOrWhiteSpace(label, nameof(label));

            return new TodoItem
            {
                Id    = id,
                Label = label,
                Done  = false
            };
        }

        public TodoItem() {}

        public void Complete()
        {
            Done = true;
        }
    }
}
