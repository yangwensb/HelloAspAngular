using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace HelloAspAngular.Web.Api.HttpActionResults
{
    public class ETagOkActionResult<T>: OkNegotiatedContentResult<T>
    {
        private byte[] _rowVersion;

        public ETagOkActionResult(byte[] rowVersion, T content, ApiController controller) : base(content, controller)
        {
            _rowVersion = rowVersion;
        }

        public async override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await base.ExecuteAsync(cancellationToken);
            response.Headers.ETag = new EntityTagHeaderValue("\"" + Convert.ToBase64String(_rowVersion) + "\"");
            return response;
        }
    }
}