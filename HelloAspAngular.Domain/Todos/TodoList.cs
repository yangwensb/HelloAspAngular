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
    }
}
