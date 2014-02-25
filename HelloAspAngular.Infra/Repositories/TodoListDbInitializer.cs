using HelloAspAngular.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Infra.Repositories
{
    public class TodoListDbInitializer : DropCreateDatabaseIfModelChanges<TodoListContext>
    {
        protected override void Seed(TodoListContext context)
        {
            {
                var list = new TodoList()
                {
                    Name = "リスト1",
                };
                context.TodoLists.Add(list);
            }
            {
                var list = new TodoList()
                {
                    Name = "リスト2",
                };
                context.TodoLists.Add(list);
            }
            {
                var list = new TodoList()
                {
                    Name = "リスト3",
                };
                context.TodoLists.Add(list);
            }

            context.SaveChanges();
        }
    }
}
