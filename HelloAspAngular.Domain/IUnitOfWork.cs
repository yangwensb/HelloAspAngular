using HelloAspAngular.Common;
using HelloAspAngular.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Domain
{
    public interface IUnitOfWork: IDisposable
    {
        ITodoListRepository TodoListRepository { get; }

        IUnitOfWorkTransaction BeginTransaction();
        Task SaveAsync();
    }
}
