using HelloAspAngular.Domain.TodoLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.App.Transfer
{
    public class AddTodoAsyncResult
    {
        public EntityDescriptor TodoListDescriptor { get; private set; }
        public Todo Todo { get; private set; }

        public AddTodoAsyncResult(EntityDescriptor todoListDesc, Todo todo)
        {
            TodoListDescriptor = todoListDesc;
            Todo = todo;
        }
    }
}
