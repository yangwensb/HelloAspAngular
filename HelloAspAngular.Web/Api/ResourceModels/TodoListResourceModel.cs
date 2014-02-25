using HelloAspAngular.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloAspAngular.Web.Api.ResourceModels
{
    public class TodoListResourceModel: IVersionable
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string Name { get; set; }
    }
}