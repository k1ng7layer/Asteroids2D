using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyECS2
{
    public class OnEntityDestroyArgs
    {
        public OnEntityDestroyArgs(ref EntityObject entity)
        {
            entityObject = entity;
        }
        public EntityObject entityObject;
    }
}
