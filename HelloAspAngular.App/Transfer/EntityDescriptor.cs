using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.App.Transfer
{
    public class EntityDescriptor
    {
        public int Id { get; private set; }
        public byte[] RowVersion { get; private set; }

        public EntityDescriptor(int id, byte[] rowVersion)
        {
            Id = id;
            RowVersion = rowVersion;
        }
    }
}
