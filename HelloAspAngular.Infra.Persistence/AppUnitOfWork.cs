using HelloAspAngular.App;
using HelloAspAngular.Domain;
using HelloAspAngular.Domain.TodoLists;
using HelloAspAngular.Infra.Persistence.TodoLists;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Infra.Persistence
{
    public class AppUnitOfWork: GenericUnitOfWork, IDisposable
    {
        public AppUnitOfWork(AppContext context) : base(context)
        {
        }
    }
}
