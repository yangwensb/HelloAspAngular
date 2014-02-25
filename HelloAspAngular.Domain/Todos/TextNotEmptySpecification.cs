using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Domain.Todos
{
    public class TextNotEmptySpecification: ISpecification<Todo>
    {
        public bool IsSatisfiedBy(Todo subject)
        {
            return !string.IsNullOrWhiteSpace(subject.Text);
        }
    }
}
