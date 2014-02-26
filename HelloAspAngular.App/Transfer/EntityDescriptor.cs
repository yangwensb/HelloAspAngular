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
        public byte[] EntityVersion { get; private set; }

        public EntityDescriptor(int id, byte[] entityVersion)
        {
            Id = id;
            EntityVersion = entityVersion;
        }
    }
}
