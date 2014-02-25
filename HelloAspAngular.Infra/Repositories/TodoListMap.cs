using HelloAspAngular.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Infra.Repositories
{
    internal class TodoListMap : EntityTypeConfiguration<TodoList>
    {
        public TodoListMap()
        {
            this.HasKey(l => l.Id);

            this.Property(l => l.RowVersion).IsRowVersion();

            this.HasMany(l => l.Todos).
                WithRequired(t => t.TodoList).
                HasForeignKey(t => t.TodoListId).
                WillCascadeOnDelete(true);
        }
    }
}
