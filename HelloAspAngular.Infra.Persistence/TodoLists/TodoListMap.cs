﻿using HelloAspAngular.Domain.TodoLists;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Infra.Persistence.TodoLists
{
    internal class TodoListMap : EntityTypeConfiguration<TodoList>
    {
        public TodoListMap()
        {
            this.HasKey(l => l.Id);

            this.Property(l => l.EntityVersion).IsRowVersion();

            this.HasMany(l => l.Todos).
                WithRequired(t => t.TodoList).
                HasForeignKey(t => t.TodoListId).
                WillCascadeOnDelete(true);
        }
    }
}
