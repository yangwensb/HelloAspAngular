using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.Infra
{
    public interface IVersionable
    {
        int Id { get; set; }
        byte[] EntityVersion { get; set; }
    }
}
