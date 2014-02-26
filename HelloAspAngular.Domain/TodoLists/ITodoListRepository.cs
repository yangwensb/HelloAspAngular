using HelloAspAngular.Domain;
using HelloAspAngular.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Domain.TodoLists
{
    public interface ITodoListRepository: IVersionableRepository<TodoList>
    {
        void UpdateTodo(Todo todo);
        void RemoveTodos(IEnumerable<Todo> todos);
    }
}
