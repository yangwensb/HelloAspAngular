using AutoMapper;
using HelloAspAngular.App;
using HelloAspAngular.App.Dto;
using HelloAspAngular.Domain.Todos;
using HelloAspAngular.Web.Api.ResourceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using HelloAspAngular.Web.Api.HttpActionResults;

namespace HelloAspAngular.Web.Controllers
{
    public class TodoListsController : ApiController
    {
        private ITodoListAppService _todoListAppService;
        private ITodoListRepository _todoListRepository;

        public TodoListsController(ITodoListAppService todoListService, ITodoListRepository todoListRepository)
        {
            _todoListAppService = todoListService;
            _todoListRepository = todoListRepository;
        }

        // GET api/todolists
        public async Task<IEnumerable<TodoListResourceModel>> Get()
        {
            var lists = await _todoListRepository.FindAllAsync();
            return Mapper.Map<IEnumerable<TodoListResourceModel>>(lists);
        }

        // GET api/todolists/{id}
        public async Task<IHttpActionResult> Get(int id)
        {
            var list = await _todoListRepository.FindAsync(l => l.Id == id, new[] { "Todos" });
            var listRm = Mapper.Map<TodoListDetailResourceModel>(list);
            return ETagOk(list.RowVersion, listRm);
        }

        // POST api/todolists/{id}/todos
        [Route("api/todolists/{id}/todos")]
        public async Task<IHttpActionResult> PostTodo(int id, TodoResourceModel input)
        {
            var todoListDesc = new EntityDescriptor(id, GetETag());
            var todo = Mapper.Map<Todo>(input);

            var ret = await _todoListAppService.AddTodoAsync(todoListDesc, todo);

            var todoRm = Mapper.Map<TodoResourceModel>(ret.Todo);
            var path = string.Format("~/api/todolists/{0}/todos/{1}", id, ret.Todo.Id);
            var uri = Url.Content(path);
            return ETagCreated(ret.TodoListDescriptor.RowVersion, uri, todoRm);
        }

        // PUT api/todolists/{id}/todos/{todoId}
        [Route("api/todolists/{id}/todos/{todoId}")]
        public async Task<IHttpActionResult> PutTodo(int id, int todoId, TodoResourceModel input)
        {
            var todoListDesc = new EntityDescriptor(id, GetETag());
            var todo = Mapper.Map<Todo>(input);
            todo.Id = todoId;

            var ret = await _todoListAppService.UpdateTodoAsync(todoListDesc, todo);

            return ETagOk(ret.RowVersion, string.Empty);
        }

        // PUT api/todolists/{id}/archive
        [HttpPut, Route("api/todolists/{id}/archive")]
        public async Task<IHttpActionResult> Archive(int id)
        {
            var todoListDesc = new EntityDescriptor(id, GetETag());

            var ret = await _todoListAppService.ArchiveAsync(todoListDesc);

            return ETagOk(ret.RowVersion, string.Empty);
        }

        private IHttpActionResult ETagOk<T>(byte[] rowVersion, T content)
        {
            return new ETagOkActionResult<T>(rowVersion, content, this);
        }

        private IHttpActionResult ETagCreated<T>(byte[] rowVersion, string uri, T content)
        {
            return new ETagCreatedActionResult<T>(rowVersion, new Uri(uri), content, this);
        }

        private byte[] GetETag()
        {
            var etagFromClient = Request.Headers.IfMatch.FirstOrDefault();
            return etagFromClient == null ?
                null :
                Convert.FromBase64String(etagFromClient.Tag.Replace("\"", String.Empty));
        }
    }
}