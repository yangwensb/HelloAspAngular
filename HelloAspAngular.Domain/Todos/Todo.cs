﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelloAspAngular.Domain.Todos
{
    public class Todo
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Text { get; set; }

        public int TodoListId { get; set; }
        public virtual TodoList TodoList { get; set; }
    }
}
