using HelloAspAngular.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Infra.Repositories
{
    internal class TodoMap : EntityTypeConfiguration<Todo>
    {
        public TodoMap()
        {
            this.ToTable("Todos");

            this.HasKey(t => t.Id);
        }
    }
}
