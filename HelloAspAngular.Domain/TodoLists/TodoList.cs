using HelloAspAngular.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Domain.TodoLists
{
    public class TodoList: IVersionable
    {
        public TodoList()
        {
            Todos = new List<Todo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] EntityVersion { get; set; }
        public virtual ICollection<Todo> Todos { get; private set; }

        public IEnumerable<Todo> GetTodosShouldBeArchived()
        {
            return Todos.Where(t => t.IsDone).ToArray();
        }
    }
}
