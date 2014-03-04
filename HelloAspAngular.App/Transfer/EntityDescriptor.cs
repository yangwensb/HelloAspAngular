using HelloAspAngular.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAspAngular.App.Transfer
{
    public class EntityDescriptor: IVersioned
    {
        public int Id { get; set; }
        public byte[] EntityVersion { get; set; }

        public EntityDescriptor(int id, byte[] entityVersion)
        {
            Id = id;
            EntityVersion = entityVersion;
        }

        public EntityDescriptor(IVersioned versioned)
        {
            Id = versioned.Id;
            EntityVersion = versioned.EntityVersion;
        }
    }
}
