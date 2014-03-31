using HelloAspAngular.App.Transfer;
using HelloAspAngular.Domain;
using HelloAspAngular.Domain.TodoLists;
using HelloAspAngular.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.App
{
    public class TodoListAppService: ITodoListAppService
    {
        private IUnitOfWork _unitOfWork;
        private ITodoListService _todoListService;
        private ITodoListRepository _todoListRepository;

        public TodoListAppService(IUnitOfWork unitOfWork, ITodoListService todoListService, ITodoListRepository todoListRepository)
        {
            _unitOfWork = unitOfWork;
            _todoListService = todoListService;
            _todoListRepository = todoListRepository;
        }

        public async Task<AddTodoAsyncResult> AddTodoAsync(EntityDescriptor todoListDesc, Todo todo)
        {
            var list = new TodoList()
            {
                Id = todoListDesc.Id,
                EntityVersion = todoListDesc.EntityVersion,
            };

            _todoListRepository.Attach(list);
            list.Todos.Add(todo);

            var prepared = _todoListRepository.PrepareVersioning(todoListDesc);
            await _unitOfWork.SaveAsync();

            return new AddTodoAsyncResult(new EntityDescriptor(prepared), todo);
        }

        public async Task<EntityDescriptor> UpdateTodoAsync(EntityDescriptor todoListDesc, Todo todo)
        {
            var storedList = await _todoListRepository.FindAsync(l => l.Id == todoListDesc.Id, new[] { "Todos" });
            storedList.ChangeTodo(todo);

            var prepared = _todoListRepository.PrepareVersioning(todoListDesc);
            await _unitOfWork.SaveAsync();

            return new EntityDescriptor(prepared);
        }

        public async Task<EntityDescriptor> ArchiveAsync(EntityDescriptor todoListDesc)
        {
            var storedList = await _todoListRepository.FindAsync(l => l.Id == todoListDesc.Id, new[] { "Todos" });
            await _todoListService.ArchiveAsync(storedList);

            var prepared = _todoListRepository.PrepareVersioning(todoListDesc);
            await _unitOfWork.SaveAsync();

            return new EntityDescriptor(prepared);
        }

        public async Task<EntityDescriptor> ClearTodosAsync(EntityDescriptor todoListDesc)
        {
            var storedList = await _todoListRepository.FindAsync(l => l.Id == todoListDesc.Id, new[] { "Todos" });
            var todos = storedList.Todos.ToArray();
            storedList.Todos.Clear();
            _todoListRepository.RemoveTodos(todos);

            var prepared = _todoListRepository.PrepareVersioning(todoListDesc);
            await _unitOfWork.SaveAsync();

            return new EntityDescriptor(prepared);
        }
    }
}
