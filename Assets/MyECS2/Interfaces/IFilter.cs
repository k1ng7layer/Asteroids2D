using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyECS2
{
    public interface IFilter
    {
        public List<int> AttachedComponentsHash { get; set; }
        internal int RegisteredEntitiesCount { get;}
        public void RegisterEntity(ref EntityObject entity);
        public void UnregisterEntity(ref EntityObject entity);
        public void Lock();
        public void Unlock();
    }
}
