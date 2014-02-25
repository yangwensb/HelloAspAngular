using HelloAspAngular.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Domain.Todos
{
    public class TodoList: IVersionable
    {
        public TodoList()
        {
            Todos = new List<Todo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Todo> Todos { get; private set; }

        public IEnumerable<Todo> Archive()
        {
            var todos = Todos.Where(t => t.IsDone).ToArray();
            foreach (var todo in todos)
            {
                Todos.Remove(todo);
            }
            return todos;
        }
    }
}
