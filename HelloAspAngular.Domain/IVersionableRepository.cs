using HelloAspAngular.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Domain
{
    public interface IVersionableRepository<TEntity> : IRepository<TEntity> where TEntity : class, IVersionable
    {
        void Touch(TEntity entity);
    }
}
