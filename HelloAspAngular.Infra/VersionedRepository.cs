using HelloAspAngular.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Infra
{
    public class VersionedRepository<TEntity, TContext> : Repository<TEntity, TContext>
        where TEntity: class, IVersioned, new()
        where TContext: DbContext
    {
        private static readonly string VersionPropertyName;

        static VersionedRepository()
        {
            VersionPropertyName = StaticReflection.GetMemberInfo<IVersioned, byte[]>(v => v.EntityVersion).Name;
        }

        public VersionedRepository(TContext context)
            : base(context)
        {

        }

        public virtual TEntity PrepareVersioning(IVersioned versioned)
        {
            if (versioned.Id != 0 && versioned.EntityVersion == null)
            {
                throw new ArgumentNullException("versioned.EntityVersion");
            }

            var entry = default(DbEntityEntry<TEntity>);

            var entries = Context.ChangeTracker.Entries<TEntity>();
            entry = entries.Where(e => e.Entity.Id == versioned.Id).FirstOrDefault();

            if (entry != null)
            {
                entry.OriginalValues[VersionPropertyName] = versioned.EntityVersion;
            }
            else
            {
                var entity = new TEntity()
                {
                    Id = versioned.Id,
                    EntityVersion = versioned.EntityVersion,
                };
                DbSet.Attach(entity);
                entry = Context.Entry(entity);
            }

            entry.Property(VersionPropertyName).IsModified = true;

            return entry.Entity;
        }
    }
}
