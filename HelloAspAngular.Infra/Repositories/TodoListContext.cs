using HelloAspAngular.Domain;
using HelloAspAngular.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HelloAspAngular.Infra.Repositories
{
    public class TodoListContext: DbContext
    {
        // EntityFramework.SqlServer.dllがbinにコピーされるようにする。
        private static Type _dummyForDllCopy = typeof(System.Data.Entity.SqlServer.SqlProviderServices);

        static TodoListContext()
        {
            Database.SetInitializer(new TodoListDbInitializer());
        }

        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Todo> Todos { get; set; }

        public TodoListContext(): base()
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TodoListMap());
            modelBuilder.Configurations.Add(new TodoMap());
        }
    }
}
