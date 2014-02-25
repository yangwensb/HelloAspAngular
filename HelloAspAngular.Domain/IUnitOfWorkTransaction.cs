﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Domain
{
    public interface IUnitOfWorkTransaction
    {
        void Commit();
        void Rollback();
    }
}
