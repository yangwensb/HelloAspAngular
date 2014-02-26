using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Infra.Persistence
{
    public class VersionableRepository<TEntity, TContext> : GenericRepository<TEntity, TContext>
        where TEntity: class, IVersionable
        where TContext: DbContext
    {
        public VersionableRepository(TContext context)
            : base(context)
        {

        }

        public virtual void Touch(TEntity entity)
        {
            var entry = default(DbEntityEntry<TEntity>);

            var entries = Context.ChangeTracker.Entries<TEntity>();
            entry = entries.Where(e => e.Entity.Id == entity.Id).FirstOrDefault();

            if (entry == null)
            {
                entry = Context.Entry(entity);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
            }

            entry.Property("RowVersion").IsModified = true;
        }
    }
}
