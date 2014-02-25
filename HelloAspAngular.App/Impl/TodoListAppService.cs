using HelloAspAngular.App.Dto;
using HelloAspAngular.Domain;
using HelloAspAngular.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.App.Impl
{
    public class TodoListAppService: ITodoListAppService
    {
        private IUnitOfWork _unitOfWork;

        public TodoListAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            var lists = await _unitOfWork.TodoListRepository.FindAsync(l => l.Id == list.Id, null, new[] { "Todos" });
            var storedList = lists.FirstOrDefault();

            var todos = storedList.Todos.Where(t => t.IsDone);
            _unitOfWork.TodoListRepository.RemoveTodos(todos);

            _unitOfWork.TodoListRepository.Touch(list);
            await _unitOfWork.SaveAsync();

            return new EntityDescriptor(list.Id, list.RowVersion);
        }
    }
}
