using HelloAspAngular.App.Transfer;
using HelloAspAngular.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.App
{
    public interface ITodoListAppService
    {
        Task<EntityDescriptor> ArchiveAsync(EntityDescriptor todoListDesc);
        Task<AddTodoAsyncResult> AddTodoAsync(EntityDescriptor todoListDesc, Todo todo);
        Task<EntityDescriptor> UpdateTodoAsync(EntityDescriptor todoListDesc, Todo todo);
    }
}
