using HelloAspAngular.Domain;
using HelloAspAngular.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Infra.Repositories
{
    public class TodoListRepository : VersionableRepository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(TodoListContext context): base(context)
        {
        }

        public void UpdateTodo(Todo todo)
        {
            Context.Todos.Attach(todo);
            var entry = Context.Entry(todo);
            entry.Property(t => t.IsDone).IsModified = true;
            entry.Property(t => t.Text).IsModified = true;
        }

        public void RemoveTodos(IEnumerable<Todo> todos)
        {
            Context.Todos.RemoveRange(todos);
        }
    }
}
