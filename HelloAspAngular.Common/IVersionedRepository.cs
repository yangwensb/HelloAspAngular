using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Common
{
    public interface IVersionedRepository<TEntity> : IRepository<TEntity> where TEntity : class, IVersioned, new()
    {
        TEntity PrepareVersioning(IVersioned versioned);
    }
}
