using HelloAspAngular.Common;
using HelloAspAngular.Domain;
using HelloAspAngular.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Infra.Repositories
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private TodoListContext _context;

        internal TodoListContext Context {
            get { return _context; }
        }

        private Lazy<ITodoListRepository> _todoListRepos;
        public ITodoListRepository TodoListRepository {
            get { return _todoListRepos.Value; }
        }

        private bool _isDisposed = false;

        public UnitOfWork(TodoListContext context)
        {
            _context = context;
            _todoListRepos = new Lazy<ITodoListRepository>(() => new TodoListRepository(_context));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }

                _context = null;
                _isDisposed = true;
            }
        }
        
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IUnitOfWorkTransaction BeginTransaction()
        {
            return new UnitOfWorkTransaction(_context.Database.BeginTransaction());
        }
    }
}
