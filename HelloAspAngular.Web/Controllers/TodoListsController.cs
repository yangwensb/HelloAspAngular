using AutoMapper;
using HelloAspAngular.App;
using HelloAspAngular.App.Transfer;
using HelloAspAngular.Domain.TodoLists;
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
        public async Task<IHttpActionResult> Get()
        {
            var lists = await _todoListRepository.FindAllAsync(l => l.Kind == TodoListKind.Normal);
            return Ok(Mapper.Map<IEnumerable<TodoListResourceModel>>(lists));
        }

        // GET api/todolists/{id}
        public async Task<IHttpActionResult> Get(int id)
        {
            var list = await _todoListRepository.FindAsync(l => l.Id == id, new[] { "Todos" });
            var listRm = Mapper.Map<TodoListDetailResourceModel>(list);
            return ETagOk(list.EntityVersion, listRm);
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
            return ETagCreated(ret.TodoListDescriptor.EntityVersion, uri, todoRm);
        }

        // PUT api/todolists/{id}/todos/{todoId}
        [Route("api/todolists/{id}/todos/{todoId}")]
        public async Task<IHttpActionResult> PutTodo(int id, int todoId, TodoResourceModel input)
        {
            var todoListDesc = new EntityDescriptor(id, GetETag());
            var todo = Mapper.Map<Todo>(input);
            todo.Id = todoId;
            var ret = await _todoListAppService.UpdateTodoAsync(todoListDesc, todo);
            return ETagOk(ret.EntityVersion, string.Empty);
        }

        // GET api/todolists/archived
        [Route("api/todolists/archived")]
        public async Task<IHttpActionResult> GetArchived()
        {
            var list = await _todoListRepository.FindAsync(l => l.Kind == TodoListKind.Archived, new[] { "Todos" });
            var listRm = Mapper.Map<TodoListDetailResourceModel>(list);
            return ETagOk(list.EntityVersion, listRm);
        }

        // DELETE api/todolists/{id}/clear
        [Route("api/todolists/{id}/clear")]
        public async Task<IHttpActionResult> DeleteTodos(int id)
        {
            var todoListDesc = new EntityDescriptor(id, GetETag());
            var ret = await _todoListAppService.ClearTodosAsync(todoListDesc);
            return ETagOk(ret.EntityVersion, string.Empty);
        }

        // PUT api/todolists/{id}/archive
        [Route("api/todolists/{id}/archive")]
        public async Task<IHttpActionResult> PutArchive(int id)
        {
            var todoListDesc = new EntityDescriptor(id, GetETag());
            var ret = await _todoListAppService.ArchiveAsync(todoListDesc);
            return ETagOk(ret.EntityVersion, string.Empty);
        }

        private IHttpActionResult ETagOk<T>(byte[] entityVersion, T content)
        {
            return new ETagOkActionResult<T>(entityVersion, content, this);
        }

        private IHttpActionResult ETagCreated<T>(byte[] entityVersion, string uri, T content)
        {
            return new ETagCreatedActionResult<T>(entityVersion, new Uri(uri), content, this);
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