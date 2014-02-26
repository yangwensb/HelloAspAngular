using HelloAspAngular.Domain.TodoLists;
using HelloAspAngular.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.App
{
    public interface IAppUnitOfWork: IUnitOfWork
    {
        ITodoListRepository TodoListRepository { get; }
    }
}
