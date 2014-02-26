using HelloAspAngular.App.Transfer;
using HelloAspAngular.Domain;
using HelloAspAngular.Domain.TodoLists;
using HelloAspAngular.Infra;
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

            _todoListRepository.Touch(list);
            await _unitOfWork.SaveAsync();

            return new AddTodoAsyncResult(new EntityDescriptor(list.Id, list.EntityVersion), todo);
        }

        public async Task<EntityDescriptor> UpdateTodoAsync(EntityDescriptor todoListDesc, Todo todo)
        {
            var list = new TodoList()
            {
                Id = todoListDesc.Id,
                EntityVersion = todoListDesc.EntityVersion,
            };

            _todoListRepository.UpdateTodo(todo);

            _todoListRepository.Touch(list);
            await _unitOfWork.SaveAsync();

            return new EntityDescriptor(list.Id, list.EntityVersion);
        }

        public async Task<EntityDescriptor> ArchiveAsync(EntityDescriptor todoListDesc)
        {
            var list = new TodoList()
            {
                Id = todoListDesc.Id,
                EntityVersion = todoListDesc.EntityVersion,
            };

            var storedList = await _todoListRepository.FindAsync(l => l.Id == list.Id, new[] { "Todos" });
            _todoListService.Archive(storedList);

            _todoListRepository.Touch(list);
            await _unitOfWork.SaveAsync();

            return new EntityDescriptor(list.Id, list.EntityVersion);
        }
    }
}
