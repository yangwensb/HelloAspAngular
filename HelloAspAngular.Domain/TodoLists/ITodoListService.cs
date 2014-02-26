using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Domain.TodoLists
{
    public interface ITodoListService
    {
        void Archive(TodoList todoList);
    }
}
