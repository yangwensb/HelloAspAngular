using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Domain.TodoLists
{
    public class TodoListService: ITodoListService
    {
        private ITodoListRepository _todoListRepository;

        public TodoListService(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        public void Archive(TodoList todoList)
        {
            var todos = todoList.Todos.Where(t => t.IsDone).ToArray();
            foreach (var todo in todos)
            {
                todoList.Todos.Remove(todo);
            }
            _todoListRepository.RemoveTodos(todos);
        }
    }
}
