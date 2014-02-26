using HelloAspAngular.App.Transfer;
using HelloAspAngular.Domain;
using HelloAspAngular.Domain.TodoLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.App
{
    public class TodoListAppService: ITodoListAppService
    {
        private IAppUnitOfWork _unitOfWork;
        private ITodoListService _todoListService;

        public TodoListAppService(IAppUnitOfWork unitOfWork, ITodoListService todoListService)
        {
            _unitOfWork = unitOfWork;
            _todoListService = todoListService;
        }

        public async Task<AddTodoAsyncResult> AddTodoAsync(EntityDescriptor todoListDesc, Todo todo)
        {
            var list = new TodoList()
            {
                Id = todoListDesc.Id,
                RowVersion = todoListDesc.RowVersion,
            };

            _unitOfWork.TodoListRepository.Attach(list);
            list.Todos.Add(todo);

            _unitOfWork.TodoListRepository.Touch(list);
            await _unitOfWork.SaveAsync();

            return new AddTodoAsyncResult(new EntityDescriptor(list.Id, list.RowVersion), todo);
        }

        public async Task<EntityDescriptor> UpdateTodoAsync(EntityDescriptor todoListDesc, Todo todo)
        {
            var list = new TodoList()
            {
                Id = todoListDesc.Id,
                RowVersion = todoListDesc.RowVersion,
            };

            _unitOfWork.TodoListRepository.UpdateTodo(todo);

            _unitOfWork.TodoListRepository.Touch(list);
            await _unitOfWork.SaveAsync();

            return new EntityDescriptor(list.Id, list.RowVersion);
        }

        public async Task<EntityDescriptor> ArchiveAsync(EntityDescriptor todoListDesc)
        {
            var list = new TodoList()
            {
                Id = todoListDesc.Id,
                RowVersion = todoListDesc.RowVersion,
            };

            var storedList = await _unitOfWork.TodoListRepository.FindAsync(l => l.Id == list.Id, new[] { "Todos" });
            _todoListService.Archive(storedList);

            _unitOfWork.TodoListRepository.Touch(list);
            await _unitOfWork.SaveAsync();

            return new EntityDescriptor(list.Id, list.RowVersion);
        }
    }
}
